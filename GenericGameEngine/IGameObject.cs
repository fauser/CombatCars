using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericGameEngine
{
    public interface IGameObject
    {
        void Update(double elapsedTime);
        void Render();
        void OnClientSizeChanged(EventArgs e);
    }
}
