
using UnityEngine;

public class EnemyIdleState : BaseState
{
  private readonly EnemyController _controller;

  private float _timeToWait;
  private float _timeSpentWaiting;
  private bool WaitedEnough => _timeSpentWaiting >= _timeToWait;
  
  public EnemyIdleState(EnemyController controller)
  {
    _controller = controller;
  }

  public override void OnEnter()
  {
    _controller.TurnBaseColor();
    _timeToWait = Random.Range(0.5f, 2f);
    _timeSpentWaiting = 0f;
  }

  public override void OnExit() { }

  public override void Update(float deltaTime)
  {
    _timeSpentWaiting += deltaTime;
    
    HandleTransitions();
  }

  private void HandleTransitions()
  {
    if (_controller.PlayerInSight)
      Transition<EnemyChaseState>();

    else if (WaitedEnough)
      Transition<EnemyPatrolState>();
  }
}