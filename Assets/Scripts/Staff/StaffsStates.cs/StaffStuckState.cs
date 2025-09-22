public class StaffStuckState : BaseState
{
  private readonly StaffController _staff;

  public StaffStuckState(StaffController staff)
  {
    _staff = staff;
  }

  public override void OnEnter()
  { 
    _staff.SetInteractable(true);
  }

  public override void OnExit()
  { 
    _staff.SetInteractable(false);
  }

  public override void Update(float deltaTime)
  {
    _staff.HandleHighlight();
    HandleTransitions();
  }
  private void HandleTransitions()
  {
    if (_staff.PressedInteractionKey)
      Transition<StaffActivatingState>();
  }
}
