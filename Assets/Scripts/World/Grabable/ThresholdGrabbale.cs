using UnityEngine;

public abstract class ThresholdGrabable : Grabbable
{
  protected Vector3 _grabPoint;
  [SerializeField] protected float _wiggleAmount = 0.05f;
  [SerializeField] protected float _wiggleSpeed = 10f;
  [SerializeField] protected float _thresholdDistance = 1.5f;

  protected Vector3 _originalPosition;

  protected override void Update()
  {
    if (!_isGrabbed)
      return;

    Wiggle();

    Camera mainCamera = GameManager.Instance.MainCamera;
    Vector3 dragPos = mainCamera.transform.position + mainCamera.transform.forward * Vector3.Distance(mainCamera.transform.position, _grabPoint);

    float distanceDragged = Vector3.Distance(dragPos, _grabPoint);

    if (distanceDragged >= _thresholdDistance)
    {
      _isGrabbed = false;
      GameManager.Instance.Player.AstralSkills.Release();
      Action();
    }
  }

  private void Wiggle()
  {
    Vector3 wiggleOffset = new Vector3(
      Mathf.Sin(Time.time * _wiggleSpeed),
      Mathf.Cos(Time.time * _wiggleSpeed),
      Mathf.Sin(Time.time * _wiggleSpeed * 0.5f)
    ) * _wiggleAmount;

    transform.position = _originalPosition + wiggleOffset;
  }

  public override void Grab()
  {
    _isGrabbed = true;
    _grabPoint = transform.position;
    _originalPosition = transform.position;
  }

  public override void Release()
  {
    _isGrabbed = false;
    transform.position = _originalPosition;
  }

  protected abstract void Action();
}
