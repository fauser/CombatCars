using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatCarsWinformsClient
{
    public interface IGameObject
    {
        void Update(double elapsedTime);
        void Render();
    }
}
