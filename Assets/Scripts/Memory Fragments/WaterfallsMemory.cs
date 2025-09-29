public class WaterfallsMemory : Interactable
{
  public override void Interact()
  {
    GameManager.Instance.RememberWaterfalls = true;
    gameObject.SetActive(false);
  }
}
