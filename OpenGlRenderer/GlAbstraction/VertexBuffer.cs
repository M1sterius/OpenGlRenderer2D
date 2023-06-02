using OpenTK.Graphics.OpenGL;

namespace OpenGlRenderer.GlAbstraction
{
    public class VertexBuffer
    {
        public readonly int GlHandle;

        public float[] Data;

        public VertexBuffer(float[] data, int size)
        {
            this.Data = data;

            GlHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, GlHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, size, data, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, GlHandle);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
