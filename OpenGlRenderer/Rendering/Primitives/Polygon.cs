using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGlRenderer.GlAbstraction;
using OpenTK.Mathematics;

namespace OpenGlRenderer.Rendering.Primitives
{
    public class Polygon : Primitive
    {
        private Vector2[] _vertices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="vertices">Defined in the local space of the triangle</param>
        public Polygon(Vector2 position, Color4 color, params Vector2[] vertices) : base(position, color)
        {
            _vertices = vertices;

            Setup();
        }

        private void Setup()
        {
            VertexArray?.Delete();
            VertexArray = new VertexArray();
            var vertices = new float[_vertices.Length * 2];
            for (var i = 0; i < vertices.Length; i++)
            {
                var div = Math.DivRem(i, 2, out var mod);
                if (mod == 0) vertices[i] = _vertices[div].X;
                else vertices[i] = _vertices[div].Y;
            }
            VertexArray.AddBuffer(new VertexBuffer(vertices, vertices.Length * sizeof(float)), LayoutElement.Vec2Element, 0);
            
            IndexBuffer?.Delete();
            var indices = new int[(_vertices.Length - 2) * 3];
            var currentIndex = 0;
            for (int i = 1; i < _vertices.Length - 1; i++)
            {
                indices[currentIndex] = 0;
                indices[currentIndex + 1] = i;
                indices[currentIndex + 2] = i + 1;
                currentIndex += 3;
            }
            IndexBuffer = new IndexBuffer(indices, indices.Length);
        }

        public void ChangeVertices(params Vector2[] vertices)
        {
            _vertices = vertices;
            Setup();
        }

        public Vector2[] GetVertices() => _vertices;
    }
}
