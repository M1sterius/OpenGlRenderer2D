using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace OpenGlRenderer.GlAbstraction
{
    public class ShaderProgram
    {
        public readonly int GlHandle;

        private readonly Dictionary<string, int> _uniformsCache = new Dictionary<string, int>();

        public ShaderProgram(params Shader[] shaders)
        {
            GlHandle = GL.CreateProgram();
            foreach (var shader in shaders) GL.AttachShader(GlHandle, shader.GlHandle);
            GL.LinkProgram(GlHandle);
            foreach (var shader in shaders) shader.Delete();
            GL.ValidateProgram(GlHandle);

            var log = GL.GetProgramInfoLog(GlHandle);
            Console.WriteLine(log != string.Empty ? log : $"Shader program compiled successfully!");
        }

        public void Bind() => GL.UseProgram(GlHandle);
        public void Unbind() => GL.UseProgram(0);

        public int FindUniform(string name)
        {
            if (_uniformsCache.TryGetValue(name, out var uniform)) return uniform;
            else
            {
                var location = GL.GetUniformLocation(this.GlHandle, name);
                if (location != -1)
                {
                    _uniformsCache.Add(name, location);
                    return location;
                }
            }
            Console.WriteLine($"Can not find the uniform named: {name}");
            return -1;
        }

        public void SetUniformColor4(string name, Color4 data)
        {
            var location = FindUniform(name);
            if (location == -1) return;
            GL.Uniform4(location, data);
        }

        public void SetUniformMatrix4(string name, bool transpose, ref Matrix4 data)
        {
            var location = FindUniform(name);
            if (location == -1) return;
            GL.UniformMatrix4(location, transpose, ref data);
        }

        public void SetUniform1(string name, int value)
        {
            var location = FindUniform(name);
            if (location == -1) return;
            GL.Uniform1(location, value);
        }
    }
}
