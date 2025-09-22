using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ColliderSwitch : MonoBehaviour
{
	private MeshFilter _filter;
	private MeshCollider _collider;
	private MeshRenderer _renderer;
	[SerializeField] private Mesh _mainColliderMesh;
	[SerializeField] private Mesh _secondaryColliderMesh;

	private bool _isMainColliderActive;

	private void Start()
	{
		_filter = GetComponent<MeshFilter>();
		_collider = GetComponent<MeshCollider>();
		_renderer = GetComponent<MeshRenderer>();

		_filter.sharedMesh = _mainColliderMesh;
		_collider.sharedMesh = _mainColliderMesh;

		Material defaultMat = new(Shader.Find("Universal Render Pipeline/Lit")) { color = Color.gray };
		_renderer.material = defaultMat;

		_isMainColliderActive = true;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			ToggleColliders();

	}

	private void ToggleColliders()
	{
		_isMainColliderActive = !_isMainColliderActive;

		if (_isMainColliderActive)
		{
			_filter.sharedMesh = _mainColliderMesh;
			_collider.sharedMesh = _mainColliderMesh;
		}
		else
		{
			_filter.sharedMesh = _secondaryColliderMesh;
			_collider.sharedMesh = _secondaryColliderMesh;	
		}
	}
}
