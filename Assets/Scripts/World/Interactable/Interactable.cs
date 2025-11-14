using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
  public abstract string Action { get; }
  public abstract void Interact();
}