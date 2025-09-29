public class CharacterInteractingState : BaseState
{
  private readonly CharacterController _controller;

  public CharacterInteractingState(CharacterController controller)
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
    _controller.Interact();
    _controller.EnableMovement();
  }

  public override void Update(float deltaTime)
  {
    HandleTransitions();
  }

  private void HandleTransitions()
  {
    if (_controller.IsAnimationFinished("Interact"))
      Transition<CharacterIdleState>();
  }
}