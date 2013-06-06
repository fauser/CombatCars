using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatCarsWinFormsClientEngine;
using Tao.OpenGl;

namespace CombatCarsWinformsClient
{
    public class Circle
    {
        Vector Position { get; set; }
        double Radius { get; set; }
        public Color Color { get; set; }

        public Circle()
            : this(Vector.Zero, 1)
        {
        }

        public Circle(Vector position, double radius)
        {
            Position = position;
            Radius = radius;
            Color = new Color(1, 1, 1, 1);
        }

        public void Render()
        {
            int vertexAmount = 20;
            double twoPI = 2.0 * Math.PI;

            Gl.glColor3f(Color.Red, Color.Green, Color.Blue);

            Gl.glBegin(Gl.GL_LINE_LOOP);
            {
                for (int i = 0; i <= vertexAmount; i++)
                {
                    double xPos = Position.X + Radius * Math.Cos(i * twoPI / vertexAmount);
                    double yPos = Position.Y + Radius * Math.Sin(i * twoPI / vertexAmount);
                    Gl.glVertex2d(xPos, yPos);
                }
            }
            Gl.glEnd();
        }

        public bool Intersects(CombatCarsWinFormsClientEngine.Point point)
        {
            Vector vPoint = new Vector(point.X, point.Y, 0);
            Vector vFromCircleToPoint = Position - vPoint;
            double distance = vFromCircleToPoint.Length();

            if (distance > Radius)
            {
                return false;
            }

            return true;
        }
    }
}
