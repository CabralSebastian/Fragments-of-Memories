using UnityEngine;

public class BoatMW2 : MonoBehaviour
{
    [SerializeField] protected float speed;

    protected Vector3 direction;
    
    protected Rigidbody rb;

    protected bool playerHasStepped;

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
        }
    }

    protected void Move()
    {
        if (playerHasStepped)
        {
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        }
    }
}
