using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;

namespace CombatCarsWinformsClient
{
    public class Path
    {
        Spline _spline = new Spline();
        Tween _tween;
        double _totalTime;
        public Path(List<Vector> points, double travelTime)
        {
            foreach (Vector v in points)
            {
                _spline.AddPoint(v);
            }

            _totalTime = travelTime;

            _tween = new Tween(0, 1, travelTime);
        }

        public void UpdatePosition(double elapsedTime, Entity enemy)
        {
            _totalTime -= elapsedTime;
            _tween.Update(elapsedTime);
            Vector position = _spline.GetPositionOnLine(_tween.Value());
            double rotation = _spline.GetRotationOnLine(_tween.Value());
            enemy.Sprite.SetPosition(position);
            enemy.Sprite.SetRotation(rotation);
        }
    }
}
