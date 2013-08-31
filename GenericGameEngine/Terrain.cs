using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericGameEngine
{
    public class Terrain : Sprite
    {
        public Terrain(Texture texture, float combatCarsDotWidth, int combatCarsDotHeight, int combatCarsDotX, int combatCarsDotY)
        {
            this.Texture = texture;
            this.CombatCarsDotWidth = combatCarsDotWidth;
            this.CombatCarsDotHeight = combatCarsDotHeight;
            this.CombatCarsDotX = combatCarsDotX;
            this.CombatCarsDotY = combatCarsDotY;
        }

        public float CombatCarsDotWidth { get; set; }
        public int CombatCarsDotHeight { get; set; }
        public int CombatCarsDotX { get; set; }
        public int CombatCarsDotY { get; set; }
    }
}
