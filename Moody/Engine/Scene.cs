using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moody.Components;
using Moody.Delegates;

namespace Moody.Engine
{
    public class Scene
    {
        private ActivateEventHandler onActivate = delegate { };
        private DeactivateEventHandler onDeactivate = delegate { };
        private ActorRegisterEventHandler onActorRegister = delegate { };
        private ActorDeregisterEventHandler onActorDeregister = delegate { };

        private List<Actor> registeredActors = new List<Actor>();
        private Matrix worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
        private Camera activeCamera;
        private InputDispatcher inputDispatcher = new InputDispatcher();

        public ActivateEventHandler OnActivate { get => onActivate; set => onActivate = value; }
        public DeactivateEventHandler OnDeactivate { get => onDeactivate; set => onDeactivate = value; }
        public ActorRegisterEventHandler OnActorRegister { get => onActorRegister; set => onActorRegister = value; }
        public ActorDeregisterEventHandler OnActorDeregister { get => onActorDeregister; set => onActorDeregister = value; }

        public List<Actor> RegisteredActors { get => registeredActors; }
        public Matrix WorldMatrix { get => worldMatrix; set => worldMatrix = value; }
        public Camera ActiveCamera { get => activeCamera; set => activeCamera = value; }
        public InputDispatcher InputDispatcher { get => inputDispatcher; set => inputDispatcher = value; }

        public void RegisterActor(Actor actor)
        {
            if (RegisteredActors.Contains(actor))
                return;
            RegisteredActors.Add(actor);
            actor.MemberScene = this;
            OnActorRegister(actor);
            actor.Start();
        }

        public void DeregisterActor(Actor actor)
        {
            if (!RegisteredActors.Contains(actor))
                return;
            RegisteredActors.Remove(actor);
            OnActorDeregister(actor);
            actor.Destroy();
        }
        public void DeregisterActor(int index)
        {
            DeregisterActor(RegisteredActors[index]);
        }
        public void DeregisterActor(Guid id)
        {
            DeregisterActor(RegisteredActors.FirstOrDefault(n => n.Id == id));
        }

        public void Start()
        {
            inputDispatcher.Start();
        }
        public void Update(float deltaTime)
        {
            InputDispatcher.Update(deltaTime);
            foreach (Actor actor in registeredActors)
            {
                actor.Update(deltaTime);
            }
        }

        
    }
}
