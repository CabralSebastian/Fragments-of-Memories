
public class EnemyAfterAttackIdleState : BaseState
{
  private readonly EnemyController _controller;

  private readonly float _timeToWait = 0.8f;
  private float _timeSpentWaiting;
  
  public EnemyAfterAttackIdleState(EnemyController controller)
  {
    _controller = controller;
  }

  public override void OnEnter()
  {
    _controller.IsStopped(true);
    _controller.Animator.CrossFade("Idle", 0.2f);
    _timeSpentWaiting = 0f;
  }

  public override void OnExit()
  {
    _controller.IsStopped(false);
  }

  public override void Update(float deltaTime)
  {
    _timeSpentWaiting += deltaTime;
    
    HandleTransitions();
  }

  private void HandleTransitions()
  {
    if (_timeSpentWaiting >= _timeToWait)
      Transition<EnemyChaseState>();
  }
}