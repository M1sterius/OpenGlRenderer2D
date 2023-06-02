using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGlRenderer.GlAbstraction;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace OpenGlRenderer.Rendering.Primitives
{
    public class Rectangle : Primitive
    {
        private Vector2 _size;

        public Vector2 Size
        {
            get => _size;
            set
            {
                if (value == _size) return;
                _size = value;
                Setup();
            }
        }

        public Rectangle(Vector2 position, Color4 color, Vector2 size) : base(position, color)
        {
            _size = size;

            Setup();
        }

        private void Setup()
        {
            VertexArray?.Delete();
            VertexArray = new VertexArray();
            var vertices = new float[]
            {
                -(_size.X / 2), _size.Y / 2,
                _size.X / 2, _size.Y / 2,
                _size.X / 2, -(_size.Y / 2),
                -(_size.X / 2), -(_size.Y / 2)
            };
            VertexArray.AddBuffer(new VertexBuffer(vertices, vertices.Length * sizeof(float)),
                LayoutElement.Vec2Element, 0);

            IndexBuffer?.Delete();
            var indices = new int[]
            {
                0, 1, 2,
                2, 3, 0
            };
            IndexBuffer = new IndexBuffer(indices, indices.Length);
        }
    }
}
