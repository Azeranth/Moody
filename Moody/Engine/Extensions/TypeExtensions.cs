using Microsoft.Xna.Framework;

namespace Moody.Engine.Extensions
{
    public static partial class Extensions
    {
        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Vector3 AsNormal(this Vector3 vector3)
        {
            vector3.Normalize();
            return vector3;
        }
        public static Quaternion AsNormal(this Quaternion quaternion)
        {
            quaternion.Normalize();
            return quaternion;
        }
    }
}