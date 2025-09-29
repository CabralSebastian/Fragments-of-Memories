using UnityEngine;

public class GrabSkill : MonoBehaviour
{
  [SerializeField] private Camera _camera;
  [SerializeField] private float _maxDistance = 100f;
  [SerializeField] private LayerMask _layerMask;

  private IGrabbable _grabbed = null;
  public bool IsGrabbing => _grabbed != null;

  public void Grab()
  {
    Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
    if (!Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _layerMask))
      return;

    GameObject hitObject = hit.collider.gameObject;

    if (hitObject.TryGetComponent(out IGrabbable grabable))
    {
      grabable.Grab();
      _grabbed = grabable;
    }
  }

  public void Release()
  {
    _grabbed.Release();
    _grabbed = null;
  }
}
