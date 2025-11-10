using UnityEngine;

public class TreeLeaves : ThresholdGrabable
{
  [SerializeField] private TreeLog _log;

    public bool IsGrabbed => _isGrabbed;

  protected override void Action()
  {
    _log.Activate();
    gameObject.SetActive(false);


    Camera mainCamera = GameManager.Instance.MainCamera;
    Vector3 dragPos = mainCamera.transform.position + mainCamera.transform.forward * Vector3.Distance(mainCamera.transform.position, _grabPoint);

    Vector3 dragDirection = dragPos - _grabPoint;
    dragDirection.Normalize();

    _log.Push(dragDirection);
  }
}