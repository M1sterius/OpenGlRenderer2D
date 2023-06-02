using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace OpenGlRenderer.GlAbstraction
{
    public static class Renderer
    {
        public static Color4 DefaultClearColor = new Color4(0.3f, 0.4f, 0.5f, 1f);

        public static readonly ShaderProgram PrimitiveShaderProgram = new ShaderProgram(
            new Shader("../../../Shaders/PrimitiveVertex.glsl", ShaderType.VertexShader),
            new Shader("../../../Shaders/PrimitiveFragment.glsl", ShaderType.FragmentShader));

        public static readonly ShaderProgram SpriteShaderProgram = new ShaderProgram(
            new Shader("../../../Shaders/SpriteVertex.glsl", ShaderType.VertexShader),
            new Shader("../../../Shaders/SpriteFragment.glsl", ShaderType.FragmentShader));

        public static void SetClearColor(Color4 color)
        {
            GL.ClearColor(color);
        }

        public static void Clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public static void Draw(VertexArray va, IndexBuffer ib, ShaderProgram shaderProgram)
        {
            shaderProgram.Bind();
            va.Bind();
            ib.Bind();
            GL.DrawElements(PrimitiveType.Triangles, ib.Count, DrawElementsType.UnsignedInt, 0);
            shaderProgram.Unbind();
            va.Unbind();
            ib.Unbind();
        }

        public static void Draw(VertexArray va, IndexBuffer ib)
        {
            va.Bind();
            ib.Bind();
            GL.DrawElements(PrimitiveType.Triangles, ib.Count, DrawElementsType.UnsignedInt, 0);
            va.Unbind();
            ib.Unbind();
        }
    }
}
