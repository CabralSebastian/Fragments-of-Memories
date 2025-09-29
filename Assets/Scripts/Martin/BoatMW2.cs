using UnityEngine;

public class BoatMW2 : MonoBehaviour
{
    [SerializeField] protected float speed;

    protected Vector3 direction;

    protected Rigidbody rb;
    protected Rigidbody playerRb;

    protected bool playerHasStepped = false;
    protected bool playerIsStepping = false;

    private void Start()
    {
        direction = new Vector3(0, 0, 1);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
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

    protected void Move()
    {
        if (playerHasStepped)
        {
            //transform.Translate(direction * speed * Time.deltaTime);
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        }
        if (playerIsStepping) 
        {
            playerRb.MovePosition(playerRb.position + direction * speed * Time.deltaTime);
        }
    }
}
