using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float touchSensitivity;
    [SerializeField] float controlSensitivity;
    [SerializeField] float screenXLimit;

    float _newPosX;
    float _newPosY;
    float _rawDeltaX;
    float _deltaX;
    float _startSpeed;
    bool _onRotator;
    bool _isPullingBack;
    bool _isAligningPos;

    Animator _animator;

    void Start()
    {
        _startSpeed = moveSpeed;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isAligningPos)
        {
            AdjustPosition();
        }

        if (!GameManager.Instance.IsGameActive || _isPullingBack) { return; }

        ProcessInput();
        ProcessMove();
    }

    public void SetDanceAnim()
    {
        _animator.SetBool("isDancing", true);
    }

    public void SetIdleAnim()
    {
        _animator.SetBool("isIdle", true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragging"))
        {
            _onRotator = !_onRotator;
        }
        else if (other.CompareTag("Finish"))
        {
            GameManager.Instance.ProcessVictory();

            _isAligningPos = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.ProcessReloadGame();
        }
        else if (other.gameObject.CompareTag("Stick"))
        {
            StartCoroutine(nameof(Knockback));
        }
    }

    IEnumerator Knockback()
    {
        _isPullingBack = true;

        yield return new WaitForSeconds(0.6f);

        _isPullingBack = false;
    }

    void ProcessMove()
    {
        _newPosY = transform.position.z + moveSpeed * Time.deltaTime;
        transform.position = new Vector3(_newPosX, transform.position.y, _newPosY);
    }

    void ProcessInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _rawDeltaX = Input.GetTouch(0).deltaPosition.x * touchSensitivity;
        }
        else if (Input.GetMouseButton(0))
        {
            _rawDeltaX = Input.GetAxis("Mouse X");
        }
        else
        {
            _rawDeltaX = 0;
        }


        _deltaX = Mathf.Lerp(_deltaX, _rawDeltaX, Time.deltaTime * controlSensitivity);

        _newPosX = transform.position.x + turnSpeed * _deltaX * Time.deltaTime;

        if (_onRotator) { return; }
        _newPosX = Mathf.Clamp(_newPosX, -screenXLimit, screenXLimit);
    }

    void AdjustPosition()
    {
        Vector3 alignPos = new Vector3(0f, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, alignPos, 0.01f);

        if (transform.position.x == 0f)
        {
            _isAligningPos = false;
        }
    }

}
