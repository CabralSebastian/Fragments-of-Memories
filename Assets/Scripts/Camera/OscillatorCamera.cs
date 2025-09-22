using UnityEngine;

public class OscillatorCamara : MonoBehaviour
{
    [SerializeField] private float _maximum = 100;
    [SerializeField] private float _minimum = 0;
    [SerializeField] private float _speed = 2f;

    private float _direction = 1;

    public void Update()
    {
        Vector3 velocity = new(_direction * _speed * Time.deltaTime, 0f, 0f);
        transform.position += velocity;

        float xValue = transform.position.x;

        if (xValue <= _minimum || xValue >= _maximum)
            _direction *= -1;

    }
}
