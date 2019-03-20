namespace Moody.Delegates
{
    public delegate void InitizalizeEventHandler();
    public delegate void StartEventHandler();
    public delegate void UpdateEventHandler(float deltaTime);
    public delegate void PreDestroyEventHandler();
    public delegate void DestroyEventHandler();
    public delegate void PostDestroyEventHandler();
}