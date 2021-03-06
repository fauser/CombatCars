﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericGameEngine
{
    public class Spline
    {
        List<Vector> _points = new List<Vector>();
        double _segmentSize = 0;

        public void AddPoint(Vector point)
        {
            _points.Add(point);
            _segmentSize = 1 / (double)_points.Count;
        }

        private int LimitPoints(int point)
        {
            if (point < 0)
            {
                return 0;
            }
            else if (point > _points.Count - 1)
            {
                return _points.Count - 1;
            }
            else
            {
                return point;
            }
        }

        public Vector GetPositionOnLine(double t)
        {
            if (_points.Count <= 1)
            {
                return new Vector(0, 0, 0);
            }

            int interval = (int)(t / _segmentSize);

            int p0 = LimitPoints(interval - 1);
            int p1 = LimitPoints(interval);
            int p2 = LimitPoints(interval + 1);
            int p3 = LimitPoints(interval + 2);

            double scaledT = (t - _segmentSize * (double)interval) / _segmentSize;
            return CalculateCatmullRom(scaledT, _points[p0], _points[p1], _points[p2], _points[p3]);
        }

        public double GetRotationOnLine(double t)
        {
            int interval = (int)(t / _segmentSize);

            int p0 = LimitPoints(interval - 1);
            int p1 = LimitPoints(interval);
            int p2 = LimitPoints(interval + 1);
            int p3 = LimitPoints(interval + 2);

            return -Math.Atan2(_points[p2].X - _points[p1].X, _points[p2].Y - _points[p1].Y); ;
        }

        private Vector CalculateCatmullRom(double t, Vector vector1, Vector vector2, Vector vector3, Vector vector4)
        {
            double t2 = t * t;
            double t3 = t2 * t;

            double b1 = 0.5 * (-t3 + 2 * t2 - t);
            double b2 = 0.5 * (3 * t3 - 5 * t2 + 2);
            double b3 = 0.5 * (-3 * t3 + 4 * t2 + t);
            double b4 = 0.5 * (t3 - t2);

            return (vector1 * b1 + vector2 * b2 + vector3 * b3 + vector4 * b4);
        }
    }
}
