using Moody.Delegates;
using Moody.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moody.Components
{
    public abstract class Actor
    {
        private InitizalizeEventHandler onInitialize = delegate { };
        private StartEventHandler onStart = delegate { };
        private UpdateEventHandler onUpdate = delegate { };
        private DestroyEventHandler onDestroy = delegate { };

        private Scene memberScene;
        private Guid id = Guid.NewGuid();
        private string displayName;
        private Transform transform = new Transform();

        public InitizalizeEventHandler OnInitialize { get => onInitialize; set => onInitialize = value; }
        public StartEventHandler OnStart { get => onStart; set => onStart = value; }
        public UpdateEventHandler OnUpdate { get => onUpdate; set => onUpdate = value; }
        public DestroyEventHandler OnDestroy { get => onDestroy; set => onDestroy = value; }

        public Scene MemberScene { get => memberScene; set => memberScene = value; }
        public Guid Id { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public Transform Transform { get => transform; set => transform = value; }

        public virtual void Initialize()
        {
            onInitialize();
        }
        public virtual void Start()
        {
            onStart();
        }
        public virtual void Update(float deltaTime)
        {
            onUpdate(deltaTime);
        }
        public virtual void Destroy()
        {
            onDestroy();
        }
    }
}
