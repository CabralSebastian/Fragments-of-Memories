public class EmptyMemory : Interactable
{
  public override string Action => "\"F\" Para Tocar";

  public override void Interact()
  {
    gameObject.SetActive(false);
  }
}
