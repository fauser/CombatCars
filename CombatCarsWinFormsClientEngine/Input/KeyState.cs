using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatCarsWinFormsClientEngine.Input
{
    class KeyState
    {
        bool _keyPressDetected = false;
        public bool Held { get; set; }
        public bool Pressed { get; set; }

        public KeyState()
        {
            Held = false;
            Pressed = false;
        }

        internal void OnDown()
        {
            if (Held == false)
            {
                _keyPressDetected = true;
            }
            Held = true;
        }

        internal void OnUp()
        {
            Held = false;
        }

        internal void Update()
        {
            Pressed = false;
            if (_keyPressDetected)
            {
                Pressed = true;
                _keyPressDetected = false;
            }
        }
    }
}
