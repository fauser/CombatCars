using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;

namespace CombatCarsWinformsClient
{
    class Bullet : Entity
    {
        public bool Dead { get; set; }
        public Vector Direction { get; set; }
        public double Speed { get; set; }

        public Bullet(Texture bulletTexture)
        {
            _sprite.Texture = bulletTexture;
            Dead = false;
            Direction = new Vector(1, 0, 0);
            Speed = 512;
        }

        public double X
        {
            get { return _sprite.GetPosition().X; }
        }

        public double Y
        {
            get { return _sprite.GetPosition().Y; }
        }

        public void SetPosition(Vector position)
        {
            _sprite.SetPosition(position);
        }

        public void SetColor(Color color)
        {
            _sprite.SetColor(color);
        }

        public void Update(double elapsedTime)
        {
            if (Dead)
            {
                return;
            }

            Vector position = _sprite.GetPosition();
            position += Direction * Speed * elapsedTime;
            _sprite.SetPosition(position);
        }

        public void Render(Renderer renderer)
        {
            if (Dead)
            {
                return;
            }

            Render_Debug();
            renderer.DrawSprite(_sprite);
        }

        internal void OnCollision(Entity entity)
        {

        }
    }
}
