using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatCarsWinformsClient.State
{
    class RectangleIntersectionState : IGameObject
    {
        Input.Input _input;
        Rectangle _rectangle = new Rectangle(new Vector(0, 0, 0), new Vector(200, 200, 0));

        public RectangleIntersectionState(Input.Input input)
        {
            _input = input;
        }

        void IGameObject.Update(double elapsedTime)
        {
            if (_rectangle.Intersects(_input.MousPosition))
            {
                _rectangle.Color = new Color(1, 0, 0, 1);
            }
            else
            {
                _rectangle.Color = new Color(1, 1, 1, 1);
            }
        }

        void IGameObject.Render()
        {
            _rectangle.Render();
        }
    }
}
