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

        public PlayerCharacter(GenericGameEngine.TextureManager textureManager)
        {
            _sprite.Texture = textureManager.Get(EnumTexture.PlayerShip);
            _dead = false;
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
    }
}
