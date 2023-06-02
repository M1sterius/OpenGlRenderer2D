using System;
using OpenGlRenderer.GlAbstraction;
using OpenTK.Mathematics;

namespace OpenGlRenderer.Rendering.Primitives
{
    public class Line : Primitive
    {
        public Vector2 Point2
        {
            get => _point2;
            set
            {
                if (_point2 == value) return;
                _point2 = value;
                Setup();
            }
        }

        public float Thickness
        {
            get => _thickness;
            set
            {
                if (Math.Abs(_thickness - value) < 0.0001f) return;
                _thickness = value;
                Setup();
            }
        }

        private Vector2 _point2;
        private float _thickness;

        public Line(Vector2 point1, Vector2 point2, Color4 color, float thickness) : base(point1, color)
        {
            this._point2 = point2;
            this._thickness = thickness;

            Setup();
        }

        private void Setup()
        {   
            VertexArray?.Delete();
            VertexArray = new VertexArray();
            var dir = (_point2 - Position).Normalized();
            var halfThickness = _thickness / 2;
            var p1 = new Vector2(-dir.Y, dir.X) * halfThickness;
            var p2 = new Vector2(dir.Y, -dir.X) * halfThickness;
            var p3 = new Vector2(-dir.Y, dir.X) * halfThickness + _point2;
            var p4 = new Vector2(dir.Y, -dir.X) * halfThickness + _point2;
            var vertices = new float[]
            {
                p1.X, p1.Y,
                p2.X, p2.Y,
                p3.X, p3.Y,
                p4.X, p4.Y
            };
            VertexArray.AddBuffer(new VertexBuffer(vertices, vertices.Length * sizeof(float)), LayoutElement.Vec2Element, 0);

            IndexBuffer?.Delete();
            var indices = new int[]
            {
                0, 1, 2,
                2, 3, 1
            };
            IndexBuffer = new IndexBuffer(indices, indices.Length);
        }
    }
}
