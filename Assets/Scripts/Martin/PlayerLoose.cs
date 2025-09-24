using UnityEngine;
using UnityEngine.Rendering;

public class PlayerLoose : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    private float _PlayerHealth;

    private void Update()
    {
        _PlayerHealth = healthManager.gameObject.GetComponent<HealthManager>().Health;
        if (_PlayerHealth <= 0)
        {
            healthManager.SetHealth(3);
            GameManager.Instance.Reset();
        }
    }
}
