using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class WaterfallsAccess : MonoBehaviour
{

	private void Awake()
	{
		BoxCollider collider = GetComponent<BoxCollider>();
		collider.enabled = GameManager.Instance.RememberWaterfalls;

		if (GameManager.Instance.RememberWaterfalls)
			return;

		MeshRenderer mesh = GetComponent<MeshRenderer>();
		mesh.material.color = Color.red;
	}
}
