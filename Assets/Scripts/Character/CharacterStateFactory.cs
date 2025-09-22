public class CharacterStateFactory : IStateFactory
{
	private readonly CharacterController _controller;

	public CharacterStateFactory(CharacterController controller)
	{
		_controller = controller;
	}

	public T Create<T>() where T : IState
	{
		var constructor = typeof(T).GetConstructor(new[] { typeof(CharacterController) });
		
		return (T)constructor.Invoke(new object[] { _controller });
	}
}