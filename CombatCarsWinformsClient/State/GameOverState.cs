using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;
using GenericGameEngine.Input;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class GameOverState : IGameObject
    {
        const double _timeOut = 4;
        double _countDown = _timeOut;

        StateSystem _system;
        Input _input;
        Font _generalFont;
        Font _titleFont;
        PersistentGameData _gameData;
        Renderer _renderer = new Renderer();

        Text _titleWin;
        Text _blurbWin;

        Text _titleLose;
        Text _blurbLose;

        public GameOverState(StateSystem system, Input input, PersistentGameData data, Font generalFont, Font titleFont)
        {
            _gameData = data;
            _system = system;
            _input = input;
            _generalFont = generalFont;
            _titleFont = titleFont;

            _titleWin = new Text("Complete!", _titleFont);
            _blurbWin = new Text("Congratulations, you won!", _generalFont);
            _titleLose = new Text("Game Over!", _titleFont);
            _blurbLose = new Text("Please try again...", _generalFont);

            FormatText(_titleWin, 300);
            FormatText(_blurbWin, 200);
            FormatText(_titleLose, 300);
            FormatText(_blurbLose, 200);
        }

        private void FormatText(Text _text, int yPosition)
        {
            _text.SetPosition(-_text.Width / 2, yPosition);
            _text.SetColor(new GenericGameEngine.Color(1, 0, 0, 1));
        }

        void IGameObject.Update(double elapsedTime)
        {
            _countDown -= elapsedTime;

            if (_countDown <= 0 || _input.Keyboard.IsKeyPressed(System.Windows.Forms.Keys.Enter))
            {
                Finish();
            }
        }

        private void Finish()
        {
            _gameData.JustWon = false;
            _system.ChangeState(EnumState.StartMenu);
            _countDown = _timeOut;
        }

        void IGameObject.Render()
        {
            Gl.glClearColor(1, 1, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            if (_gameData.JustWon)
            {
                _renderer.DrawText(_titleWin);
                _renderer.DrawText(_blurbWin);
            }
            else
            {
                _renderer.DrawText(_titleLose);
                _renderer.DrawText(_blurbLose);
            }
            _renderer.Render();
        }
    }
}
