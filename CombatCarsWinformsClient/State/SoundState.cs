using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;

namespace CombatCarsWinformsClient.State
{
    class SoundState : IGameObject
    {
        SoundManager _soundManager;
        double _count = 3;

        public SoundState(SoundManager soundManager)
        {
            _soundManager = soundManager;
            _soundManager.MasterVolume(0.1f);
        }

        void IGameObject.Update(double elapsedTime)
        {
            _count -= elapsedTime;
            if (_count < 0)
            {
                _count = 3;
                Sound one = _soundManager.PlaySound("engine");
                Sound two = _soundManager.PlaySound("gun");

                if (_soundManager.IsSoundPlaying(one))
                {
                    _soundManager.StopSound(one);
                }
            }
        }

        void IGameObject.Render()
        {

        }
    }
}
