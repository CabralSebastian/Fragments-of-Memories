
public class EnemyChaseState : BaseState
{
  private readonly EnemyController _controller;
  private readonly float _chaseSpeedMultiplier = 2f;  

  public EnemyChaseState(EnemyController controller)
  {
    _controller = controller;
  }

  public override void OnEnter()
  { 
    _controller.TurnYellow();
    _controller.MultiplySpeed(_chaseSpeedMultiplier);
  }

  public override void OnExit()
  { 
    _controller.MultiplySpeed(1f);
  }

  public override void Update(float deltaTime)
  {
    _controller.MoveTo(GameManager.Instance.Player.transform.position);

    HandleTransitions();
  }

  private void HandleTransitions()
  {
    if (_controller.PlayerInAttackRange)
      Transition<EnemyAttackState>();
    
    else if (!_controller.PlayerInChaseRange)
      Transition<EnemyIdleState>();
  }
}