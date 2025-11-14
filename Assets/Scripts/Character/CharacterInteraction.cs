using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
  [SerializeField] private KeyCode _interactionKey = KeyCode.F;
  [SerializeField] private LayerMask _layerMask;
  [SerializeField] private float _radius = 2f;
  [SerializeField] private bool _showGizmo = true;
  private readonly Collider[] _hits = new Collider[2];
  private Vector3 Center => transform.position + transform.up;

  [SerializeField] private float _outlineThinkness = 0.1f;
  [SerializeField] private Color _outlineColor = Color.white;

  private Interactable _interactable = null;
  private Interactable _lastInteractable = null;

  private MaterialPropertyBlock _mpb;

  public bool Interacted => Input.GetKeyDown(_interactionKey);
  public bool ThereIsAnInteractable() => _interactable != null;

  private void Awake()
  {
    _mpb = new MaterialPropertyBlock();
  }

  private void Update()
  {
    _interactable = GetNearestInteractableObjects();

    if (_interactable == null)
    {
      ClearLastHighlight();
      return;
    }

    if (_interactable != _lastInteractable)
    {
      ClearLastHighlight();
      TurnHighlightOn(_interactable);
      _lastInteractable = _interactable;
    }
  }

  public void Interact()
  {
    if (_interactable != null)
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
      if (hit.TryGetComponent(out Interactable interactable))
      {
        float distance = Vector3.Distance(Center, hit.transform.position);
        if (distance < shortestDistance)
        {
          shortestDistance = distance;
          nearestInteractable = interactable;
        }
      }
    }

    if (nearestInteractable == null || (nearestInteractable.CompareTag("Staff") && GameManager.Instance.IsAstralWorld > 0))
        return null;

    return nearestInteractable;
  }

  private void TurnHighlightOn(Interactable interactable)
  {
    /*
    Renderer[] renderers = interactable.GetComponentsInChildren<Renderer>();
    foreach (var renderer in renderers)
    {
      renderer.GetPropertyBlock(_mpb);
      _mpb.SetFloat("_OutlineThickness", _outlineThinkness);
      _mpb.SetColor("_OutlineColor", _outlineColor);
      renderer.SetPropertyBlock(_mpb);
    }*/
  }

  private void TurnHighlightOff(Interactable interactable)
  {
    /*
    Renderer[] renderers = interactable.GetComponentsInChildren<Renderer>();
    foreach (var renderer in renderers)
    {
      renderer.GetPropertyBlock(_mpb);
      _mpb.SetFloat("_OutlineThickness", 0f);
      renderer.SetPropertyBlock(_mpb);
    }*/
  }

  private void ClearLastHighlight()
  {
    if (_lastInteractable != null)
    {
      TurnHighlightOff(_lastInteractable);
      _lastInteractable = null;
    }
  }

  private void OnDrawGizmosSelected()
  {
    if (!_showGizmo)
      return;

    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(Center, _radius);
  }
}
