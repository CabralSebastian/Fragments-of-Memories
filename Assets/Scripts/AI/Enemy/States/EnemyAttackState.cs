using UnityEngine;

public class EnemyAttackState : BaseState
{
  private readonly EnemyController _controller;

  public EnemyAttackState(EnemyController controller)
  {
    _controller = controller;
  }

  public override void OnEnter()
  {
    _controller.IsStopped(true);
    _controller.FacePlayer();
    _controller.Animator.CrossFade(_controller.AttackName, 0.2f);
  }

  public override void OnExit()
  {
    _controller.IsStopped(false); 
  }

  public override void Update(float deltaTime)
  {
    HandleTransitions();
  }

  private void HandleTransitions()
  {
    AnimatorStateInfo stateInfo = _controller.Animator.GetCurrentAnimatorStateInfo(0);

    if (_controller.Animator.IsInTransition(0))
      stateInfo = _controller.Animator.GetNextAnimatorStateInfo(0);
        
    if (stateInfo.normalizedTime >= 0.95f && stateInfo.IsName(_controller.AttackName))
      Transition<EnemyAfterAttackIdleState>();
  }
}