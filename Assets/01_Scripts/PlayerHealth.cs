using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;
    [SerializeField] private Material _material;
    [SerializeField] private SkinnedMeshRenderer[] _mesh;

    private Animator _playerAnim;
    private PlayerMovement _playerMove;
    private Material _currentMat;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMovement>();
        _playerAnim = GetComponentInChildren<Animator>();
        _currentMat = _mesh[0].material;
        
        _health = _maxHealth;
    }

    public void Damage(int damage)
    {
        if (_health > 0)
        {
            StopCoroutine(OnDamage());
            _health -= damage;
            _playerAnim.SetTrigger("Damage");
            StartCoroutine(OnDamage());
        }
        else
        {
            StopAllCoroutines();
            _playerAnim.SetTrigger("Die");
            _playerMove.Active = false;
        }
    }

    private IEnumerator OnDamage()
    {
        for(int i = 0; i < 3f; i++)
        {
            foreach(var mat in _mesh)
            {
                mat.material = _material;
            }

            yield return new WaitForSeconds(0.1f);

            foreach(var mat in _mesh)
            {
                mat.material = _currentMat;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
