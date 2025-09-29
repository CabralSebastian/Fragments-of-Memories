using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private float _health;

    [SerializeField] private float _maxHealth = 3;

    public float Health => _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            _health--;
            Debug.Log(_health);
        }
    }

    public void SetHealth(float newHealth)
    {
        _health = newHealth;
    }

}
