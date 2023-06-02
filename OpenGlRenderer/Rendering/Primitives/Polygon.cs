using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace OpenGlRenderer.Rendering.Primitives
{
    public class Polygon : Primitive
    {
        private Vector2[] _vertices;

        public Polygon(Vector2 position, Color4 color, params Vector2[] vertices) : base(position, color)
        {
            _vertices = vertices;
        }
    }
}
