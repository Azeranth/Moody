using Microsoft.Xna.Framework;
using Moody.Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moody.Engine
{
    public class Transform
    {
        Vector3 position = Vector3.Zero;
        Vector3 scale = Vector3.One;
        Quaternion rotation = Quaternion.Identity;

        public Vector3 Position { get => position; set => position = value; }
        public Vector3 Scale { get => scale; set => scale = value; }
        public Quaternion Rotation { get => rotation; set => rotation = value; }

        public Matrix WorldMatrix { get => Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(position); }
        public Vector3 Forward { get => Vector3.Transform(Vector3.UnitZ, rotation).AsNormal(); }
        public Vector3 Right { get => Vector3.Transform(Vector3.UnitX, rotation).AsNormal(); }
        public Vector3 Up { get => Vector3.Transform(Vector3.UnitY, rotation).AsNormal(); }
    }
}
