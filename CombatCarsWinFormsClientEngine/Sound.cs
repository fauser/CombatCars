using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericGameEngine
{
    public class Sound
    {
        public int Channel { get; set; }

        public bool FailedToPLay
        {
            get { return (Channel == -1); }
        }

        public Sound(int channel)
        {
            Channel = channel;
        }
    }
}
