using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatCarsWinFormsClientEngine
{
    struct SoundSource
    {
        public int _bufferId;
        string _filePath;

        public SoundSource(int bufferId, string filePath)
        {
            _bufferId = bufferId;
            _filePath = filePath;
        }
    }
}
