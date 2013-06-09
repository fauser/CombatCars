using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CombatCarsWinFormsClientEngine.Input
{
    public class Keyboard
    {
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        Control _openGLControl;
        public KeyPressEventHandler KeyPressEvent;

        Dictionary<Keys, KeyState> _keyStates = new Dictionary<Keys, KeyState>();

        public Keyboard(Control openGLControl)
        {
            _openGLControl = openGLControl;
            _openGLControl.KeyDown += new KeyEventHandler(OnKeyDown);
            _openGLControl.KeyUp += new KeyEventHandler(OnKeyUp);
            _openGLControl.KeyPress += new KeyPressEventHandler(OnKeyPress);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            EnsureKeyStateExists(e.KeyCode);
            _keyStates[e.KeyCode].OnDown();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            EnsureKeyStateExists(e.KeyCode);
            _keyStates[e.KeyCode].OnUp();
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (KeyPressEvent != null)
            {
                KeyPressEvent(sender, e);
            }
        }

        private void EnsureKeyStateExists(Keys key)
        {
            if (!_keyStates.Keys.Contains(key))
            {
                _keyStates.Add(key, new KeyState());
            }
        }

        public bool IsKeyPressed(Keys key)
        {
            EnsureKeyStateExists(key);
            return _keyStates[key].Pressed;
        }

        public bool IsKeyHeld(Keys key)
        {
            EnsureKeyStateExists(key);
            return _keyStates[key].Held;
        }

        public void Update()
        {
            ProcessControlKeys();
            foreach (KeyState state in _keyStates.Values)
            {
                state.Pressed = false;
                state.Update();
            }
        }

        private bool PollKeyPressed(Keys key)
        {
            return (GetAsyncKeyState((int)key) != 0);
        }

        private void ProcessControlKeys()
        {
            UpdateControllKey(Keys.Left);
            UpdateControllKey(Keys.Right);
            UpdateControllKey(Keys.Up);
            UpdateControllKey(Keys.Down);
            UpdateControllKey(Keys.LMenu);
        }

        private void UpdateControllKey(Keys keys)
        {
            if (PollKeyPressed(keys))
            {
                OnKeyDown(this, new KeyEventArgs(keys));
            }
            else
            {
                OnKeyUp(this, new KeyEventArgs(keys));
            }
        }
    }
}
