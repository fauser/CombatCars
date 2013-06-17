using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;

namespace CombatCarsWinformsClient
{
    class EnemyManager
    {
        List<Enemy> _enemies = new List<Enemy>();
        TextureManager _textureManager;
        EffectsManager _effectsManager;
        int _leftBound;
        List<EnemyDef> _upComingEnemies = new List<EnemyDef>();
        BulletManager _bulletManager;
        PlayerCharacter _playerCharacter;
        public List<Enemy> EnemyList
        {
            get
            {
                return _enemies;
            }
        }

        public EnemyManager(TextureManager textureManager, EffectsManager effectsManager, BulletManager bulletManager, PlayerCharacter playerCharacter, int leftBound)
        {
            _textureManager = textureManager;
            _effectsManager = effectsManager;
            _leftBound = leftBound;
            _upComingEnemies.Add(new EnemyDef(EnumEnemyType.CannonFodder, 30));
            _upComingEnemies.Add(new EnemyDef(EnumEnemyType.CannonFodder, 29));
            _upComingEnemies.Add(new EnemyDef(EnumEnemyType.CannonFodder, 28));

            _upComingEnemies.Add(new EnemyDef(EnumEnemyType.CannonFodderLow, 20));
            _upComingEnemies.Add(new EnemyDef(EnumEnemyType.CannonFodderLow, 19));
            _upComingEnemies.Add(new EnemyDef(EnumEnemyType.CannonFodderLow, 18));

            _upComingEnemies.Add(new EnemyDef(EnumEnemyType.Straight, 17));
            _upComingEnemies.Add(new EnemyDef(EnumEnemyType.Down1, 16));
            _upComingEnemies.Add(new EnemyDef(EnumEnemyType.Up1, 15));

            _bulletManager = bulletManager;
            _playerCharacter = playerCharacter;

            _upComingEnemies.Sort(delegate(EnemyDef firstEnemy, EnemyDef secondEnenmy)
            {
                return firstEnemy.LaunchTime.CompareTo(secondEnenmy.LaunchTime);
            });
        }

        public void Update(double elapsedTime, double gameTime)
        {
            UpdateEnemySpawns(gameTime);
            _enemies.ForEach(x => x.Update(elapsedTime));
            CheckForoutOfBounds();
            RemoveDeadEnemies();
        }

        private void UpdateEnemySpawns(double gameTime)
        {
            if (_upComingEnemies.Count == 0)
            {
                return;
            }

            EnemyDef lastElement = _upComingEnemies[_upComingEnemies.Count - 1];
            if (gameTime < lastElement.LaunchTime)
            {
                _upComingEnemies.RemoveAt(_upComingEnemies.Count - 1);
                _enemies.Add(CreateEnemyFromDef(lastElement));
            }
        }

        private Enemy CreateEnemyFromDef(EnemyDef definition)
        {
            Enemy enemy = new Enemy(_textureManager, _effectsManager, _bulletManager, _playerCharacter);
            if (definition.EnemyType == EnumEnemyType.CannonFodder)
            {
                List<Vector> pathPoints = new List<Vector>();
                pathPoints.Add(new Vector(1400, 0, 0));
                pathPoints.Add(new Vector(0, 250, 0));
                pathPoints.Add(new Vector(-1400, 0, 0));

                enemy.Path = new Path(pathPoints, 10);
            }
            else if (definition.EnemyType == EnumEnemyType.CannonFodderLow)
            {
                List<Vector> pathPoints = new List<Vector>();
                pathPoints.Add(new Vector(1400, 0, 0));
                pathPoints.Add(new Vector(0, -250, 0));
                pathPoints.Add(new Vector(-1400, 0, 0));

                enemy.Path = new Path(pathPoints, 10);
            }
            else if (definition.EnemyType == EnumEnemyType.Straight)
            {
                List<Vector> pathPoints = new List<Vector>();
                pathPoints.Add(new Vector(1400, 0, 0));
                pathPoints.Add(new Vector(-1400, 0, 0));

                enemy.Path = new Path(pathPoints, 14);
            }
            else if (definition.EnemyType == EnumEnemyType.Up1)
            {
                List<Vector> pathPoints = new List<Vector>();
                pathPoints.Add(new Vector(500, -375, 0));
                pathPoints.Add(new Vector(500, 0, 0));
                pathPoints.Add(new Vector(500, 0, 0));
                pathPoints.Add(new Vector(-1400, 200, 0));

                enemy.Path = new Path(pathPoints, 10);
            }
            else if (definition.EnemyType == EnumEnemyType.Down1)
            {
                List<Vector> pathPoints = new List<Vector>();
                pathPoints.Add(new Vector(500, 375, 0));
                pathPoints.Add(new Vector(500, 0, 0));
                pathPoints.Add(new Vector(500, 0, 0));
                pathPoints.Add(new Vector(-1400, -200, 0));

                enemy.Path = new Path(pathPoints, 10);

            }
            else
            {
                System.Diagnostics.Debug.Assert(false, "Unknonw enemy type");
            }

            return enemy;
        }

        private void CheckForoutOfBounds()
        {
            foreach (Enemy enemy in _enemies)
            {
                if (enemy.GetBoundingBox().Right < _leftBound)
                {
                    enemy.Health = 0;
                }
            }
        }

        public void Render(Renderer renderer)
        {
            _enemies.ForEach(x => x.Render(renderer));
        }

        private void RemoveDeadEnemies()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                if (_enemies[i].IsDead)
                {
                    _enemies.RemoveAt(i);
                }
            }
        }
    }
}
