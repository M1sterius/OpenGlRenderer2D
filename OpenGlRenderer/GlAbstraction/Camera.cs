using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace OpenGlRenderer.GlAbstraction
{
    public class Camera
    {
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                _translationMatrix = Matrix4.CreateTranslation(new Vector3(-_position));
                _viewMatrix = _translationMatrix * _rotationMatrix * _scaleMatrix;
            }
        }
        public Vector2 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                _scaleMatrix = Matrix4.CreateScale(new Vector3(_scale));
                _viewMatrix = _translationMatrix * _rotationMatrix * _scaleMatrix;
            }
        }
        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                _rotationMatrix = Matrix4.CreateRotationZ(_rotation);
                _viewMatrix = _translationMatrix * _rotationMatrix * _scaleMatrix;
            }
        }

        public readonly Vector2 ScreenSize;

        private readonly Matrix4 _projection;

        private Vector2 _position;
        private Vector2 _scale;
        private float _rotation;

        private Matrix4 _translationMatrix;
        private Matrix4 _rotationMatrix;
        private Matrix4 _scaleMatrix;
        private Matrix4 _viewMatrix;

        public Camera(Vector2 screenSize)
        {
            this.ScreenSize = screenSize;
            this.Scale = Vector2.One;

            _projection = Matrix4.CreateOrthographic(ScreenSize.X, ScreenSize.Y, -1f, 1f);
            
            _translationMatrix = Matrix4.CreateTranslation(new Vector3(-_position));
            _scaleMatrix = Matrix4.CreateScale(new Vector3(_scale));
            _rotationMatrix = Matrix4.CreateRotationZ(_rotation);
            _viewMatrix = _translationMatrix * _rotationMatrix * _scaleMatrix;
        }

        public Matrix4 GetView() => _viewMatrix;

        public Matrix4 GetProjection() => _projection;

        public Matrix4 GetViewProjection() => GetView() * GetProjection();
    }
}
