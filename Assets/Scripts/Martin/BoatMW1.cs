using UnityEngine;

public class BoatMW1 : BoatMW2
{
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = new Vector3(1, 0, 0);
    }

    private void Update()
    {
        if (playerHasStepped)
        {
            Move();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StopPoint"))
        {
            direction = new Vector3(direction.x * -1, 0, 0);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHasStepped = true;
            playerIsStepping = true;
            playerRb = collision.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsStepping = false;
        }
    }
}
