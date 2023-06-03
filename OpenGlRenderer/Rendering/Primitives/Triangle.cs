using OpenGlRenderer.GlAbstraction;
using OpenTK.Mathematics;

namespace OpenGlRenderer.Rendering.Primitives
{
    public class Triangle : Primitive
    {
        private Vector2 _vertex1;
        private Vector2 _vertex2;
        private Vector2 _vertex3;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="vertex1">Defined in the local space of the triangle</param>
        /// <param name="vertex2">Defined in the local space of the triangle</param>
        /// <param name="vertex3">Defined in the local space of the triangle</param>
        public Triangle(Vector2 position, Color4 color, Vector2 vertex1, Vector2 vertex2, Vector2 vertex3) : base(position, color)
        {
            _vertex1 = vertex1;
            _vertex2 = vertex2;
            _vertex3 = vertex3;

            Setup();
        }

        private void Setup()
        {   
            VertexArray?.Delete();
            VertexArray = new VertexArray();
            var vertices = new float[]
            {
                _vertex1.X, _vertex1.Y,
                _vertex2.X, _vertex2.Y,
                _vertex3.X, _vertex3.Y
            };
            VertexArray.AddBuffer(new VertexBuffer(vertices, vertices.Length * sizeof(float)), LayoutElement.Vec2Element, 0);
            
            IndexBuffer?.Delete();
            var indices = new int[] { 0, 1, 2 };
            IndexBuffer = new IndexBuffer(indices, indices.Length);
        }

        /// <summary>
        /// Vertices are defined in triangle's local space
        /// </summary>
        /// <param name="newVertex1"></param>
        /// <param name="newVertex2"></param>
        /// <param name="newVertex3"></param>
        public void ChangeVertices(Vector2 newVertex1, Vector2 newVertex2, Vector2 newVertex3)
        {
            if (newVertex1 == _vertex1 && newVertex2 == _vertex2 && newVertex3 == _vertex3) return;
            _vertex1 = newVertex1;
            _vertex2 = newVertex2;
            _vertex3 = newVertex3;
            Setup();
        }

        public Vector2[] GetVertices() => new Vector2[] { _vertex1, _vertex2, _vertex3 };
    }
}
