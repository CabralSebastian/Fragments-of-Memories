public class CharacterWakeUpState : BaseState
{
  private readonly CharacterController _controller;

  public CharacterWakeUpState(CharacterController controller)
  {
    _controller = controller;
  }

  public override void OnEnter()
  {
    _controller.AstralSkills.UnAstralize();
    _controller.PlayAnimation("Stand_Up");
    _controller.DisableMovement();

  }

  public override void OnExit()
  {
    _controller.EnableMovement();
  }

  public override void Update(float deltaTime)
  {
    HandleTransitions();
  }

  private void HandleTransitions()
  {
    if (_controller.IsAnimationFinished("Stand_Up"))
      Transition<CharacterIdleState>();
  }
}