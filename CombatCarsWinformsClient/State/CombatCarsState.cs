using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GenericGameEngine;
using GenericGameEngine.Input;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class CombatCarsState : IGameObject
    {
        Renderer _renderer = new Renderer();
        StateSystem _system;
        Input _input;
        TextureManager _textrureManager;
        CombatCarsGameData _gameData;
        Control _openGLControl;
        List<float> _xes = new List<float>();
        List<float> _ys = new List<float>();
        float _delta;

        TerrainManager _terrainManager;

        public CombatCarsState(StateSystem system, Input input, TextureManager textureManager, Control openGlControl, CombatCarsGameData gameData)
        {
            _system = system;
            _input = input;
            _textrureManager = textureManager;
            _openGLControl = openGlControl;
            _gameData = gameData;
            GenerateCoordinates();
            _terrainManager = new TerrainManager(textureManager);

            _terrainManager.AddTerrain(EnumTexture.Forest1x1, new Vector(_xes[0], _ys[2], 0), _delta, _delta, 0);
            _terrainManager.AddTerrain(EnumTexture.Forest1x1, new Vector(_xes[5], _ys[5], 0), _delta, _delta, 0);
            _terrainManager.AddTerrain(EnumTexture.House1x2_1, new Vector(_xes[7], _ys[7], 0), _delta, _delta * 2, Math.PI * 0.5);
        }

        void IGameObject.Update(double elapsedTime)
        {
            if (_input.Keyboard.IsKeyPressed(System.Windows.Forms.Keys.Escape))
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        void IGameObject.Render()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glClearColor((float)30 / 255, (float)144 / 255, (float)255 / 255, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            DrawGameBoardDots();
            DrawGameBoardStraightLines();

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            _terrainManager.Render(_renderer);
            _renderer.Render();
        }

        private void DrawGameBoardStraightLines()
        {
            Gl.glLineWidth(2.0f);
            Gl.glBegin(Gl.GL_LINES);
            {
                for (int x = 0; x < _xes.Count; x++)
                {
                    Gl.glVertex2f(_xes[x], _ys[0] - _delta);
                    Gl.glVertex2f(_xes[x], _ys[_gameData.NumberOfdotsY - 1] + _delta);
                }

                for (int y = 0; y < _ys.Count; y++)
                {
                    Gl.glVertex2f(_xes[0] - _delta, _ys[y]);
                    Gl.glVertex2f(_xes[_gameData.NumberOfdotsX - 1] + _delta, _ys[y]);
                }
            }
            Gl.glEnd();
        }

        private void DrawGameBoardDots()
        {
            Gl.glPointSize(5.0f);

            Gl.glBegin(Gl.GL_POINTS);
            {
                if (_input.Mouse.Position.X < 0 && _input.Mouse.Position.Y < 0)
                    Gl.glColor3f(1, 0, 0);
                else if (_input.Mouse.Position.X > 0 && _input.Mouse.Position.Y < 0)
                    Gl.glColor3f(0, 1, 0);
                else if (_input.Mouse.Position.X < 0 && _input.Mouse.Position.Y > 0)
                    Gl.glColor3f(0, 0, 1);
                else if (_input.Mouse.Position.X > 0 && _input.Mouse.Position.Y > 0)
                    Gl.glColor3f(1, 1, 1);

                Gl.glVertex2f(_input.Mouse.Position.X, _input.Mouse.Position.Y);

                Gl.glColor3f(0, 1, 0);
                for (int x = 0; x < _xes.Count; x++)
                {
                    for (int y = 0; y < _ys.Count; y++)
                    {
                        Gl.glVertex2f(_xes[x], _ys[y]);
                    }
                }
            }
            Gl.glEnd();
        }

        void IGameObject.OnClientSizeChanged(EventArgs e)
        {
            GenerateCoordinates();
        }

        private void GenerateCoordinates()
        {
            _delta = Math.Min((float)_openGLControl.Width / _gameData.NumberOfdotsX, (float)_openGLControl.Height / _gameData.NumberOfdotsY);
            float halfDelta = _delta / 2;

            _xes = new List<float>();
            _ys = new List<float>();

            for (int x = 0; x < _gameData.NumberOfdotsX / 2; x++)
            {
                _xes.Add(halfDelta + _delta * x);
                _xes.Add(-halfDelta - _delta * x);
            }

            _xes.Sort(delegate(float first, float second) { return first.CompareTo(second); });

            _ys.Add(0);
            for (int y = 1; y <= _gameData.NumberOfdotsY / 2; y++)
            {
                _ys.Add(_delta * y);
                _ys.Add(-_delta * y);
            }
            _ys.Sort(delegate(float first, float second) { return first.CompareTo(second); });
        }
    }
}
