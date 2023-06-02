using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenGlRenderer.GlAbstraction;
using System.Globalization;
using OpenGlRenderer.Rendering;
using OpenGlRenderer.Rendering.Primitives;
using OpenGlRenderer.Rendering.Sprites;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenGlRenderer
{
    internal class Program
    {
        private const bool ShowFpsInTitle = true;
        private static GameWindow _gameWindow;

        private static readonly List<RenderableObject> Objects = new List<RenderableObject>();

        private static readonly Camera Camera = new Camera(new Vector2(1600, 900));

        private static void Main()
        {
            var gws = new GameWindowSettings();
            var nws = new NativeWindowSettings
            {
                Size = new Vector2i(1600, 900),
                Title = "OpenGL Renderer",
                WindowBorder = WindowBorder.Fixed
            };
            _gameWindow = new GameWindow(gws, nws);

            _gameWindow.Load += OnLoad;
            _gameWindow.RenderFrame += OnRenderFrame;
            _gameWindow.UpdateFrame += OnUpdateFrame;
            _gameWindow.Resize += SetViewport;
            _gameWindow.KeyDown += delegate(KeyboardKeyEventArgs args)
            {
                if (args.Key == Keys.W)
                {
                    Camera.Position += new Vector2(0, 8);
                }
                if (args.Key == Keys.S)
                {
                    Camera.Position += new Vector2(0, -8);
                }
                if (args.Key == Keys.A)
                {
                    Camera.Position += new Vector2(-8, 0);
                }
                if (args.Key == Keys.D)
                {
                    Camera.Position += new Vector2(8, 0);
                }
            };
            _gameWindow.MouseWheel += delegate(MouseWheelEventArgs args)
            {
                if (args.OffsetY < 0)
                {
                    Camera.Scale *= 0.95f;
                }
                else
                {
                    Camera.Scale *= 1.05f;
                }
            };

            Console.WriteLine($"GPU vender: {GL.GetString(StringName.Vendor)}");
            Console.WriteLine($"GPU model: {GL.GetString(StringName.Renderer)}");
            Console.WriteLine($"GPU driver version: {GL.GetString(StringName.Version)}");
            Console.WriteLine();

            _gameWindow.Run();
        }

        private static void OnLoad()
        {
            Renderer.SetClearColor(Renderer.DefaultClearColor);

            var tex = new Texture("D:\\Important Coding Projects\\OpenGlRenderer\\OpenGlRenderer\\Assets\\Misterius3Dk.png");
            var sprite = new Sprite(new Vector2(-300, 300), tex, new Vector2(100, 100));
            Objects.Add(sprite);

            var rect = new Rectangle(new Vector2(0, 300), Color4.Blue, new Vector2(100, 100));
            Objects.Add(rect);

            var circle = new Circle(new Vector2(300, 300), Color4.BlueViolet, 50);
            Objects.Add(circle);

            var triangle = new Triangle(new Vector2(-300, 0), Color4.Beige, new Vector2(-50, 50), Vector2.Zero, new Vector2(50, 50));
            Objects.Add(triangle);

            var line = new Line(new Vector2(0, 0), new Vector2(50, 0), Color4.DarkViolet, 3);
            Objects.Add(line);
        }

        private static void OnRenderFrame(FrameEventArgs args)
        {
            Renderer.Clear();

            var cameraViewProjection = Camera.GetViewProjection();

            for (var i = 0; i < Objects.Count; i++)
            {
                var obj = Objects[i];

                if (obj is Primitive primitive)
                {
                    var program = Renderer.PrimitiveShaderProgram;
                    program.Bind();

                    var mvp = primitive.GetModel() * cameraViewProjection;
                    program.SetUniformColor4("u_Color", primitive.Color);
                    program.SetUniformMatrix4("u_MVP", true, ref mvp);

                    Renderer.Draw(primitive.GetVertexArray, primitive.GetIndexBuffer);
                }
                else if (obj is Sprite sprite)
                {
                    var program = Renderer.SpriteShaderProgram;
                    program.Bind();

                    var mvp = sprite.GetModel() * cameraViewProjection;

                    sprite.Texture.Bind();
                    program.SetUniform1("tex", 0);
                    program.SetUniformMatrix4("u_MVP", true, ref mvp);

                    Renderer.Draw(sprite.GetVertexArray, sprite.GetIndexBuffer);
                }
            }

            _gameWindow.SwapBuffers();
        }

        private static void OnUpdateFrame(FrameEventArgs args)
        {
            if (ShowFpsInTitle) _gameWindow.Title = Convert.ToString(Math.Round(1 / args.Time, 0), CultureInfo.InvariantCulture);
        }

        private static void SetViewport(ResizeEventArgs args)
        {
            GL.Viewport(0, 0, args.Width, args.Height);
        }
        private static float RandomFloat(float min, float max)
        {
            var random = new Random();
            var val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }

        //private static void GenerateRandomRenderables(int amount, Texture texture)
        //{   
        //    var rng = new Random();
            
        //    for (int i = 0; i < amount; i++)
        //    {
        //        var type = rng.Next(0, 3);

        //        if (type == 0)
        //        {
        //            var circle = new Circle(new Vector2(RandomFloat(-800, 800), RandomFloat(-450, 450)), Color4.Gold,
        //                RandomFloat(5, 50));
        //            Objects.Add(circle);
        //        }
        //        else if (type == 1)
        //        {
        //            var rect = new Rectangle(new Vector2(RandomFloat(-800, 800), RandomFloat(-450, 450)), Color4.BlueViolet,
        //                new Vector2(RandomFloat(5, 100), RandomFloat(5, 100)));
        //            Objects.Add(rect);
        //        }
        //        else if (type == 2)
        //        {   
        //            if (texture == null) continue;
        //            var sprite = new Sprite(new Vector2(RandomFloat(-800, 800), RandomFloat(-450, 450)), texture, 0.05f);
        //            Objects.Add(sprite);
        //        }
        //    }
        //}
    }
}
