public class StaffStateFactory : IStateFactory
{
    private readonly StaffController _controller;

    public StaffStateFactory(StaffController controller)
    {
        _controller = controller;
    }

    public T Create<T>() where T : IState
    {
        var constructor = typeof(T).GetConstructor(new[] { typeof(StaffController) });
        
        return (T)constructor.Invoke(new object[] { _controller });
    }
}