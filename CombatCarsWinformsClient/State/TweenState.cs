using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class TweenState : IGameObject
    {
        Tween _tween = new Tween(0, 256, 5, Tween.Linear);
        Sprite _sprite = new Sprite();
        Renderer _renderer = new Renderer();

        public TweenState(TextureManager textureManager)
        {
            _sprite.Texture = textureManager.Get(EnumTexture.Face);
        }

        void IGameObject.Update(double elapsedTime)
        {
            if (_tween.IsFinished() != true)
            {
                _tween.Update(elapsedTime);
                _sprite.SetWidth((float)_tween.Value());
                _sprite.SetHeight((float)_tween.Value());
            }
        }

        void IGameObject.Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);


            _renderer.DrawSprite(_sprite);
            _renderer.Render();
        }
    }
}
