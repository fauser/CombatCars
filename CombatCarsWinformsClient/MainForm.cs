using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CombatCarsWinformsClient.GameMechanics;
using Tao.OpenGl;
using Tao.DevIl;
using CombatCarsWinformsClient.State;
using CombatCarsWinformsClient.Input;

namespace CombatCarsWinformsClient
{
    public partial class MainForm : Form
    {
        FastLoop _fastLoop;
        bool _fullscreen = false;

        StateSystem _system = new StateSystem();
        TextureManager _textureManager = new TextureManager();
        Input.Input _input = new Input.Input();

        public MainForm()
        {
            InitializeComponent();
            _openGLControl.InitializeContexts();

            InitializeDisplay();
            InitializeTextures();
            InitializeGameState();

            _fastLoop = new FastLoop(GameLoop);
        }

        private void InitializeGameState()
        {
            _system.AddState(EnumState.Splash, new SplashScreenState(_system));
            _system.AddState(EnumState.Title, new TitleMenuState());
            _system.AddState(EnumState.Spritetest, new DrawSpriteState(_textureManager));
            _system.AddState(EnumState.FPS, new FPSState(_textureManager));
            _system.AddState(EnumState.Wave, new WaveFormGraphState());
            _system.AddState(EnumState.SpecialEffect, new SpecialEffectState(_textureManager));
            _system.AddState(EnumState.CircleIntersection, new CircleIntersectionState(_input));
            _system.AddState(EnumState.RectangleIntersection, new RectangleIntersectionState(_input));
            _system.AddState(EnumState.Tween, new TweenState(_textureManager));
            _system.AddState(EnumState.Matrix, new MatrixState(_textureManager));

            _system.ChangeState(EnumState.Matrix);
        }

        private void InitializeTextures()
        {
            Il.ilInit();
            Ilu.iluInit();
            Ilut.ilutInit();
            Ilut.ilutRenderer(Ilut.ILUT_OPENGL);

            _textureManager.LoadTexture("face", "Ninja_Awesome_Smiley_by_E_rap.png");
            _textureManager.LoadTexture("face_alpha", "364px-Ezio-transparent.png");
            _textureManager.LoadTexture("font", "font_0.tga");
        }

        private void InitializeDisplay()
        {
            if (_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                ClientSize = new Size(1280, 720);
            }

            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Gl.glViewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
        }

        private void Setup2DGraphics(double width, double height)
        {
            double halfWidth = width / 2;
            double halfheight = height / 2;
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-halfWidth, halfWidth, -halfheight, halfheight, -100, 100);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

        private void UpdateInput()
        {
            System.Drawing.Point mousePos = Cursor.Position;
            mousePos = _openGLControl.PointToClient(mousePos);

            Point adjustedMousePoint = new Point();
            adjustedMousePoint.X = (float)mousePos.X - ((float)ClientSize.Width / 2);
            adjustedMousePoint.Y = ((float)ClientSize.Height / 2) - (float)mousePos.Y;

            _input.MousPosition = adjustedMousePoint;
        }

        void GameLoop(double elapsedTime)
        {
            UpdateInput();
            _system.Update(elapsedTime);
            _system.Render();
            _openGLControl.Refresh();
        }

    }
}
