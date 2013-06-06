using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class SplashScreenState : IGameObject
    {
        private StateSystem _system;
        double _delayInSeconds = 3;

        public SplashScreenState(StateSystem _system)
        {
            this._system = _system;
        }

        void IGameObject.Update(double elapsedTime)
        {
            _delayInSeconds -= elapsedTime;
            if (_delayInSeconds <= 0)
            {
                _delayInSeconds = 3;
                _system.ChangeState(EnumState.Title);
            }
        }

        void IGameObject.Render()
        {
            Gl.glClearColor(1, 1, 1, 1);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glFinish();
        }
    }
}
