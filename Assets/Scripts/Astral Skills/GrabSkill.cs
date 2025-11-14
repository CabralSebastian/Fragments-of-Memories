using UnityEngine;

public class GrabSkill : MonoBehaviour
{
  [SerializeField] private Camera _camera;
  [SerializeField] private float _maxDistance = 100f;
  [SerializeField] private LayerMask _layerMask;
  [SerializeField] private float _outlineThinkness = 0.1f;
  [SerializeField] private Color _outlineColor = Color.white;

  private Grabbable _highlighted = null;
  private Grabbable _grabbed = null;

  private MaterialPropertyBlock _mpb;
  private string Action => "\"C\" Para Agarrar";


  public bool IsGrabbing => _grabbed != null;

  private void Awake()
  {
      _mpb = new MaterialPropertyBlock();
  }

  private void Update()
  {
    if (_grabbed)
      return;

    Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

    if (!Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _layerMask))
    {
      ClearHighlight();
      return;
    }

    if (hit.collider.TryGetComponent(out Grabbable grabable) && grabable.enabled)
    {
      if (_highlighted == grabable)
        return;

      ClearHighlight();
      _highlighted = grabable;
      TurnHighlightOn();
    }
    else
      ClearHighlight();
  }

  public void Grab()
  {
    TurnHighlightOff();
    _highlighted.Grab();
    _grabbed = _highlighted;
  }

  public void Release()
  {
    _grabbed.Release();
    _grabbed = null;
  }

  private void TurnHighlightOn()
  {
    if (!_highlighted)
      return;

    Renderer renderer = _highlighted.GetComponent<Renderer>();
    renderer.GetPropertyBlock(_mpb);

    _mpb.SetColor("_OutlineColor", _outlineColor);
    _mpb.SetFloat("_OutlineThickness", _outlineThinkness);

    renderer.SetPropertyBlock(_mpb);

    GameManager.Instance.ShowAction(Action);
  }

  private void TurnHighlightOff()
  {
    if (!_highlighted)
      return;

    Renderer renderer = _highlighted.GetComponent<Renderer>();
    renderer.GetPropertyBlock(_mpb);

    _mpb.SetFloat("_OutlineThickness", 0f);

    renderer.SetPropertyBlock(_mpb);

    GameManager.Instance.HideAction();
  }

  private void ClearHighlight()
  {
    if (_highlighted)
      TurnHighlightOff();

    _highlighted = null;
  }
}
