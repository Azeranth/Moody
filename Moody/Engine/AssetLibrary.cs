using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moody.Engine
{
    public class AssetLibrary
    {
        private List<Model> models = new List<Model>();

        public List<Model> Models { get => models; set => models = value; }
    }
}
