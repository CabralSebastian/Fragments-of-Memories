public abstract class BaseState : IState
{
  private FSM _fsm;

  public virtual void Initialize(FSM fsm)
  {
    _fsm = fsm;
  }

  protected virtual void Transition<T>() where T : IState
  {
    _fsm.ChangeState<T>();
  }

  public abstract void OnEnter();
  public abstract void OnExit();
  public abstract void Update(float deltaTime);
}
