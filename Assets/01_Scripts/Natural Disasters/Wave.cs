using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private Transform _playerTrm; // Vector3.zero; 인데 음..
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateOffset = 90f;
    [SerializeField] private float _waveForce;

    public bool ActiveMove;

    private Vector3 _moveDir;

    public void SetRandomDir()
    {
        // 1. 원 밖에서 생성 
        float theta = Random.Range(0f, 360f);
        float radianTheta = theta * Mathf.Deg2Rad;
        float xPos =  _radius * Mathf.Cos(radianTheta);
        float yPos =  _radius * Mathf.Sin(radianTheta);
        transform.position = new Vector3(xPos, transform.position.y, yPos);

        // 2. 중심을 보게 회전
        transform.rotation = Quaternion.Euler(-60f, -theta + _rotateOffset, 0f);
        Vector3 moveVec = _playerTrm.position - transform.position;
        _moveDir = new Vector3(moveVec.x, 0, moveVec.z);
        _moveDir.Normalize();
        ActiveMove = true;                          // 나중에 시점을 나중으로 뭔가 할듯
    }

    private void Update()
    { 
        if(ActiveMove)
            Move();
    }

    private void Move()
    {
        transform.position += _moveDir * _moveSpeed * Time.deltaTime;

        if(Vector3.Distance(Vector3.zero, transform.position) > 150f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Rigidbody result))
        {
            result.AddForce(_moveDir * _waveForce, ForceMode.Impulse);
            if(other.TryGetComponent(out PlayerMovement player))
            {
                player.Active = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement player))
        {
            player.Active = true;
        }
    }
}
