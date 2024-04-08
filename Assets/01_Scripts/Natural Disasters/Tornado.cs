using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    [Header("---------Tornado_Move---------")]
    [SerializeField] private float _speed;
    [SerializeField] private int _repeatNum;

    [Header("---------Tornado_Phsyics---------")]
    [SerializeField] private Transform _tornadoCenter;
    [SerializeField][Range(0,20)] private float _pullForce;

    private Transform _player;
    private Rigidbody _rigid;

    private bool _exit = false;
    private bool isMoving = false;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerHealth>().transform;
        //gameObject.SetActive(false);
    }

    private void Update()
    {
        //일정 확률에 따라 플레이어 근처로 가든지 or 랜덤
        //float per = Random.Range(0, 100);
        //if (per < _percent)
        Vector3 direction = _player.position - transform.position;
        direction.y = 0;

        _rigid.AddForce(direction.normalized * _speed * Time.deltaTime);
    }

    public void OnTornado()
    {
        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        int time = 0;
        while (time < _repeatNum)
        {
            int randomNumber = Random.Range(0, 100);

            if (randomNumber < 30)
            {
                isMoving = true;
                StartCoroutine(MoveToPosition(_player.position));
            }
            else
            {
                isMoving = true;
                Vector3 rand = new Vector3(Random.Range(_player.position.x - 20, _player.position.x + 20), _player.position.y);
                StartCoroutine(MoveToPosition(rand));
            }

            time++;
            yield return new WaitForSeconds(3f);
        }
        Destroy(gameObject);
        NaturalManager.Instace._endPattern = true;
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        while (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 5f * Time.deltaTime);

            if (transform.position == target)
                isMoving = false;

            yield return null;
        }
    }

    #region Tornado_Phsyics
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("PullObj") || obj.CompareTag("Player"))
        {
            _exit = false;
            StartCoroutine(PullObject(obj));
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        if (obj.CompareTag("PullObj") || obj.CompareTag("Player"))
        {
            _exit = true;
        }
    }

    private IEnumerator PullObject(Collider col)
    {
        Vector3 foreDir = _tornadoCenter.position - col.transform.position;
        col.GetComponent<Rigidbody>().AddForce(foreDir.normalized * _pullForce * 100 * Time.deltaTime);
        yield return null;
        if(!_exit)
            StartCoroutine(PullObject(col));
    }
    #endregion
}
