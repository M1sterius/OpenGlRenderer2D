using System;
using System.Linq;
using OpenGlRenderer.GlAbstraction;
using OpenTK.Mathematics;

namespace OpenGlRenderer.Rendering.Primitives
{
    public class Circle : Primitive
    {
        private float _radius;
        private readonly int _triangles;

        public float Radius
        {
            get => _radius;
            set
            {
                if (Math.Abs(_radius - value) < 0.0001f) return;
                _radius = value;
                Setup();
            }
        }

        public Circle(Vector2 position, Color4 color, float radius, int triangles = 26) : base(position, color)
        {
            _radius = radius;
            _triangles = triangles;

            Setup();
        }

        private void Setup()
        {   
            VertexArray?.Delete();
            VertexArray = new VertexArray();

            var vertices = new float[_triangles * 2];

            var angle = (float)(Math.PI * 2 / _triangles);
            for (var i = 0; i < vertices.Length; i++)
            {
                var theta = Math.DivRem(i, 2, out _) * angle;
                if (i % 2 == 0) vertices[i] = (float)(_radius * Math.Sin(theta));
                else vertices[i] = (float)(_radius * Math.Cos(theta));
            }
            vertices = new float[] { 0, 0 }.Concat(vertices).ToArray();
            VertexArray.AddBuffer(new VertexBuffer(vertices, vertices.Length * sizeof(float)), LayoutElement.Vec2Element, 0);

            IndexBuffer?.Delete();
            var indices = new int[_triangles * 3];
            for (var i = 0; i < indices.Length; i += 3)
            {
                indices[i] = 0;
            }
            for (var i = 1; i < indices.Length; i += 3)
            {
                indices[i] = Math.DivRem(i, 3, out _) + 1;
            }
            var iterator = 2;
            for (var i = 2; i < indices.Length; i += 3)
            {
                if (i + 1 == indices.Length) { indices[i] = 1; break; }

                indices[i] = iterator;
                iterator++;
            }

            IndexBuffer = new IndexBuffer(indices, indices.Length);
        }
    }
}
