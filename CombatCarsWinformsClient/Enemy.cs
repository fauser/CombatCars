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
        double _scale = 1;

        public Enemy(TextureManager textureManager)
        {
            _sprite.Texture = textureManager.Get(EnumTexture.Enemy);
            _sprite.SetScale(_scale, _scale);
            _sprite.SetPosition(200, 0);
        }

        public void Update(double elapsedTime)
        {

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

        }
    }
}
