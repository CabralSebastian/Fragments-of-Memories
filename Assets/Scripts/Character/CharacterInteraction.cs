using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
  [SerializeField] private KeyCode _interactionKey = KeyCode.F;
  [SerializeField] private LayerMask _layerMask;
  [SerializeField] private float _radius = 2f;
  [SerializeField] private bool _showGizmo = true;
  private readonly Collider[] _hits = new Collider[2];
  private Vector3 Center => transform.position + transform.up;
  private Interactable _interactable;

  public bool Interacted => Input.GetKeyDown(_interactionKey);

  public bool ThereIsAnInteractable()
  {
    _interactable = GetNearestInteractableObjects();

    return _interactable;
  }

  public void Interact()
  {
    _interactable.Interact();
  }

  private Interactable GetNearestInteractableObjects()
  {
    int hitCount = Physics.OverlapSphereNonAlloc(Center, _radius, _hits, _layerMask);
    Interactable nearestInteractable = null;
    float shortestDistance = float.MaxValue;

    for (int i = 0; i < hitCount; i++)
    {
      Collider hit = _hits[i];
      if (hit.gameObject.TryGetComponent(out Interactable interactable))
      {
        float distance = Vector3.Distance(Center, hit.transform.position);
        if (distance < shortestDistance)
        {
          shortestDistance = distance;
          nearestInteractable = interactable;
        }
      }
    }

    if (nearestInteractable.CompareTag("Staff") && GameManager.Instance.IsAstralWorld > 0)
      nearestInteractable = null;

    return nearestInteractable;
  }


  private void OnDrawGizmosSelected()
  {
    if (!_showGizmo)
      return;

    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(Center, _radius);
  }
}