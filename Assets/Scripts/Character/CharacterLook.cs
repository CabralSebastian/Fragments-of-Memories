using System;
using UnityEngine;

public class CharacterLook : MonoBehaviour
{
  [SerializeField] private float _sensitivity = 100f;
  [SerializeField] private Transform _cameraPivot;
  private float _rotationX = 0f;
  private float _rotationY = 0f;
  [SerializeField] private float _maxLookX = 10f;
  [SerializeField] private float _maxLookY = 30f;

  private void Start()
  {
    GameManager.Instance.LockCursor();
  }

  private void Update()
  {
    float mouseX = Input.GetAxis("Mouse X");
    float mouseY = Input.GetAxis("Mouse Y");

    _rotationY += mouseX * _sensitivity * Time.deltaTime;
    _rotationY = Math.Clamp(_rotationY, -_maxLookX, _maxLookX);

    _rotationX -= mouseY * _sensitivity * Time.deltaTime;
    _rotationX = Math.Clamp(_rotationX, -_maxLookY+10, _maxLookY+10);

    _cameraPivot.localRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);

    if (Math.Abs(_rotationX) > _maxLookY - 0.1f)
      return;

    float rotationY = transform.rotation.eulerAngles.y + mouseX * _sensitivity * Time.deltaTime;
    transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
  }
}
