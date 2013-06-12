using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatCarsWinFormsClientEngine.Input;
using System.Windows.Forms;

namespace CombatCarsWinFormsClientEngine
{
    public class VerticalMenu
    {
        Vector _position = new Vector();
        Input.Input _input;
        List<Button> _buttons = new List<Button>();
        int _currentFocus = 0;

        public double Spacing { get; set; }

        public VerticalMenu(double x, double y, Input.Input input)
        {
            _input = input;
            _position = new Vector(x, y, 0);
            Spacing = 50;
        }

        public void AddButton(Button button)
        {
            double _currentY = _position.Y;

            if (_buttons.Count != 0)
            {
                _currentY = _buttons.Last().Position.Y;
                _currentY -= Spacing;
            }
            else
            {
                button.OnGainFocus();
            }

            button.Position = new Vector(_position.X, _currentY, 0);
            _buttons.Add(button);
        }

        public void HandleInput()
        {
            if (_input.Keyboard.IsKeyPressed(Keys.Down))
            {
                OnDown();
            }
            else if (_input.Keyboard.IsKeyPressed(Keys.Up))
            {
                OnUp();
            }
            else if (_input.Keyboard.IsKeyPressed(Keys.Enter))
            {
                OnButtonPress();
            }
        }

        private void OnButtonPress()
        {
            _buttons[_currentFocus].OnPress();
        }

        private void OnUp()
        {
            int oldFocus = _currentFocus;
            _currentFocus++;
            if (_currentFocus == _buttons.Count)
            {
                _currentFocus = 0;
            }
            ChangeFocus(oldFocus, _currentFocus);
        }

        private void OnDown()
        {
            int oldFocus = _currentFocus;
            _currentFocus--;
            if (_currentFocus == -1)
            {
                _currentFocus = (_buttons.Count - 1);
            }
            ChangeFocus(oldFocus, _currentFocus);
        }

        private void ChangeFocus(int from, int to)
        {
            if (from != to)
            {
                _buttons[from].OnLoseFocus();
                _buttons[to].OnGainFocus();
            }
        }

        public void Render(Renderer renderer)
        {
            _buttons.ForEach(x => x.Render(renderer));
        }
    }
}
