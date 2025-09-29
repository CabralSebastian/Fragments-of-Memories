public interface IState
{
	public void Initialize(FSM fsm);
	public void OnEnter();
	public void OnExit();
	public void Update(float deltaTime);
}
