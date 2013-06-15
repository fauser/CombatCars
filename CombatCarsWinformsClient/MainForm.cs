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
using GenericGameEngine;
using CombatCarsWinformsClient.State;
using GenericGameEngine.Input;

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
        GenericGameEngine.Font _titleFont;
        GenericGameEngine.Font _generalFont;
        PersistentGameData _persistandData = new PersistentGameData();

        public MainForm()
        {
            InitializeComponent();
            _openGLControl.InitializeContexts();

            _input.Mouse = new Mouse(this, _openGLControl);
            _input.Keyboard = new Keyboard(_openGLControl);

            InitializeDisplay();
            InitializeSounds();
            InitializeTextures();
            InitializeGameData();
            InitializeFonts();
            InitializeGameState();

            _fastLoop = new FastLoop(GameLoop);
        }

        private void InitializeFonts()
        {
            _titleFont = new GenericGameEngine.Font(_textureManager.Get(EnumTexture.Calibri40), FontParser.Parse(@"Font\Calibri40.fnt"));
            _generalFont = new GenericGameEngine.Font(_textureManager.Get(EnumTexture.Calibri30), FontParser.Parse(@"Font\Calibri30.fnt"));
        }

        private void InitializeGameData()
        {
            LevelDescription level = new LevelDescription { Time = 30 };
            _persistandData.CurrentLevel = level;
        }

        private void InitializeSounds()
        {
            _soundManager.LoadSound("engine", @"Sound\Engine.wav");
            _soundManager.LoadSound("gun", @"Sound\Gun.wav");
        }

        private void InitializeGameState()
        {

            //_system.AddState(EnumState.Splash, new SplashScreenState(_system));
            //_system.AddState(EnumState.Title, new TitleMenuState());
            //_system.AddState(EnumState.Spritetest, new DrawSpriteState(_textureManager));
            //_system.AddState(EnumState.FPS, new FPSState(_textureManager, _generalFont));
            //_system.AddState(EnumState.Wave, new WaveFormGraphState());
            //_system.AddState(EnumState.SpecialEffect, new SpecialEffectState(_textureManager, _generalFont));
            //_system.AddState(EnumState.CircleIntersection, new CircleIntersectionState(_input));
            //_system.AddState(EnumState.RectangleIntersection, new RectangleIntersectionState(_input));
            //_system.AddState(EnumState.Tween, new TweenState(_textureManager));
            //_system.AddState(EnumState.Matrix, new MatrixState(_textureManager));
            //_system.AddState(EnumState.Sound, new SoundState(_soundManager));
            //_system.AddState(EnumState.Input, new InputState(_input));

            _system.AddState(EnumState.StartMenu, new StartMenuState(_system, _input, _generalFont, _titleFont));
            _system.AddState(EnumState.InnerGame, new InnerGameState(_system, _input, _textureManager, _persistandData, _generalFont));
            _system.AddState(EnumState.GameOver, new GameOverState(_system, _input, _persistandData, _generalFont, _titleFont));

            _system.ChangeState(EnumState.StartMenu);
        }

        private void InitializeTextures()
        {
            Il.ilInit();
            Ilu.iluInit();
            Ilut.ilutInit();
            Ilut.ilutRenderer(Ilut.ILUT_OPENGL);

            _textureManager.LoadTexture(EnumTexture.Face, @"Image\Ninja_Awesome_Smiley_by_E_rap.png");
            _textureManager.LoadTexture(EnumTexture.FaceAlpha, @"Image\364px-Ezio-transparent.png");
            _textureManager.LoadTexture(EnumTexture.PlayerShip, @"Image\viper_mark_ii.png");
            _textureManager.LoadTexture(EnumTexture.Calibri40, @"Font\Calibri40_0.tga");
            _textureManager.LoadTexture(EnumTexture.Calibri30, @"Font\Calibri30_0.tga");
            _textureManager.LoadTexture(EnumTexture.BackGround, @"Image\background.jpg");
            _textureManager.LoadTexture(EnumTexture.Planet, @"Image\Virtual Planets Earth Planet 03.png");
            _textureManager.LoadTexture(EnumTexture.Enemy, @"Image\kspaceduel.png");
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
                ControlBox = false;
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
