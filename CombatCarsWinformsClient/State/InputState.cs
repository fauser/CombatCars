using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatCarsWinFormsClientEngine;
using CombatCarsWinFormsClientEngine.Input;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class InputState : IGameObject
    {
        Input _input;

        bool _leftToggle = false;
        bool _rightToggle = false;
        bool _middleToggle = false;


        public InputState(Input input)
        {
            _input = input;
        }

        private void DrawButtonPoint(bool held, int xPos, int yPos)
        {
            if (held)
            {
                Gl.glColor3f(0, 1, 0);
            }
            else
            {
                Gl.glColor3f(0, 0, 0);
            }
            Gl.glVertex2f(xPos, yPos);
        }

        void IGameObject.Update(double elapsedTime)
        {

        }

        void IGameObject.Render()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glClearColor(1, 1, 1, 0);

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glPointSize(10.0f);

            Gl.glBegin(Gl.GL_POINTS);
            {
                Gl.glColor3f(1, 0, 0);
                Gl.glVertex2f(_input.Mouse.Position.X, _input.Mouse.Position.Y);

                if (_input.Mouse.LeftPressed)
                {
                    _leftToggle = !_leftToggle;
                }

                if (_input.Mouse.RightPressed)
                {
                    _rightToggle = !_rightToggle;
                }

                if (_input.Mouse.MiddlePressed)
                {
                    _middleToggle = !_middleToggle;
                }

                DrawButtonPoint(_leftToggle, -100, 0);
                DrawButtonPoint(_rightToggle, 100, 0);
                DrawButtonPoint(_middleToggle, 0, 0);

                DrawButtonPoint(_input.Keyboard.IsKeyHeld(System.Windows.Forms.Keys.Left), -100, 20);
                DrawButtonPoint(_input.Keyboard.IsKeyHeld(System.Windows.Forms.Keys.Right), 100, 20);
                DrawButtonPoint(_input.Keyboard.IsKeyHeld(System.Windows.Forms.Keys.A), 0, 20);
            }
            Gl.glEnd();
        }
    }
}
