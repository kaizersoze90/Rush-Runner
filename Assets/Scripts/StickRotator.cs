using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickRotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] float knockbackStrenght;
    [SerializeField] Vector3 rotateDir;

    Vector3 _parentPos;

    void Start()
    {
        _parentPos = GetComponentInParent<Transform>().position;
    }

    void Update()
    {
        transform.RotateAround(_parentPos, rotateDir, rotateSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") ||
            other.gameObject.CompareTag("Opponent"))
        {
            Vector3 dir = (other.transform.position - other.GetContact(0).point).normalized;
            dir.y = 0f;

            other.gameObject.GetComponent<Rigidbody>().AddForce
                (dir * knockbackStrenght, ForceMode.Impulse);
        }
    }
}
