public class EnemyStateFactory : IStateFactory
{
	private readonly EnemyController _controller;

	public EnemyStateFactory(EnemyController controller)
	{
		_controller = controller;
	}

	public T Create<T>() where T : IState
	{
		var constructor = typeof(T).GetConstructor(new[] { typeof(EnemyController) });
		
		return (T)constructor.Invoke(new object[] { _controller });
	}
}