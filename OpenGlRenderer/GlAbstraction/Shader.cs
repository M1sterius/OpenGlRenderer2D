using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace OpenGlRenderer.GlAbstraction
{
    public class Shader
    {
        private readonly string _path;
        private readonly ShaderType _shaderType;

        public readonly int GlHandle;

        public Shader(string path, ShaderType shaderType)
        {
            this._path = path;
            this._shaderType = shaderType;

            if (!File.Exists(_path))
            {
                Console.WriteLine($"Unable to find source file at: {_path}");
                return;
            }

            var source = File.ReadAllText(_path);
            GlHandle = GL.CreateShader(_shaderType);
            GL.ShaderSource(GlHandle, source);
            GL.CompileShader(GlHandle);

            var log = GL.GetShaderInfoLog(GlHandle);

            Console.WriteLine(log != string.Empty ? log : $"Shader at: '{_path}' compiled successfully!");
        }

        public void Delete() => GL.DeleteShader(GlHandle);

        public ShaderType GetShaderType() => _shaderType;
        public string GetSourceFile() => _path;
    }
}
