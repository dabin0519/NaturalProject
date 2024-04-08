using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Transform _playerTrm;

    [SerializeField] private bool _onDamage;
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = _playerTrm.GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _onDamage = false;
        }

        if (collision.gameObject.CompareTag("Player") && _onDamage)
        {
            _playerHealth.Damage(_damage);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _onDamage = true;
        }
    }
}
