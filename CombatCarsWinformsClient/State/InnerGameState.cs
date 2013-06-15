using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;
using GenericGameEngine.Input;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class InnerGameState : IGameObject
    {
        Renderer _renderer = new Renderer();
        Input _input;
        StateSystem _system;
        PersistentGameData _gameData;
        Font _generalFont;

        double _gameTime;

        public InnerGameState(StateSystem system, Input input, PersistentGameData gameData, Font generalFont)
        {
            _input = input;
            _system = system;
            _gameData = gameData;
            _generalFont = generalFont;
            OnGameStart();
        }

        private void OnGameStart()
        {
            _gameTime = _gameData.CurrentLevel.Time;
        }

        void IGameObject.Update(double elapsedTime)
        {
            _gameTime -= elapsedTime;
            if (_gameTime <= 0)
            {
                OnGameStart();
                _gameData.JustWon = true;
                _system.ChangeState(EnumState.GameOver);
            }
        }

        void IGameObject.Render()
        {
            Gl.glClearColor(1, 0, 1, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.Render();
        }
    }
}
