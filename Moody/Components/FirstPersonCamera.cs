using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Moody.Delegates;
using Moody.Engine.Extensions;
using Moody.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moody.Components
{
    public class FirstPersonCamera : Camera
    {
        public float speed = 5;
        public float orbitalSpeed = 100;

        public override void Start()
        {
            MemberScene.InputDispatcher.SubscribeAxisDelegegate(InputAxes.MoveForward, MoveForward);
            MemberScene.InputDispatcher.SubscribeAxisDelegegate(InputAxes.MoveRight, MoveRight);
            MemberScene.InputDispatcher.SubscribeAxisDelegegate(InputAxes.MoveUp, MoveUp);

            base.Start();
        }

        private void MoveForward(float AxisDelta, float deltaTime)
        {
            var positionDelta = Transform.Forward * AxisDelta * speed * deltaTime;

            Transform.Position += positionDelta;
        }
        private void MoveRight(float AxisDelta, float deltaTime)
        {
            Transform.Position += Transform.Right * AxisDelta * speed * deltaTime;
        }
        private void MoveUp(float AxisDelta, float deltaTime)
        {
            Transform.Position += Transform.Up * AxisDelta * speed * deltaTime;
        }

        public override void Update(float deltaTime)
        {
            var MouseX = MemberScene.InputDispatcher.Axes[InputAxes.MouseX].GetValue() * deltaTime * orbitalSpeed;
            var MouseY = MemberScene.InputDispatcher.Axes[InputAxes.MouseY].GetValue() * deltaTime * orbitalSpeed;

            Transform.Rotation = Transform.Rotation * Quaternion.CreateFromYawPitchRoll(0, -MouseY, 0);
            Transform.Rotation = Quaternion.CreateFromYawPitchRoll(MouseX, 0, 0) * Transform.Rotation;
            Transform.Rotation.Normalize();

            base.Update(deltaTime);
        }
    }
}
