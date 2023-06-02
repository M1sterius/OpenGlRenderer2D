using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace OpenGlRenderer.GlAbstraction
{
    public readonly struct LayoutElement
    {
        public readonly int Count;
        public readonly VertexAttribPointerType Type;
        public readonly bool Normalized;
        public readonly int Stride;
        public readonly int Offset;

        public static LayoutElement Vec1Element =
            new LayoutElement(1, VertexAttribPointerType.Float, false, sizeof(float), 0);
        public static LayoutElement Vec2Element =
            new LayoutElement(2, VertexAttribPointerType.Float, false, sizeof(float) * 2, 0);
        public static LayoutElement Vec3Element =
            new LayoutElement(3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);
        public static LayoutElement Vec4Element =
            new LayoutElement(4, VertexAttribPointerType.Float, false, sizeof(float) * 4, 0);

        public LayoutElement(int count, VertexAttribPointerType type, bool normalized, int stride, int offset)
        {
            this.Count = count;
            this.Type = type;
            this.Normalized = normalized;
            this.Stride = stride;
            this.Offset = offset;
        }
    }
}
