using UnityEngine;

public class AimTarget : MonoBehaviour
{
  [SerializeField] private RectTransform _crosshair;
  [SerializeField] private Camera _mainCamera;
  [SerializeField] private float _distance = 10f;

  private void Update()
  {
    Vector3 screenPos = _crosshair.position;
    Ray ray = _mainCamera.ScreenPointToRay(screenPos);
    transform.position = ray.origin + ray.direction * _distance;

    transform.forward = ray.direction;
  }
}
