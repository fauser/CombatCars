using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class MatrixState : IGameObject
    {
        Sprite _faceSprite = new Sprite();
        Renderer _renderer = new Renderer();

        public MatrixState(TextureManager textureManager)
        {
            _faceSprite.Texture = textureManager.Get(EnumTexture.Face);
            Gl.glEnable(Gl.GL_TEXTURE_2D);

            Matrix m = new Matrix();
            m.SetRotate(new Vector(0, 0, 1), Math.PI / 5);

            Matrix mScale = new Matrix();
            mScale.SetScale(new Vector(2.0, 2.0, 2.0));

            m *= mScale;
            Vector scale = m.GetScale();

            m *= m.Inverse();

            for (int i = 0; i < _faceSprite.VertexPositions.Length; i++)
            {
                _faceSprite.VertexPositions[i] *= m;
            }
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
                _renderer.DrawSprite(_faceSprite);
                _renderer.Render();
            }
        }

        void IGameObject.OnClientSizeChanged(EventArgs e)
        {
        }
    }
}
