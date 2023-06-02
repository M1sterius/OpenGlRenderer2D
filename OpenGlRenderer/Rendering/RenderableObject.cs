using System;
using OpenGlRenderer.GlAbstraction;
using OpenTK.Mathematics;

namespace OpenGlRenderer.Rendering
{
    public abstract class RenderableObject
    {
        protected VertexArray VertexArray;
        protected IndexBuffer IndexBuffer;

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                _translationMatrix = Matrix4.CreateTranslation(new Vector3(Position));
                _modelMatrix = _rotationMatrix * _scaleMatrix * _translationMatrix;
            }
        }
        public Vector2 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                _scaleMatrix = Matrix4.CreateScale(new Vector3(_scale));
                _modelMatrix = _rotationMatrix * _scaleMatrix * _translationMatrix;
            }
        }
        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                _rotationMatrix = Matrix4.CreateRotationZ(_rotation);
                _modelMatrix = _rotationMatrix * _scaleMatrix * _translationMatrix;
            }
        }

        private Vector2 _position;
        private Vector2 _scale;
        private float _rotation;

        private Matrix4 _rotationMatrix;
        private Matrix4 _scaleMatrix;
        private Matrix4 _translationMatrix;
        private Matrix4 _modelMatrix;

        protected RenderableObject(Vector2 position)
        {
            this.Position = position;
            this.Scale = Vector2.One;

            _rotationMatrix = Matrix4.CreateRotationZ(Rotation);
            _scaleMatrix = Matrix4.CreateScale(new Vector3(Scale.X, Scale.Y, 0f));
            _translationMatrix = Matrix4.CreateTranslation(new Vector3(Position.X, Position.Y, 0f));
            _modelMatrix = _rotationMatrix * _scaleMatrix * _translationMatrix;
        }

        public Matrix4 GetModel() => _modelMatrix;

        public VertexArray GetVertexArray => VertexArray;
        public IndexBuffer GetIndexBuffer => IndexBuffer;
    }
}
