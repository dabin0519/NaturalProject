using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private float _groundYPos;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(transform.position.y < _groundYPos)
        {
            Destroy(gameObject);
        }
    }

    private void Method()
    {
        //_rigidbody.AddForce();
    }
}
