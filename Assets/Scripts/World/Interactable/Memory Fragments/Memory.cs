
using UnityEngine;

public abstract class Memory : Interactable
{
  [SerializeField] private string _text = "Recuerdo";
  public override string Action => "Tocar";

  public override void Interact()
  {
    GameManager.Instance.ShowMemory(_text);
    OnInteract();

    // TODO: Sonido agarrar Recuerdo
    gameObject.SetActive(false);
  }

  public virtual void OnInteract()
  {
    
  }
}
