using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Earthquake : MonoBehaviour
{
    private Vector3 _curretPos;

    private void Awake()
    {
        _curretPos = transform.position;
    }

    public void OnQuake()
    {
        StartCoroutine(Quake());
    }

    private IEnumerator Quake()
    {
        for(int i = 0; i < 10; i++)
        {
            transform.DOShakePosition(3,3);
            yield return new WaitForSeconds(1f);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        NaturalManager.Instace._endPattern = true;
        transform.position = _curretPos;
    }
}
