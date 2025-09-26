
public class CharacterStickStaffState : BaseState
{
  private readonly CharacterController _controller;

  public CharacterStickStaffState(CharacterController controller)
  {
    _controller = controller;
  }

  public override void OnEnter()
  {
    _controller.PlayAnimation("Interact");
    _controller.DisableMovement();
  }

  public override void OnExit()
  {
    _controller.EnableMovement();
    _controller.Staff.ChangeToStuck();
    _controller.AstralSkills.Astralizing = false;
  }

  public override void Update(float deltaTime)
  {
    HandleTransitions();
  }

  private void HandleTransitions()
  {
    if (!_controller.IsAnimationFinished("Interact"))
      return;

    if (_controller.AstralSkills.Astralizing)
      Transition<CharacterMeditateState>();
    else
      Transition<CharacterIdleState>();
  }
}