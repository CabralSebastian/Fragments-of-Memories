public interface IStateFactory
{
	public T Create<T>() where T : IState;
}
