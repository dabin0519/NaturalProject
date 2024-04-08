using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    [SerializeField] private GameObject _meteor;

    public void OnMeteor()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for(int i = 0; i < 60; i++)
        {
            Instantiate(_meteor, new Vector3(Random.Range(-10, 10), 20, Random.Range(-10, 10)), Quaternion.identity);
            if (i % 3 == 0)
                yield return new WaitForSeconds(1f);
            else
                yield return null;
        }
        NaturalManager.Instace._endPattern = true;
    }
}
