using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StaffController : Interactable
{
	private FSM _fms;
	private BoxCollider _collider;
	[SerializeField] private Synthesizer _synthesizer;
	[SerializeField] private KeyCode _interactionKey = KeyCode.E;
	[SerializeField] private GameObject _model;
	private MeshRenderer _modelMesh;
	[SerializeField] private GameObject _handStaff;
	[SerializeField] private Transform _stickPosition;
	[SerializeField] private bool _showDebug;

	public Synthesizer Synthesizer => _synthesizer;
	public bool PressedInteractionKey => Input.GetKeyDown(_interactionKey);
	public bool IsOnHand => _fms.CurrentState is StaffOnHandState;

	public void Awake()
	{
		_modelMesh = _model.GetComponent<MeshRenderer>();
		_collider = GetComponent<BoxCollider>();
		StaffStateFactory stateFactory = new(this);
		IState initialState = stateFactory.Create<StaffOffState>();

		_fms = new FSM(stateFactory, initialState);
		_synthesizer.enabled = false;
	}

	public void Update()
	{
		_fms.Update(Time.deltaTime);

		HandleHighlight();
	}

	public override void Interact()
	{
		Grab();
	}

	public void SetInteractable(bool interactable)
	{
		gameObject.layer = interactable ? LayerMask.NameToLayer("Interactable") : LayerMask.NameToLayer("Default");
	}

	private void Grab()
	{
		if (_fms.CurrentState is not StaffOffState && _fms.CurrentState is not StaffStuckState)
			return;

		_fms.ChangeState<StaffOnHandState>();
		transform.SetParent(_stickPosition);
		transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

		_modelMesh.enabled = false;
		_collider.enabled = false;
		_handStaff.SetActive(true);
	}

	public void Stick()
	{
		if (_fms.CurrentState is not StaffOnHandState)
			return;

		_fms.ChangeState<StaffStuckState>();
		transform.SetParent(null);

		_modelMesh.enabled = true;
		_collider.enabled = true;
		_handStaff.SetActive(false);
	}

	public void HandleHighlight()
	{
		// TODO: Update Shader
	}

	private void OnGUI()
	{
		if (!_showDebug)
			return;

		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.fontSize = 16;
		style.normal.textColor = Color.white;

		Rect rect = new Rect(10, 10, 300, 30);
		GUI.Label(rect, $"Staff FSM State: {_fms.CurrentState?.GetType().Name}", style);
	}
}
