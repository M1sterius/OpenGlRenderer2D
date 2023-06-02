using OpenTK.Mathematics;

namespace OpenGlRenderer.Rendering.Primitives
{
    public abstract class Primitive : RenderableObject
    {
        public Color4 Color;

        protected Primitive(Vector2 position, Color4 color) : base(position)
        {
            this.Color = color;
        }
    }
}
