using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private Transform _visualTrm;
    [SerializeField] private Transform _footTrm;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _rayDistance;
    [SerializeField] private float _checkFallTime;

    public bool Active;

    private Animator _animator;
    private Rigidbody _rigid;
    private PlayerHealth _playerHealth;
    private float _flightTime;
    private bool _isFall;

    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Active)
        {
            Move();
            Jump();

            if(!CheckGround())
            {
                Fall();
            }
        }
        PlayerAnimation();
    }

    private void Fall()
    {
        if(_flightTime < _checkFallTime)
        {
            _flightTime += Time.deltaTime;
        }
        else
            _isFall = true;
    }

    private void PlayerAnimation()
    {
        if(CheckGround())
            _animator.SetBool("IsGround", true);
    }

    private void Jump()
    {
        if (CheckGround() && Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
            _animator.SetBool("IsGround", false);
            _rigid.AddForce(Vector3.up * _jumpForce);
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _animator.SetBool("IsMove", horizontal != 0 || vertical != 0);

        /*_animator.SetFloat("MoveX", horizontal);
        _animator.SetFloat("MoveZ", vertical);*/

        Vector3 moveVec = new Vector3(horizontal, 0, vertical);
        if(moveVec != Vector3.zero)
            _visualTrm.rotation = Quaternion.LookRotation(moveVec);

        transform.position += moveVec * _moveSpeed * Time.deltaTime;
    }

    public bool CheckGround()
    {
        if(Physics.Raycast(_footTrm.position, Vector3.down, _rayDistance, _groundLayer))
        {
            _flightTime = 0;

            if(_isFall)
            {
                _isFall = false;
                _playerHealth.Damage(1);
            }

            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(_footTrm.position, Vector3.down * _rayDistance, Color.red);
    }
}
