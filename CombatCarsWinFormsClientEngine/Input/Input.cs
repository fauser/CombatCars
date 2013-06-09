using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatCarsWinFormsClientEngine.Input
{
    public class Input
    {
        public CombatCarsWinFormsClientEngine.Point MousePosition { get; set; }
        public Mouse Mouse { get; set; }

        public Input()
        {

        }

        public void Update(double elapsedTime)
        {
            Mouse.Update(elapsedTime);
        }
    }
}
