using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace OpenGlRenderer.Rendering.Primitives
{
    public class Quadrilateral : Primitive
    {
        public Quadrilateral(Vector2 position, Color4 color, Vector2[] vertices) : base(position, color)
        {

        }
    }
}
