using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.Player.BoardBoat(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.Instance.Player.LeaveBoat();
    }
}
