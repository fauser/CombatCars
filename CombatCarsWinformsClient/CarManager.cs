using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;

namespace CombatCarsWinformsClient
{
    class CarManager
    {
        List<Car> _cars = new List<Car>();
        TextureManager _textureManager;
        public GameCoordinates GameCoordinates { get; set; }


        public List<Car> Cars { get { return _cars; } }

        public CarManager(TextureManager textureManager)
        {
            _textureManager = textureManager;
        }

        public void Update(double elapsedTime)
        {
            _cars.ForEach(x => x.Update(elapsedTime));
        }

        public void Render(Renderer renderer, GameCoordinates _gameCoordinates)
        {
            GameCoordinates = _gameCoordinates;
            _cars.ForEach(x => x.Render(renderer));
        }

        internal void AddTerrain(Car t)
        {
            _cars.Add(t);
        }
    }
}
