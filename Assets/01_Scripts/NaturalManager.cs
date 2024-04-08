using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NaturalManager : MonoBehaviour
{
    public static NaturalManager Instace;
    public bool _endPattern;

    [SerializeField] private GameObject Tornado;
    [SerializeField] private Wave _wave;

    private Earthquake _earthquake;
    private Tornado _tornado;
    private SpawnMeteor _spawnMeteor;

    private void Awake()
    {
        if(Instace == null)
        {
            Instace = this;
        }
        else
        {
            Debug.LogError("이거 두개인데요?");
        }

        _earthquake = FindObjectOfType<Earthquake>();
        _spawnMeteor = FindObjectOfType<SpawnMeteor>();
    }

    private void Start()
    {
        StartCoroutine(Pattern());
    }

    private IEnumerator Pattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            int r = Random.Range(0, 1000);
            _endPattern = false;
            switch (r % 4)
            {
                case 0:
                    _earthquake.OnQuake();
                    break;
                case 1:
                    GameObject t = Instantiate(Tornado, new Vector3(Random.Range(-20, 20), 0), Quaternion.identity);
                    _tornado = t.GetComponent<Tornado>();
                    _tornado.OnTornado();
                    break;
                case 2:
                    _spawnMeteor.OnMeteor();
                    break;
                case 3:
                    Wave w = Instantiate(_wave, new Vector3(200, 200, 0), Quaternion.identity);
                    w.SetRandomDir();
                    break;
            }

            yield return new WaitUntil(() => _endPattern == true);
        }
    }
}
