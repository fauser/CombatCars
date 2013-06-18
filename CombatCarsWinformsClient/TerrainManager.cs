using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;

namespace CombatCarsWinformsClient
{
    class TerrainManager
    {
        List<Sprite> _effects = new List<Sprite>();
        TextureManager _textureManager;

        public TerrainManager(TextureManager textureManager)
        {
            _textureManager = textureManager;
        }

        public void AddTerrain(EnumTexture texture, Vector position, float width, float height, double rotation)
        {
            Sprite explosion = new Sprite();
            explosion.Texture = _textureManager.Get(texture);
            explosion.SetPosition(position);
            explosion.SetWidth(width);
            explosion.SetHeight(height);
            explosion.SetRotation(rotation);
            _effects.Add(explosion);
        }

        public void Update(double elapsedTime)
        {
        }

        public void Render(Renderer renderer)
        {
            _effects.ForEach(x => renderer.DrawSprite(x));
        }
    }
}
