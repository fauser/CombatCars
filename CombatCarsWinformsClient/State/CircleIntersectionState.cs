using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatCarsWinFormsClientEngine;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class CircleIntersectionState : IGameObject
    {
        Circle _circle = new Circle(Vector.Zero, 200);
        Input _input;

        public CircleIntersectionState(Input input)
        {
            Gl.glLineWidth(3);
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            _input = input;
        }

        void IGameObject.Update(double elapsedTime)
        {
            if (_circle.Intersects(_input.MousPosition))
            {
                _circle.Color = new Color(1, 0, 0, 1);
            }
            else
            {
                _circle.Color = new Color(1, 1, 1, 1);
            }
        }

        void IGameObject.Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _circle.Render();

            Gl.glPointSize(5);
            Gl.glBegin(Gl.GL_POINTS);
            {
                Gl.glVertex2f(_input.MousPosition.X, _input.MousPosition.Y);
            }
            Gl.glEnd();
        }
    }
}
