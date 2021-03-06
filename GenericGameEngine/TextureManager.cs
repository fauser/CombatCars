﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.DevIl;
using Tao.OpenGl;

namespace GenericGameEngine
{
    public class TextureManager : IDisposable
    {
        Dictionary<EnumTexture, Texture> _textureDatabase = new Dictionary<EnumTexture, Texture>();

        public Texture Get(EnumTexture textureId)
        {
            return _textureDatabase[textureId];
        }

        public void LoadTexture(EnumTexture textureId, string path)
        {
            int devilId = 0;
            Il.ilGenImages(1, out devilId);
            Il.ilBindImage(devilId);

            if (!Il.ilLoadImage(path))
            {
                System.Diagnostics.Debug.Assert(false, "Could not open file, [" + path + "].");
            }

            Ilu.iluFlipImage();
            int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
            int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
            int openGLId = Ilut.ilutGLBindTexImage();

            System.Diagnostics.Debug.Assert(openGLId != 0);
            Il.ilDeleteImages(1, ref devilId);

            _textureDatabase.Add(textureId, new Texture(openGLId, width, height));
        }

        void IDisposable.Dispose()
        {
            foreach (Texture t in _textureDatabase.Values)
            {
                Gl.glDeleteTextures(1, new int[] { t.Id });
            }
        }
    }
}
