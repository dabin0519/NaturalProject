using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NaturalManager : MonoBehaviour
{
    public static NaturalManager Instace;
    public bool _endPattern;

    [SerializeField] private GameObject Tornado;

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
            int r = Random.Range(0, 3);
            _endPattern = false;
            switch (r)
            {
                case 0:
                    _earthquake.OnQuake();
                    Debug.Log("지진이다!!");
                    break;
                case 1:
                    Debug.Log("회오리다!!");
                    GameObject t = Instantiate(Tornado, new Vector3(Random.Range(-20, 20), 0), Quaternion.identity);
                    _tornado = t.GetComponent<Tornado>();
                    _tornado.OnTornado();
                    break;
                default:
                    Debug.Log("메테오다!!");
                    _spawnMeteor.OnMeteor();
                    break;
            }

            yield return new WaitUntil(() => _endPattern == true);
        }
    }
}
