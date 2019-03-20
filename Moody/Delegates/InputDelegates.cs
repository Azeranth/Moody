using System.Collections.Generic;

namespace Moody.Delegates
{
    public delegate void KeyDownEventHandler();
    public delegate void KeyUpEventHandler();
    public delegate void KeyHeldDelegate(float deltaTime);
    public delegate void AxisDelegate(float AxisDelta, float deltaTime);
}