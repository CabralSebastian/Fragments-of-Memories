using UnityEngine;

public class Passiflora : Interactable
{
  [SerializeField] private GameObject _vines;

  public override void Interact()
  {
    bool areVinesActive = _vines.activeSelf;
    _vines.SetActive(!areVinesActive);
  }
}
