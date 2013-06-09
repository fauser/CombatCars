using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CombatCarsWinFormsClientEngine.Input
{
    public class Mouse
    {
        Form _parentForm;
        Control _openGLControl;

        bool _leftClickDetected = false;
        bool _rightClickDetected = false;
        bool _middleClickDetected = false;

        public bool LeftPressed { get; set; }
        public bool RightPressed { get; set; }
        public bool MiddlePressed { get; set; }

        public bool LeftHeld { get; set; }
        public bool RightHeld { get; set; }
        public bool MiddleHeld { get; set; }

        public Point Position { get; set; }

        public Mouse(Form form, Control openGlControl)
        {
            _parentForm = form;
            _openGLControl = openGlControl;
            _openGLControl.MouseClick += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _leftClickDetected = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    _rightClickDetected = true;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    _middleClickDetected = true;
                }
            };

            _openGLControl.MouseDown += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    LeftHeld = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    RightHeld = true;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    MiddleHeld = true;
                }
            };

            _openGLControl.MouseUp += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    LeftHeld = false;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    RightHeld = false;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    MiddleHeld = false;
                }
            };

            _openGLControl.MouseLeave += delegate(object obj, EventArgs e)
            {
                LeftHeld = false;
                RightHeld = false;
                MiddleHeld = false;
            };

        }

        public void Update(double elapsedTime)
        {
            UpdateMousePosition();
            UpdateMouseButtons();
        }

        private void UpdateMouseButtons()
        {
            LeftPressed = false;
            RightPressed = false;
            MiddlePressed = false;

            if (_leftClickDetected)
            {
                LeftPressed = true;
                _leftClickDetected = false;
            }

            if (_rightClickDetected)
            {
                RightPressed = true;
                _rightClickDetected = false;
            }


            if (_middleClickDetected)
            {
                MiddlePressed = true;
                _middleClickDetected = false;
            }
        }

        private void UpdateMousePosition()
        {
            System.Drawing.Point mousePos = Cursor.Position;
            mousePos = _openGLControl.PointToClient(mousePos);

            CombatCarsWinFormsClientEngine.Point adjustedMousePoint = new CombatCarsWinFormsClientEngine.Point();
            adjustedMousePoint.X = (float)mousePos.X - ((float)_parentForm.ClientSize.Width / 2);
            adjustedMousePoint.Y = ((float)_parentForm.ClientSize.Height / 2) - (float)mousePos.Y;
            Position = adjustedMousePoint;
        }
    }
}
