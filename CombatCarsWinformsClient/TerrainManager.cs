using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;

namespace CombatCarsWinformsClient
{
    class TerrainManager
    {
        List<Terrain> _effects = new List<Terrain>();
        TextureManager _textureManager;
        public GameCoordinates GameCoordinates { get; set; }

        public TerrainManager(TextureManager textureManager)
        {
            _textureManager = textureManager;
        }

        public void Update(double elapsedTime)
        {
        }

        public void Render(Renderer renderer, GameCoordinates _gameCoordinates)
        {
            foreach (Terrain t in _effects)
            {
                if (t.CombatCarsDotWidth != t.CombatCarsDotHeight && t.GetWidth() > t.GetHeight())
                {
                    t.SetPosition(new Vector(_gameCoordinates._xes[t.CombatCarsDotX] + _gameCoordinates._delta / 2, _gameCoordinates._ys[t.CombatCarsDotY], 0));
                    t.SetWidth(_gameCoordinates._delta * t.CombatCarsDotWidth);
                    t.SetHeight(_gameCoordinates._delta * t.CombatCarsDotHeight);
                }
                else
                {
                    t.SetPosition(new Vector(_gameCoordinates._xes[t.CombatCarsDotX], _gameCoordinates._ys[t.CombatCarsDotY], 0));
                    t.SetWidth(_gameCoordinates._delta * t.CombatCarsDotWidth);
                    t.SetHeight(_gameCoordinates._delta * t.CombatCarsDotHeight);
                }

                renderer.DrawSprite(t);
            }
        }

        internal void AddTerrain(Terrain t, GameCoordinates _gameCoordinates)
        {
            t.SetPosition(new Vector(_gameCoordinates._xes[t.CombatCarsDotX], _gameCoordinates._ys[t.CombatCarsDotY], 0));
            t.SetWidth(_gameCoordinates._delta * t.CombatCarsDotWidth);
            t.SetHeight(_gameCoordinates._delta * t.CombatCarsDotHeight);
            _effects.Add(t);
        }
    }
}
