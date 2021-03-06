﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GenericGameEngine;
using Tao.OpenGl;

namespace CombatCarsWinformsClient
{
    public class Entity
    {
        public Sprite Sprite { get { return _sprite; } }

        protected Sprite _sprite = new Sprite();

        public RectangleF GetBoundingBox()
        {
            float width = (float)(_sprite.Texture.Width * _sprite.ScaleX);
            float height = (float)(_sprite.Texture.Height * _sprite.ScaleY);
            RectangleF ds = new RectangleF((float)_sprite.GetPosition().X - width / 2,
                (float)_sprite.GetPosition().Y - height / 2,
                width, height);
            return ds;
        }

        public void Render_Debug()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            RectangleF bounds = GetBoundingBox();
            Gl.glBegin(Gl.GL_LINE_LOOP);
            {
                Gl.glColor3f(1, 0, 0);
                Gl.glVertex2f(bounds.Left, bounds.Top);
                Gl.glVertex2f(bounds.Right, bounds.Top);
                Gl.glVertex2f(bounds.Right, bounds.Bottom);
                Gl.glVertex2f(bounds.Left, bounds.Bottom);
            }
            Gl.glEnd();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }
    }
}
