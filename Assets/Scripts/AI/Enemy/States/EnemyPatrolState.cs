
public class EnemyPatrolState : BaseState
{
  private readonly EnemyController _controller;

  public EnemyPatrolState(EnemyController controller)
  {
    _controller = controller;
  }

  public override void OnEnter()
  {
    _controller.TurnBaseColor();
    _controller.MoveToRandomPoint();
  }

  public override void OnExit() { }

  public override void Update(float deltaTime)
  {
    HandleTransitions();
  }

  private void HandleTransitions()
  {
    if (_controller.PlayerInSight)
      Transition<EnemyChaseState>();
    
    else if (_controller.ReachedTarget)
      Transition<EnemyIdleState>();
  }
}