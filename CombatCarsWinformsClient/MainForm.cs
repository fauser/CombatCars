using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.DevIl;
using CombatCarsWinFormsClientEngine;
using CombatCarsWinformsClient.State;
using CombatCarsWinFormsClientEngine.Input;

namespace CombatCarsWinformsClient
{
    public partial class MainForm : Form
    {
        bool _fullscreen = false;
        FastLoop _fastLoop;
        StateSystem _system = new StateSystem();
        Input _input = new Input();
        TextureManager _textureManager = new TextureManager();
        SoundManager _soundManager = new SoundManager();
        CombatCarsWinFormsClientEngine.Font _titleFont;
        CombatCarsWinFormsClientEngine.Font _generalFont;

        public MainForm()
        {
            InitializeComponent();
            _openGLControl.InitializeContexts();

            _input.Mouse = new Mouse(this, _openGLControl);
            _input.Keyboard = new Keyboard(_openGLControl);

            InitializeDisplay();
            InitializeSounds();
            InitializeTextures();
            InitializeFonts();
            InitializeGameState();

            _fastLoop = new FastLoop(GameLoop);
        }

        private void InitializeFonts()
        {
            _titleFont = new CombatCarsWinFormsClientEngine.Font(_textureManager.Get("calibrifont"), FontParser.Parse("CalibriFont.fnt"));
            _generalFont = new CombatCarsWinFormsClientEngine.Font(_textureManager.Get("font"), FontParser.Parse("font.fnt"));
        }

        private void InitializeSounds()
        {
            _soundManager.LoadSound("engine", "Engine.wav");
            _soundManager.LoadSound("gun", "Gun.wav");
        }

        private void InitializeGameState()
        {

            //_system.AddState(EnumState.Splash, new SplashScreenState(_system));
            //_system.AddState(EnumState.Title, new TitleMenuState());
            //_system.AddState(EnumState.Spritetest, new DrawSpriteState(_textureManager));
            _system.AddState(EnumState.FPS, new FPSState(_textureManager));
            //_system.AddState(EnumState.Wave, new WaveFormGraphState());
            //_system.AddState(EnumState.SpecialEffect, new SpecialEffectState(_textureManager));
            //_system.AddState(EnumState.CircleIntersection, new CircleIntersectionState(_input));
            //_system.AddState(EnumState.RectangleIntersection, new RectangleIntersectionState(_input));
            //_system.AddState(EnumState.Tween, new TweenState(_textureManager));
            //_system.AddState(EnumState.Matrix, new MatrixState(_textureManager));
            //_system.AddState(EnumState.Sound, new SoundState(_soundManager));
            //_system.AddState(EnumState.Input, new InputState(_input));

            _system.AddState(EnumState.StartMenu, new StartMenuState(_titleFont, _generalFont, _input));
            _system.ChangeState(EnumState.StartMenu);
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
            _textureManager.LoadTexture("calibrifont", "CalibriFont_0.tga");
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
            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
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

        private void UpdateInput(double elapsedTime)
        {
            _input.Update(elapsedTime);
        }

        void GameLoop(double elapsedTime)
        {
            UpdateInput(elapsedTime);
            _system.Update(elapsedTime);
            _system.Render();
            _openGLControl.Refresh();
        }

    }
}
