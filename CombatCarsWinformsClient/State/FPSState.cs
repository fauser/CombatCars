﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class FPSState : IGameObject
    {
        TextureManager _textureManager;
        Font _font;
        Text _fpsText;
        Renderer _renderer = new Renderer();
        FramesPerScond _fps = new FramesPerScond();

        public FPSState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _font = new Font(textureManager.Get("font"), FontParser.Parse("font.fnt"));
            _fpsText = new Text("FPS:", _font);
        }

        void IGameObject.Update(double elapsedTime)
        {
            _fps.Process(elapsedTime);
        }

        void IGameObject.Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _fpsText = new Text("Fps: " + _fps.CurrentFPS.ToString("00.0"), _font);
            _renderer.DrawText(_fpsText);

            //_renderer.DrawText(new Text("The quick brown fox jumps over the lazy dog", _font, 400));

            for (int i = 0; i < 1000; i++)
            {
                _renderer.DrawText(_fpsText);
            }
        }
    }
}
