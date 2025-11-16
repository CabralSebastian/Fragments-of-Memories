using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private InteractableObject nearbyObject;

    private void Update()
    {
        if (!DialogueManager.Instance.inputEnabled)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TryInteract();
        }

        Debug.Log(nearbyObject);
    }

    public void SetNearbyObject(InteractableObject obj)
    {
        nearbyObject = obj;
    }

    public void ClearNearbyObject(InteractableObject obj)
    {
        if (nearbyObject == obj)
        {
            nearbyObject = null;
        }
    }

    private void TryInteract()
    {
        if (nearbyObject != null)
        {
            nearbyObject.Interact();
            Debug.Log("Interacted");
        }
    }
}
