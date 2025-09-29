using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class IvyLabyrinthAccess : MonoBehaviour
{
	private void Awake()
	{
		BoxCollider collider = GetComponent<BoxCollider>();
		collider.enabled = GameManager.Instance.RememberIvyLabyrinth;

		if (GameManager.Instance.RememberIvyLabyrinth)
			return;

		MeshRenderer mesh = GetComponent<MeshRenderer>();
		mesh.material.color = Color.red;
	}
}
