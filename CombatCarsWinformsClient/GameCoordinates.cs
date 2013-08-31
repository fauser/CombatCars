using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CombatCarsWinformsClient
{
    class GameCoordinates
    {
        public List<float> _xes = new List<float>();
        public List<float> _ys = new List<float>();
        public float _delta;
        private Control _openGlControl;
        private CombatCarsGameData _gameData;

        public GameCoordinates(Control openGlControl, CombatCarsGameData gameData)
        {
            _openGlControl = openGlControl;
            _gameData = gameData;
            GenerateCoordinates();
        }

        public void GenerateCoordinates()
        {
            _delta = Math.Min((float)_openGlControl.Width / _gameData.NumberOfdotsX, (float)_openGlControl.Height / _gameData.NumberOfdotsY);
            float halfDelta = _delta / 2;

            _xes = new List<float>();
            _ys = new List<float>();

            for (int x = 0; x < _gameData.NumberOfdotsX / 2; x++)
            {
                _xes.Add(halfDelta + _delta * x);
                _xes.Add(-halfDelta - _delta * x);
            }

            _xes.Sort(delegate(float first, float second) { return first.CompareTo(second); });

            _ys.Add(0);
            for (int y = 1; y <= _gameData.NumberOfdotsY / 2; y++)
            {
                _ys.Add(_delta * y);
                _ys.Add(-_delta * y);
            }
            _ys.Sort(delegate(float first, float second) { return first.CompareTo(second); });
        }
    }
}
