using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GenericGameEngine;
using GenericGameEngine.Input;


namespace CombatCarsWinformsClient
{
    class Level
    {
        Input _input;
        PersistentGameData _gameData;
        PlayerCharacter _playerCharacter;
        TextureManager _textureManager;
        ScrollingBackground _background;
        ScrollingBackground _backgroundLayer;
        BulletManager _bulletManager = new BulletManager(new System.Drawing.RectangleF(-1300 / 2, -750 / 2, 1300, 750));
        EffectsManager _effectsManager;
        EnemyManager _enemyManager;

        public Level(Input input, TextureManager textureManager, PersistentGameData gameData)
        {
            _input = input;
            _textureManager = textureManager;
            _gameData = gameData;

            _background = new ScrollingBackground(textureManager.Get(EnumTexture.BackGround));
            _background.SetScale(2, 2);
            _background.Speed = 0.15f;

            _backgroundLayer = new ScrollingBackground(textureManager.Get(EnumTexture.Planet));
            //_background.SetScale(2, 2);
            _backgroundLayer.Speed = 0.007f;

            _playerCharacter = new PlayerCharacter(_textureManager, _bulletManager);

            _effectsManager = new EffectsManager(_textureManager);
            _enemyManager = new EnemyManager(_textureManager, _effectsManager, _bulletManager, _playerCharacter, -1300);
        }

        public void Update(double elapsedTime, double gameTime)
        {
            _effectsManager.Update(elapsedTime);
            UpdateCollisions();
            _bulletManager.Update(elapsedTime);
            _background.Update((float)elapsedTime);
            _backgroundLayer.Update((float)elapsedTime);
            _enemyManager.Update(elapsedTime, gameTime);
            _playerCharacter.Update(elapsedTime);
            UpdateInput(elapsedTime);
        }

        private void UpdateInput(double elapsedTime)
        {
            Vector controlInput = new Vector(0, 0, 0);

            if (_input.Keyboard.IsKeyHeld(Keys.Left))
            {
                controlInput.X = -1;
            }

            if (_input.Keyboard.IsKeyHeld(Keys.Right))
            {
                controlInput.X = 1;
            }

            if (_input.Keyboard.IsKeyHeld(Keys.Up))
            {
                controlInput.Y = 1;
            }

            if (_input.Keyboard.IsKeyHeld(Keys.Down))
            {
                controlInput.Y = -1;
            }

            if (_input.Keyboard.IsKeyHeld(Keys.Space))
            {
                _playerCharacter.Fire();
            }

            _playerCharacter.Move(controlInput * elapsedTime);
        }

        private void UpdateCollisions()
        {
            _bulletManager.UpdatePlayerCollisions(_playerCharacter);

            foreach (Enemy enemy in _enemyManager.EnemyList)
            {
                if (enemy.GetBoundingBox().IntersectsWith(_playerCharacter.GetBoundingBox()))
                {
                    enemy.OnCollision(_playerCharacter);
                    _playerCharacter.OnCollision(enemy);
                }

                _bulletManager.UpdateEnemyCollisions(enemy);
            }
        }

        public void Render(Renderer renderer)
        {
            _background.Render(renderer);
            _backgroundLayer.Render(renderer);

            _enemyManager.Render(renderer);
            //renderer.Render();
            _playerCharacter.Render(renderer);
            //renderer.Render();
            _bulletManager.Render(renderer);
            _effectsManager.Render(renderer);
            renderer.Render();

        }

        public bool HasPlayerDied()
        {
            return _playerCharacter.IsDead;
        }
    }
}
