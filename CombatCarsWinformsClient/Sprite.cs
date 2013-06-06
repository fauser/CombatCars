using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatCarsWinformsClient
{
    public class Sprite
    {
        internal const int VertexAmount = 6;
        Vector[] _vertexPositions = new Vector[VertexAmount];
        Color[] _vertexColors = new Color[VertexAmount];
        Point[] _vertexUVs = new Point[VertexAmount];
        Texture _texture = new Texture();

        double _scaleX = 1;
        double _scaleY = 1;
        double _rotation = 0;
        double _positionX = 0;
        double _positionY = 0;

        public Sprite()
        {
            InitVertexPositions(new Vector(0, 0, 0), 1, 1);
            SetColor(new Color(1, 1, 1, 1));
            SetUVs(new Point(0, 0), new Point(1, 1));
        }

        public Texture Texture
        {
            get
            {
                return _texture;
            }

            set
            {
                _texture = value;
                InitVertexPositions(GetCenter(), _texture.Width, _texture.Height);
            }
        }

        public Vector[] VertexPositions { get { return _vertexPositions; } }
        public Color[] VertexColors { get { return _vertexColors; } }
        public Point[] VertexUVs { get { return _vertexUVs; } }

        private Vector GetCenter()
        {
            double halfWidth = GetWidth() / 2;
            double halfHeight = GetHeight() / 2;

            return new Vector(
                _vertexPositions[0].X + halfWidth,
                _vertexPositions[0].Y - halfHeight,
                _vertexPositions[0].Z);

        }

        private void InitVertexPositions(Vector position, double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;

            _vertexPositions[0] = new Vector(position.X - halfWidth, position.Y + halfHeight, position.Z);
            _vertexPositions[1] = new Vector(position.X + halfWidth, position.Y + halfHeight, position.Z);
            _vertexPositions[2] = new Vector(position.X - halfWidth, position.Y - halfHeight, position.Z);
            _vertexPositions[3] = new Vector(position.X + halfWidth, position.Y + halfHeight, position.Z);
            _vertexPositions[4] = new Vector(position.X + halfWidth, position.Y - halfHeight, position.Z);
            _vertexPositions[5] = new Vector(position.X - halfWidth, position.Y - halfHeight, position.Z);
        }

        public double GetWidth()
        {
            return _vertexPositions[1].X - _vertexPositions[0].X;
        }

        public double GetHeight()
        {
            return _vertexPositions[0].Y - _vertexPositions[2].Y;
        }

        public Vector GetPosition()
        {
            return GetCenter();
        }

        public void SetWidth(float width)
        {
            InitVertexPositions(GetCenter(), width, GetHeight());
        }

        public void SetHeight(float height)
        {
            InitVertexPositions(GetCenter(), GetWidth(), height);
        }

        public void SetPosition(double x, double y)
        {
            SetPosition(new Vector(x, y, 0));
        }

        public void SetPosition(Vector position)
        {
            //InitVertexPositions(position, GetWidth(), GetHeight());
            Matrix m = new Matrix();
            m.SetTranslation(new Vector(_positionX, _positionY, 0));
            ApplyMatrix(m.Inverse());
            m.SetTranslation(position);
            ApplyMatrix(m);
            _positionX = position.X;
            _positionY = position.Y;
        }

        public void SetScale(double x, double y)
        {
            double oldX = _positionX;
            double oldY = _positionY;
            SetPosition(0, 0);

            Matrix mScale = new Matrix();
            mScale.SetScale(new Vector(_scaleX, _scaleY, 1));
            mScale = mScale.Inverse();
            ApplyMatrix(mScale);
            mScale.SetScale(new Vector(x, y, 1));
            ApplyMatrix(mScale);

            SetPosition(oldX, oldY);
            _scaleX = x;
            _scaleY = y;
        }

        public void SetRotation(double rotation)
        {
            double oldX = _positionX;
            double oldY = _positionY;
            SetPosition(0, 0);

            Matrix mRot = new Matrix();
            mRot.SetRotate(new Vector(0, 0, 1), _rotation);
            ApplyMatrix(mRot.Inverse());

            mRot = new Matrix();
            mRot.SetRotate(new Vector(0, 0, 1), rotation);
            ApplyMatrix(mRot);

            SetPosition(oldX, oldY);
            _rotation = rotation;
        }

        public void SetColor(Color color)
        {
            for (int i = 0; i < Sprite.VertexAmount; i++)
            {
                _vertexColors[i] = color;
            }
        }

        public void SetUVs(Point topLeft, Point bottomRight)
        {
            _vertexUVs[0] = topLeft;
            _vertexUVs[1] = new Point(bottomRight.X, topLeft.Y);
            _vertexUVs[2] = new Point(topLeft.X, bottomRight.Y);

            _vertexUVs[3] = new Point(bottomRight.X, topLeft.Y);
            _vertexUVs[4] = bottomRight;
            _vertexUVs[5] = new Point(topLeft.X, bottomRight.Y);
        }

        public void ApplyMatrix(Matrix m)
        {
            for (int i = 0; i < VertexPositions.Length; i++)
            {
                VertexPositions[i] *= m;
            }
        }
    }
}
