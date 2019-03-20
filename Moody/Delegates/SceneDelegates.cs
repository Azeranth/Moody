using Moody.Components;
using Moody.Engine;

namespace Moody.Delegates
{
    public delegate void ActivateEventHandler();
    public delegate void ActorRegisterEventHandler(Actor actorDeregistered);
    public delegate void ActorDeregisterEventHandler(Actor actorRegistered);
    public delegate void DeactivateEventHandler();
}