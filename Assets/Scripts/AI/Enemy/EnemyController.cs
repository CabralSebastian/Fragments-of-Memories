using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MoveAgent))]
[RequireComponent(typeof(EnemyDetection))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour, IMortal
{
	private FSM _fms;
	private Health _health;
	private MoveAgent _moveAgent;
	private Animator _animator;
	private EnemyDetection _enemyDetection;

	[SerializeField] private float _wanderRadius = 15f;
  [SerializeField] private GameObject _attackPrefab;
  [SerializeField] private Transform _attackPoint;
  [SerializeField] private string _attackName;

  public Health Health => _health;
	public Animator Animator => _animator;
	public string AttackName => _attackName;

	private bool _isPause = false;

	/* Awake & Update */
	public void Awake()
	{
		_health = GetComponent<Health>();
		_moveAgent = GetComponent<MoveAgent>();
		_animator = GetComponent<Animator>();
		_enemyDetection = GetComponent<EnemyDetection>();

		_health.SetMortal(this);

		EnemyStateFactory stateFactory = new(this);
		IState initialState = stateFactory.Create<EnemyIdleState>();

		_fms = new FSM(stateFactory, initialState);
	}

	public void Update()
	{
		if (_isPause)
			return;

		_fms.Update(Time.deltaTime);
	}

	/* Pause */
	public void SetPause(bool pause)
	{
		_isPause = pause;
	}

  /* Detection */
  public bool PlayerInSight => _enemyDetection.PlayerInSight;
  public bool PlayerInChaseRange => _enemyDetection.PlayerInChaseRange;
  public bool PlayerInAttackRange => _enemyDetection.PlayerInAttackRange;

  /* Movement */
  public bool ReachedTarget => _moveAgent.ReachedTarget;
  public void MoveTo(Vector3 target) => _moveAgent.MoveTo(target);
  public void MoveToRandomPoint() => _moveAgent.MoveToRandomPoint(_wanderRadius);
  public void MultiplySpeed(float multiplier) => _moveAgent.MultiplySpeed(multiplier);
  public void IsStopped(bool stop) => _moveAgent.IsStopped(stop);
	public void FacePlayer()
	{
		Vector3 targetPosition = GameManager.Instance.Player.transform.position;
		targetPosition.y = transform.position.y;

		transform.LookAt(targetPosition);
	}

  /* Attack */
	public void Attack()
	{
		// TODO: Attack Pool
		Instantiate(_attackPrefab, _attackPoint.position, transform.rotation);
	}

	/* Health */
  public void Die() => gameObject.SetActive(false);
	public void OnTakeDamage(float damage) => Debug.Log($"Taking damage: {damage}");
}
