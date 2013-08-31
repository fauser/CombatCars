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
        TerrainManager _terrainManager;
        GameCoordinates _gameCoordinates;
        CarManager _carManager;
        int _timesClicked = 0;
        List<Vector> _pathPoints;

        public CombatCarsState(StateSystem system, Input input, TextureManager textureManager, Control openGlControl, CombatCarsGameData gameData)
        {
            _system = system;
            _input = input;
            _textrureManager = textureManager;
            _openGLControl = openGlControl;
            _gameData = gameData;

            _gameCoordinates = new GameCoordinates(openGlControl, gameData);

            _terrainManager = new TerrainManager(textureManager) { GameCoordinates = _gameCoordinates };

            _terrainManager.AddTerrain(new Terrain(_textrureManager.Get(EnumTexture.House1x2_1), 1, 2, 7, 7), _gameCoordinates);
            _terrainManager.AddTerrain(new Terrain(_textrureManager.Get(EnumTexture.House2x1_1), 2, 1, 10, 9), _gameCoordinates);
            _terrainManager.AddTerrain(new Terrain(_textrureManager.Get(EnumTexture.Forest1x1), 1, 1, 4, 4), _gameCoordinates);

            _carManager = new CarManager(_textrureManager) { GameCoordinates = _gameCoordinates };

            Car c = new Car(_textrureManager.Get(EnumTexture.Car1), 3, 8);

            double ds = Math.Max(c.Sprite.GetWidth(), _gameCoordinates._delta) - Math.Min(c.Sprite.GetWidth(), _gameCoordinates._delta);




            c.Sprite.SetScale(_gameCoordinates._delta / c.Sprite.GetWidth() / 2, _gameCoordinates._delta / c.Sprite.GetHeight());

            List<Vector> pathPoints = new List<Vector>();
            pathPoints.Add(new Vector(_gameCoordinates._xes[0], _gameCoordinates._ys[0], 0));
            pathPoints.Add(new Vector(_gameCoordinates._xes[1], _gameCoordinates._ys[1], 0));
            pathPoints.Add(new Vector(_gameCoordinates._xes[0], _gameCoordinates._ys[2], 0));

            pathPoints.Add(new Vector(_gameCoordinates._xes[4], _gameCoordinates._ys[4], 0));




            c.Path = new Path(pathPoints, 10);

            c.Sprite.SetRotation(Math.PI * 0.5f);

            _carManager.AddTerrain(c);
        }

        void IGameObject.Update(double elapsedTime)
        {
            if (_input.Keyboard.IsKeyPressed(System.Windows.Forms.Keys.Escape))
            {
                System.Windows.Forms.Application.Exit();
            }

            if (_input.Mouse.LeftPressed)
            {
                _timesClicked++;
                if (_timesClicked == 1)
                {
                    _pathPoints = new List<Vector>();
                    _pathPoints.Add(new Vector(_carManager.Cars[0].Sprite.GetPosition().X, _carManager.Cars[0].Sprite.GetPosition().Y, 0));
                    _pathPoints.Add(new Vector(_input.Mouse.Position.X, _input.Mouse.Position.Y, 0));
                }
                else if (_timesClicked == 2)
                {
                    _pathPoints.Add(new Vector(_input.Mouse.Position.X, _input.Mouse.Position.Y, 0));
                }
                else
                {
                    _timesClicked = 0;
                    _pathPoints.Add(new Vector(_input.Mouse.Position.X, _input.Mouse.Position.Y, 0));
                    _carManager.Cars[0].Path = new Path(_pathPoints, 10);
                }
            }


            _terrainManager.Update(elapsedTime);
            _carManager.Update(elapsedTime);
        }

        void IGameObject.Render()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glClearColor((float)30 / 255, (float)144 / 255, (float)255 / 255, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            DrawGameBoardDots();
            DrawGameBoardStraightLines();

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            _terrainManager.Render(_renderer, _gameCoordinates);
            _carManager.Render(_renderer, _gameCoordinates);

            _renderer.Render();
        }

        private void DrawGameBoardStraightLines()
        {
            Gl.glLineWidth(2.0f);
            Gl.glBegin(Gl.GL_LINES);
            {
                for (int x = 0; x < _gameCoordinates._xes.Count; x++)
                {
                    Gl.glVertex2f(_gameCoordinates._xes[x], _gameCoordinates._ys[0] - _gameCoordinates._delta);
                    Gl.glVertex2f(_gameCoordinates._xes[x], _gameCoordinates._ys[_gameData.NumberOfdotsY - 1] + _gameCoordinates._delta);
                }

                for (int y = 0; y < _gameCoordinates._ys.Count; y++)
                {
                    Gl.glVertex2f(_gameCoordinates._xes[0] - _gameCoordinates._delta, _gameCoordinates._ys[y]);
                    Gl.glVertex2f(_gameCoordinates._xes[_gameData.NumberOfdotsX - 1] + _gameCoordinates._delta, _gameCoordinates._ys[y]);
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
                for (int x = 0; x < _gameCoordinates._xes.Count; x++)
                {
                    for (int y = 0; y < _gameCoordinates._ys.Count; y++)
                    {
                        Gl.glVertex2f(_gameCoordinates._xes[x], _gameCoordinates._ys[y]);
                    }
                }
            }
            Gl.glEnd();
        }

        void IGameObject.OnClientSizeChanged(EventArgs e)
        {
            _gameCoordinates.GenerateCoordinates();
            _terrainManager.GameCoordinates = _gameCoordinates;
            _carManager.GameCoordinates = _gameCoordinates;
        }
    }
}
