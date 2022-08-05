using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float rotateSpeed;
    [SerializeField] Transform target;

    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
