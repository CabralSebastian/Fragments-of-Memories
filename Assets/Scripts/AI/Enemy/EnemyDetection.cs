using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
  [SerializeField] private LayerMask _playerLayerMask;
  [SerializeField] private float _sightRadius;
  [SerializeField] private float _chaseRadius;
  [SerializeField] private float _attackRadius;
  [SerializeField] private bool _drawSightGizmos = false;
  [SerializeField] private bool _drawChaseGizmos = false;
  [SerializeField] private bool _drawAttackGizmos = false;

  public bool PlayerInSight =>
    Physics.CheckSphere(transform.position, _sightRadius, _playerLayerMask);

  public bool PlayerInChaseRange =>
    Physics.CheckSphere(transform.position, _chaseRadius, _playerLayerMask);

  public bool PlayerInAttackRange =>
    Physics.CheckSphere(transform.position, _attackRadius, _playerLayerMask);

  private void OnDrawGizmos()
  {
    if (_drawSightGizmos)
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, _sightRadius);
    }

    if (_drawChaseGizmos)
    {
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position, _chaseRadius);
    }

    if (_drawAttackGizmos)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
  }

/*
  void DetectarJugador() {
    Collider[] colliders = Physics.OverlapSphere(transform.position, radioDeteccion, capaJugador);
    
    foreach (Collider col in colliders) {
      Vector3 direccionAlJugador = (col.transform.position - transform.position).normalized;
      float angulo = Vector3.Angle(transform.forward, direccionAlJugador);

      if (angulo < anguloVision / 2f) {
        // Solo dentro del campo de visión
        Ray ray = new Ray(transform.position + Vector3.up, direccionAlJugador);
        if (Physics.Raycast(ray, out RaycastHit hit, radioDeteccion)) {
          if (hit.collider.CompareTag("Jugador")) {
            // El enemigo ve al jugador
            PerseguirJugador(hit.collider.transform);
          }
        }
      }
    }
  }
*/
}