using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatCarsWinFormsClientEngine;
using CombatCarsWinFormsClientEngine.Input;
using Tao.OpenGl;

namespace CombatCarsWinformsClient.State
{
    class StartMenuState : IGameObject
    {
        Renderer _renderer = new Renderer();
        Text _title;
        CombatCarsWinFormsClientEngine.Font _generalFont;
        Input _input;
        VerticalMenu _menu;

        public StartMenuState(CombatCarsWinFormsClientEngine.Font titleFont, CombatCarsWinFormsClientEngine.Font generalFont, Input input)
        {
            _input = input;
            _generalFont = generalFont;
            InitializeMenu();

            _title = new Text("Shooter", titleFont);
            _title.SetColor(new CombatCarsWinFormsClientEngine.Color(1, 1, 1, 1));
            _title.SetPosition(-_title.Width / 2, 300);
        }

        private void InitializeMenu()
        {
            _menu = new VerticalMenu(1, 150, _input);
            Button startGame = new Button(delegate(object o, EventArgs e)
            {

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
            Gl.glClearColor(0, 0, 0, 0);

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawText(_title);
            _menu.Render(_renderer);
            _renderer.Render();
        }
    }
}
