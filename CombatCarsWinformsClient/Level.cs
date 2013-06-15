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
        List<Enemy> _enemyList = new List<Enemy>();

        public Level(Input input, TextureManager textureManager, PersistentGameData gameData)
        {
            _input = input;
            _textureManager = textureManager;
            _gameData = gameData;

            _background = new ScrollingBackground(textureManager.Get(EnumTexture.BackGround));
            //_background.SetScale(2, 2);
            _background.Speed = 0.15f;

            _backgroundLayer = new ScrollingBackground(textureManager.Get(EnumTexture.Planet));
            //_background.SetScale(2, 2);
            _backgroundLayer.Speed = 0.007f;

            _enemyList.Add(new Enemy(_textureManager));
            _playerCharacter = new PlayerCharacter(_textureManager);
        }

        public void Update(double elapsedTime)
        {
            UpdateCollisions();
            _background.Update((float)elapsedTime);
            _backgroundLayer.Update((float)elapsedTime);
            _enemyList.ForEach(x => x.Update(elapsedTime));

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

            _playerCharacter.Move(controlInput * elapsedTime);
        }

        private void UpdateCollisions()
        {
            foreach (Enemy enemy in _enemyList)
            {
                if (enemy.GetBoundingBox().IntersectsWith(_playerCharacter.GetBoundingBox()))
                {
                    enemy.OnCollision(_playerCharacter);
                    _playerCharacter.OnCollision(enemy);
                }
            }
        }

        public void Render(Renderer renderer)
        {
            _background.Render(renderer);
            _backgroundLayer.Render(renderer);
            _playerCharacter.Render(renderer);
            _enemyList.ForEach(x => x.Render(renderer));
        }

        public bool HasPlayerDied()
        {
            return _playerCharacter.IsDead;
        }
    }
}
