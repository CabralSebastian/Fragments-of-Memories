public class EmptyMemory : Interactable
{
  public override void Interact()
  {
    gameObject.SetActive(false);
  }
}
