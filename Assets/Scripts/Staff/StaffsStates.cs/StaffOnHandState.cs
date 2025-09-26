public class StaffOnHandState : BaseState
{
  private readonly StaffController _staff;

  public StaffOnHandState(StaffController staff)
  {
    _staff = staff;
  }

  public override void OnEnter()
  { 
    _staff.SetInteractable(false);
    _staff.GoToHand();

  }

  public override void OnExit()
  { 
    _staff.SetInteractable(true);
  }

  public override void Update(float deltaTime)
  {
    // if (_staff.PressedInteractionKey)
    //  Transition<StaffStuckState>();
  }
}
