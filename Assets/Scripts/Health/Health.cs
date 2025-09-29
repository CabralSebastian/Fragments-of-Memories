using UnityEngine;

public class Health : MonoBehaviour
{
  [SerializeField] private IMortal _mortal;
  [SerializeField] private float _maxHealth = 3;
  [SerializeField] private float _currentHealth;
  public float Value => _currentHealth;
  public float MaxValue => _maxHealth;

  private void Start()
  {
    _currentHealth = _maxHealth;
  }

  public void TakeDamage(float damage)
  {
    _currentHealth -= damage;
    if (_currentHealth <= 0)
      _mortal.Die();
    else
      _mortal.OnTakeDamage(damage);
  }

  public void FillToMax()
  {
    _currentHealth = _maxHealth;
  }

  public void SetMortal(IMortal mortal)
  {
    _mortal = mortal;
  }
}
