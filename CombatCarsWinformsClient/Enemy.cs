using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GenericGameEngine;
using Tao.OpenGl;

namespace CombatCarsWinformsClient
{
    class Enemy : Entity
    {
        public int Health { get; set; }
        static readonly double HitFlashTime = 0.25;
        double _hitFlashCountDown = 0;
        EffectsManager _effectsManager;
        Random _random = new Random();
        double _shootCountDown;
        BulletManager _bulletManager;
        Texture _bulletTexture;
        PlayerCharacter _player;

        public Enemy(TextureManager textureManager, EffectsManager effectsManager, BulletManager bulletManager, PlayerCharacter player)
        {
            Health = 50;
            _sprite.Texture = textureManager.Get(EnumTexture.Enemy);
            _sprite.SetScale(0.5, 0.5);
            _sprite.SetPosition(200, 0);
            _effectsManager = effectsManager;
            _bulletManager = bulletManager;
            _bulletTexture = textureManager.Get(EnumTexture.Bullet);
            MaxTimeToShoot = 4;
            MinTimeToShoot = 0.5;
            _player = player;
            RestartShootCountDown();
        }

        private void RestartShootCountDown()
        {
            _shootCountDown = MinTimeToShoot + (_random.NextDouble() * MaxTimeToShoot);
        }

        public double MaxTimeToShoot { get; set; }
        public double MinTimeToShoot { get; set; }

        public Path Path { get; set; }

        public bool IsDead
        {
            get { return Health == 0; }
        }

        public void Update(double elapsedTime)
        {
            _shootCountDown = _shootCountDown - elapsedTime;
            if (_shootCountDown <= 0)
            {
                Bullet bullet = new Bullet(_bulletTexture);
                bullet.Speed = 350;

                Vector currentPosition = _sprite.GetPosition();
                Vector bulletDir = _player.GetPosition() - currentPosition;
                bulletDir = bulletDir.Normalize(bulletDir);
                bullet.Direction = bulletDir;
                
                bullet.SetPosition(_sprite.GetPosition());
                bullet.SetColor(new GenericGameEngine.Color(1, 0, 0, 1));
                _bulletManager.EnemyShoot(bullet);
                RestartShootCountDown();
            }

            if (Path != null)
            {
                Path.UpdatePosition(elapsedTime, this);
            }

            if (_hitFlashCountDown != 0)
            {
                _hitFlashCountDown = Math.Max(0, _hitFlashCountDown - elapsedTime);
                double scaledTime = 1 - (_hitFlashCountDown / HitFlashTime);
                _sprite.SetColor(new GenericGameEngine.Color(1, 1, (float)scaledTime, 1));
            }
        }

        public void Render(Renderer renderer)
        {
            Render_Debug();
            renderer.DrawSprite(_sprite);
        }

        public void SetPosition(Vector position)
        {
            _sprite.SetPosition(position);
        }

        internal void OnCollision(Entity entity)
        {
            if (Health == 0)
            {
                return;
            }

            Health = Math.Max(0, Health - 25);
            _hitFlashCountDown = HitFlashTime;
            _sprite.SetColor(new GenericGameEngine.Color(1, 1, 0, 1));

            if (Health == 0)
            {
                OnDestroyed();
            }
        }

        private void OnDestroyed()
        {
            _effectsManager.AddExplosion(_sprite.GetPosition());
        }
    }
}
