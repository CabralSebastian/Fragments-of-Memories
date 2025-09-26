using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Attack : Hitbox
{
  [SerializeField] private float _durationAlive = 0.5f;
  private float _timeSpentAlive = 0f;

  private void Update()
  {
    _timeSpentAlive += Time.deltaTime;

    if (_timeSpentAlive >= _durationAlive)
      Destroy(gameObject);
  }
}