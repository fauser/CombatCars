using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;

namespace CombatCarsWinformsClient
{
    public class Car : Entity
    {
        public Car(Texture texture, int combatCarsDotX, int combatCarsDotY)
        {
            this._sprite.Texture = texture;
            this.CombatCarsDotX = combatCarsDotX;
            this.CombatCarsDotY = combatCarsDotY;
        }

        public void Update(double elapsedTime)
        {
            if (Path != null)
            {
                Path.UpdatePosition(elapsedTime, this);
            }

        }

        public void Render(Renderer renderer)
        {
            Render_Debug();
            renderer.DrawSprite(_sprite);
        }

        public int CombatCarsDotX { get; set; }
        public int CombatCarsDotY { get; set; }
        public Path Path { get; set; }
    }
}
