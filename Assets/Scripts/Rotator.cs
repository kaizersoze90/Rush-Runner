using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] float rotateStrenght;

    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;

            other.gameObject.GetComponent<Rigidbody>().AddForce
                (new Vector3(rotateStrenght, 0f, 0f), ForceMode.Acceleration);
        }
        else if (other.gameObject.CompareTag("Opponent"))
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;

            other.gameObject.GetComponent<Rigidbody>().AddForce
                (new Vector3(rotateStrenght / 2f, 0f, 0f), ForceMode.Acceleration);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player") ||
            other.gameObject.CompareTag("Opponent"))
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
