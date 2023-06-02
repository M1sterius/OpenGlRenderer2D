using OpenTK.Graphics.OpenGL;

namespace OpenGlRenderer.GlAbstraction
{
    public class IndexBuffer
    {
        public readonly int GlHandle;
        private int _count;

        public int[] Data;

        public IndexBuffer(int[] data, int count)
        {
            this.Data = data;
            this._count = count;

            GlHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, GlHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, count * sizeof(int), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public int Count => _count;

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, GlHandle);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Delete()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DeleteBuffer(GlHandle);
        }
    }
}
