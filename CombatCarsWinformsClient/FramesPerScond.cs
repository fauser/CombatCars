using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatCarsWinformsClient
{
    public class FramesPerScond
    {
        int _numberOfFrames = 0;
        double _timePassed = 0;

        public double CurrentFPS { get; set; }
        public void Update(double elapsedTime)
        {
            _numberOfFrames++;
            _timePassed = _timePassed + elapsedTime;
            if (_timePassed > 1)
            {
                CurrentFPS = (double)_numberOfFrames / _timePassed;
                _timePassed = 0;
                _numberOfFrames = 0;
            }
        }
    }
}
