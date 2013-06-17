using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GenericGameEngine;
using Tao.OpenGl;

namespace CombatCarsWinformsClient
{
    class PlayerCharacter : Entity
    {
        double _speed = 512;
        private bool _dead;
        BulletManager _bulletManager;
        Vector _gunOffset = new Vector(55, 0, 0);
        Texture _bulletTexture;
        static readonly double FireRecovery = 0.25;
        double _fireRecoveryTime = 0;

        public PlayerCharacter(GenericGameEngine.TextureManager textureManager, BulletManager bulletManager)
        {
            _sprite.Texture = textureManager.Get(EnumTexture.PlayerShip);
            //_sprite.SetScale(0.5, 0.5);
            _dead = false;
            _bulletManager = bulletManager;
            _bulletTexture = textureManager.Get(EnumTexture.Bullet);
        }

        public void Update(double elapsedTime)
        {
            _fireRecoveryTime = Math.Max(0, (_fireRecoveryTime - elapsedTime));
        }

        public void Render(GenericGameEngine.Renderer renderer)
        {
            renderer.DrawSprite(_sprite);
            Render_Debug();
        }

        public void Move(Vector amount)
        {
            amount *= _speed;
            _sprite.SetPosition(_sprite.GetPosition() + amount);
        }

        public bool IsDead { get { return _dead; } }

        internal void OnCollision(Entity entity)
        {
            _dead = true;
        }

        public void Fire()
        {
            if (_fireRecoveryTime > 0)
            {
                return;
            }
            else
            {
                _fireRecoveryTime = FireRecovery;
            }

            Bullet bullet = new Bullet(_bulletTexture);
            bullet.Speed = 1024;
            bullet.SetColor(new GenericGameEngine.Color(0, 1, 0, 1));
            bullet.SetPosition(_sprite.GetPosition() + _gunOffset);
            _bulletManager.Shoot(bullet);
        }

        internal Vector GetPosition()
        {
            return _sprite.GetPosition();
        }
    }
}
