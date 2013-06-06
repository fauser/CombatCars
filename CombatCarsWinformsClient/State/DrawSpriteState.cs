using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatCarsWinFormsClientEngine;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class DrawSpriteState : IGameObject
    {
        Renderer _renderer = new Renderer();
        TextureManager _textureManager;
        Sprite _testSprite = new Sprite();
        Sprite _testSprite2 = new Sprite();

        public DrawSpriteState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _testSprite.Texture = _textureManager.Get("face_alpha");
            _testSprite.SetPosition(256, 0);
            //_testSprite.SetHeight(1000);

            _testSprite2.Texture = _textureManager.Get("face_alpha");
            _testSprite2.SetPosition(-256, 0);
            _testSprite2.SetColor(new CombatCarsWinFormsClientEngine.Color(1, 0, 0, 1));
        }

        void IGameObject.Update(double elapsedTime)
        {

        }

        void IGameObject.Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            for (int i = 0; i < 1000; i++)
            {
                _renderer.DrawSprite(_testSprite);
                _renderer.DrawSprite(_testSprite2);
            }
        }
    }
}
