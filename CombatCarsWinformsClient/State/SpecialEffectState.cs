using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatCarsWinFormsClientEngine;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class SpecialEffectState : IGameObject
    {
        Font _font;
        Text _text;
        Renderer _renderer = new Renderer();
        double _totalTime = 0;

        public SpecialEffectState(TextureManager manager)
        {
            _font = new Font(manager.Get("font"), FontParser.Parse("font.fnt"));
            _text = new Text("Hello", _font);
        }


        void IGameObject.Update(double elapsedTime)
        {
            //RedPulsatingText();
            //RainbowPulsatingText();
            //MoveTextAsCircle();
            WaveFormText();

            _totalTime += elapsedTime;
        }

        private void WaveFormText()
        {
            double frequency = 7;
            int xAdvance = 0;
            foreach (CharacterSprite cs in _text.CharacterSprites)
            {
                Vector position = cs.Sprite.GetPosition();
                position.Y = 0 + Math.Sin((_totalTime * xAdvance) * frequency) * 25;
                cs.Sprite.SetPosition(position);
                xAdvance++;
            }
        }

        private void MoveTextAsCircle()
        {
            double frequency = 7;
            double _wavyNumberX = Math.Sin(_totalTime * frequency) * 15;
            double _wavyNumberY = Math.Cos(_totalTime * frequency) * 15;
            _text.SetPosition(_wavyNumberX, _wavyNumberY);
        }

        private void RainbowPulsatingText()
        {
            double frequency = 7;
            float _wavyNumberR = (float)Math.Sin(_totalTime * frequency);
            float _wavyNumberG = (float)Math.Cos(_totalTime * frequency);
            float _wavyNumberB = (float)Math.Sin(_totalTime + 0.25f * frequency);

            _wavyNumberR = 0.5f + _wavyNumberR * 0.5f;
            _wavyNumberG = 0.5f + _wavyNumberG * 0.5f;
            _wavyNumberB = 0.5f + _wavyNumberB * 0.5f;

            _text.SetColor(new CombatCarsWinFormsClientEngine.Color(_wavyNumberR, _wavyNumberG, _wavyNumberB, 1));
        }

        private void RedPulsatingText()
        {
            double frequency = 7;
            float _wavyNumber = (float)Math.Sin(_totalTime * frequency);
            _wavyNumber = 0.5f + _wavyNumber * 0.5f;
            _text.SetColor(new CombatCarsWinFormsClientEngine.Color(1, 0, 0, _wavyNumber));
        }

        void IGameObject.Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawText(_text);
            _renderer.Render();

        }
    }
}
