using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericGameEngine
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
