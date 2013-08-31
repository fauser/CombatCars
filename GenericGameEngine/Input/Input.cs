using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericGameEngine.Input
{
    public class Input
    {
        public Mouse Mouse { get; set; }
        public Keyboard Keyboard { get; set; }

        public Input()
        {

        }

        public void Update(double elapsedTime)
        {
            Mouse.Update(elapsedTime);
            Keyboard.Update();
        }
    }
}
