using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGlRenderer.GlAbstraction;
using OpenTK.Mathematics;

namespace OpenGlRenderer.Rendering.Sprites
{
    public class Sprite : RenderableObject
    {
        public readonly Texture Texture;
        private Vector2 _size;

        public Sprite(Vector2 position, Texture texture, Vector2 size) : base(position)
        {
            Texture = texture;
            _size = size;

            VertexArray = new VertexArray();

            var vertices = new float[]
            {
                -(_size.X / 2), _size.Y / 2,
                _size.X / 2, _size.Y / 2,
                _size.X / 2, -(_size.Y / 2),
                -(_size.X / 2), -(_size.Y / 2)
            };
            VertexArray.AddBuffer(new VertexBuffer(vertices, vertices.Length * sizeof(float)), LayoutElement.Vec2Element, 0);
            var texCoords = new float[]
            {
                0f, 0f,
                1f, 0f,
                1f, 1f,
                0f, 1f
            };
            VertexArray.AddBuffer(new VertexBuffer(texCoords, texCoords.Length * sizeof(float)), LayoutElement.Vec2Element, 1);
            var indices = new int[]
            {
                0, 1, 2,
                2, 3, 0
            };
            IndexBuffer = new IndexBuffer(indices, indices.Length);
        }
    }
}
