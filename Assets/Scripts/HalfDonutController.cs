using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutController : MonoBehaviour
{
    [SerializeField] float maxDelayTrigger, minDelayTrigger;

    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();

        StartCoroutine(nameof(SetUpStick));
    }

    IEnumerator SetUpStick()
    {
        while (true)
        {
            _animator.SetTrigger("isTriggered");

            yield return new WaitForSeconds(Random.Range(minDelayTrigger, maxDelayTrigger));
        }
    }
}
