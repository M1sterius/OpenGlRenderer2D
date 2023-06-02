using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace OpenGlRenderer.GlAbstraction
{
    public class VertexArray
    {
        public readonly int GlHandle;

        private readonly List<int> _vbHandles = new List<int>();

        public VertexArray()
        {
            GlHandle = GL.GenVertexArray();
        }

        public void AddBuffer(VertexBuffer vertexBuffer, LayoutElement element, int bufferLayout)
        {   
            GL.BindVertexArray(GlHandle);
            vertexBuffer.Bind();
            _vbHandles.Add(vertexBuffer.GlHandle);

            GL.EnableVertexAttribArray(bufferLayout);
            GL.VertexAttribPointer(bufferLayout, element.Count, element.Type, element.Normalized, element.Stride, element.Offset);
            
            vertexBuffer.Unbind();
            GL.BindVertexArray(0);
        }

        public int[] GetHandlesOfAllVertexBuffers() => _vbHandles.ToArray();

        public void Bind()
        {
            GL.BindVertexArray(GlHandle);
        }
        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        public void Delete()
        {
            GL.BindVertexArray(0);
            foreach (var buffer in _vbHandles)
            {
                GL.DeleteBuffer(buffer);
            }
            GL.DeleteVertexArray(GlHandle);
        }
    }
}
