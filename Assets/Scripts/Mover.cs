using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] Vector3 targetPos;
    [SerializeField] float period;

    Vector3 _startPos;
    float _movementFactor;

    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }    //Safer way instead of (period ==0)
        float cycles = Time.time / period;      //Continually growing over time

        const float tau = Mathf.PI * 2;     //constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);     //going from -1 to 1

        _movementFactor = (rawSinWave + 1f) / 2;    //Recalculate to return the value from 0 to 1

        Vector3 offset = targetPos * _movementFactor;
        transform.position = _startPos + offset;
    }
}
