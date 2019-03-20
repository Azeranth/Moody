using Microsoft.Xna.Framework;

namespace Moody.Components
{
    public class Camera : Actor
    {
        private float fieldOfView = MathHelper.ToRadians(60);
        private float aspectRatio = 1;
        private float nearPlane = 1;
        private float farPlane = 1000;

        public Matrix ViewMatrix
        {
            get
            {
                return Matrix.CreateLookAt(Transform.Position, Transform.Position + Transform.Forward, Vector3.UnitY);
            }
        }
        public Matrix ProjectionMatrix
        {
            get
            {
                return Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlane, farPlane);
            }
        }

        public float FieldOfView { get => fieldOfView; set => fieldOfView = value; }
        public float AspectRatio { get => aspectRatio; set => aspectRatio = value; }
        public float NearPlane { get => nearPlane; set => nearPlane = value; }
        public float FarPlane { get => farPlane; set => farPlane = value; }
    }
}