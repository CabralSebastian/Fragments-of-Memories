using UnityEngine;
using System.Collections;
using System;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private float speed;
    public float Speed => speed;

    public void CalculateSpeed(bool condition)
    {
        StartCoroutine(CalcSpeed(condition));
    }

    private IEnumerator CalcSpeed(bool condition)
    {
        while (condition)
        {
            Vector3 prevPos = transform.position;

            yield return new WaitForFixedUpdate();

            speed = Mathf.RoundToInt(Vector3.Distance(transform.position, prevPos) / Time.fixedDeltaTime);
        }
    }
}
