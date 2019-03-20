using Microsoft.Xna.Framework.Graphics;
using Moody.Engine;
using Moody.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moody.Components
{
    public class StaticActor : Actor, IDrawable
    {
        private Model mainModel;

        public Model MainModel { get => mainModel; set => mainModel = value; }

        public void Draw()
        {
            foreach (ModelMesh mesh in mainModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = Transform.WorldMatrix;
                }
                mesh.Draw();
            }
        }
    }
}
