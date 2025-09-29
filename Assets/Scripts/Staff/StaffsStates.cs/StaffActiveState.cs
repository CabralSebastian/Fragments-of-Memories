public class StaffActiveState : BaseState
{
  private readonly StaffController _staff;

  public StaffActiveState(StaffController staff)
  {
    _staff = staff;
  }

  public override void OnEnter() { }

  public override void OnExit() { }

  public override void Update(float deltaTime)
  {
    if (_staff.PressedInteractionKey)
      Transition<StaffDeactivatingState>();
  }
}
