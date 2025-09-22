public class StaffOffState : BaseState
{
  private readonly StaffController _staff;

  public StaffOffState(StaffController staff)
  {
    _staff = staff;
  }

  public override void OnEnter()
  { 
    _staff.SetInteractable(true);
  }

  public override void OnExit()
  {
    GameManager.Instance.Player.Staff = _staff;
    _staff.SetInteractable(false);
  }

  public override void Update(float deltaTime) { }
}
