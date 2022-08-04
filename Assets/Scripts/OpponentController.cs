using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentController : MonoBehaviour
{
    [SerializeField] Transform target;

    NavMeshAgent _agent;
    Vector3 _startPos;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(target.position);

        _startPos = transform.position;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(nameof(ProcessHit));
        }
    }

    IEnumerator ProcessHit()
    {
        _agent.enabled = false;
        transform.position = _startPos;

        yield return new WaitForSeconds(0.5f);

        _agent.enabled = true;
        _agent.SetDestination(target.position);
    }
}
