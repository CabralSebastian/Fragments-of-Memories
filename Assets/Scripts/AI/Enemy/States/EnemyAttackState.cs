
public class EnemyAttackState : BaseState
{
  private readonly EnemyController _controller;
  private readonly float _timeToAttack = 2f;
  private float _timeSpentAttacking;
  private bool AttackedEnough => _timeSpentAttacking >= _timeToAttack;
  private bool QuarterSecondPassed => _timeSpentAttacking >= _timeToAttack * 0.25f;
  private bool HalfSecondPassed => _timeSpentAttacking >= _timeToAttack * 0.5f;
  private bool _attacked = false;

  public EnemyAttackState(EnemyController controller)
  {
    _controller = controller;
  }

  public override void OnEnter()
  {
    _controller.IsStopped(true);
    _controller.TurnOrange();
    _timeSpentAttacking = 0f;
    _attacked = false;
  }

  public override void OnExit()
  {
    _controller.IsStopped(false); 
  }

  public override void Update(float deltaTime)
  {
    _timeSpentAttacking += deltaTime;

    if (QuarterSecondPassed)
    {
      _controller.TurnRed();
      _controller.FacePlayer();
    }
    
    if (HalfSecondPassed && !_attacked)
    {
      _attacked = true;
      _controller.Attack();
    }
    
    HandleTransitions();
  }

  private void HandleTransitions()
  {
    if (AttackedEnough)
      Transition<EnemyChaseState>();
  }
}