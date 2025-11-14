public class WaterfallsMemory : Interactable
{
  public override string Action => "\"F\" Para Tocar";

  public override void Interact()
  {
    GameManager.Instance.RememberWaterfalls = true;
    gameObject.SetActive(false);
  }
}
