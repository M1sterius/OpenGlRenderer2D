using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using StbImageSharp;

namespace OpenGlRenderer.GlAbstraction
{
    public class Texture
    {
        public readonly string Path;

        public readonly int GlHandle;
        public readonly Vector2 Size;

        public Texture(string path)
        {
            Path = path;

            var image = ImageResult.FromStream(File.OpenRead(Path), ColorComponents.RedGreenBlueAlpha);
            Size = new Vector2(image.Width, image.Height);

            GlHandle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, GlHandle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)Size.X, (int)Size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slot">The slot this texture will be bound to</param>
        public void Bind(int slot = 0)
        {
            GL.ActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + slot));
            GL.BindTexture(TextureTarget.Texture2D, GlHandle);
        }

        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}