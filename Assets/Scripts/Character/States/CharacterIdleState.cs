
public class CharacterIdleState : BaseState
{
  private readonly CharacterController _controller;

  public CharacterIdleState(CharacterController controller)
  {
    _controller = controller;
  }

  public override void OnEnter()
  {
    _controller.PlayAnimation("Idle");
  }

  public override void OnExit() { }
  public override void Update(float deltaTime)
  { 
    HandleTransitions();
  }

  private void HandleTransitions()
  {
    if (_controller.Jumped)
      Transition<CharacterJumpState>();

    else if (!_controller.IsGrounded)
      Transition<CharacterFallState>();

    else if (_controller.AstralSkills.AstralizeKeyPressed)
      if (!_controller.AstralSkills.IsAstralized)
        Transition<CharacterMeditateState>();
      else
        Transition<CharacterWakeUpState>();

    else if (_controller.Interacted)
      Transition<CharacterInteractingState>();

    else if (_controller.Staff && _controller.Staff.IsOnHand && _controller.Staff.PressedInteractionKey)
      Transition<CharacterStickStaffState>();

    else if (_controller.IsMoving)
      Transition<CharacterMovingState>();
  }
}