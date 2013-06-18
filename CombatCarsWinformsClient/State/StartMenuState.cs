using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;
using GenericGameEngine.Input;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class StartMenuState : IGameObject
    {
        Renderer _renderer = new Renderer();
        Text _title;
        GenericGameEngine.Font _generalFont;
        Input _input;
        StateSystem _system;
        VerticalMenu _menu;

        public StartMenuState(StateSystem system, Input input, GenericGameEngine.Font generalFont, GenericGameEngine.Font titleFont)
        {
            _system = system;
            _input = input;
            _generalFont = generalFont;
            InitializeMenu();

            _title = new Text("Shooter", titleFont);
            _title.SetColor(new GenericGameEngine.Color(0, 0, 0, 1));
            _title.SetPosition(-_title.Width / 2, 300);
        }

        private void InitializeMenu()
        {
            _menu = new VerticalMenu(1, 150, _input);
            Button startGame = new Button(delegate(object o, EventArgs e)
            {
                _system.ChangeState(EnumState.InnerGame);
            }, new Text("Start", _generalFont));

            Button exitGame = new Button(delegate(object o, EventArgs e)
            {
                System.Windows.Forms.Application.Exit();
            }, new Text("Exit", _generalFont));

            _menu.AddButton(startGame);
            _menu.AddButton(exitGame);
        }

        void IGameObject.Update(double elapsedTime)
        {
            _menu.HandleInput();
        }

        void IGameObject.Render()
        {
            Gl.glClearColor(1, 1, 1, 0);

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawText(_title);
            _menu.Render(_renderer);
            _renderer.Render();
        }

        void IGameObject.OnClientSizeChanged(EventArgs e)
        {
        }
    }
}
