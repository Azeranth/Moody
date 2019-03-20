using Microsoft.Xna.Framework.Input;
using Moody.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moody.Engine
{
    public class Axis
    {
        public List<Func<float>> weights;

        public float GetValue()
        {
            return weights.Select(n => n()).Sum();
        }
    }
}