using UnityEngine;

public class Volcano : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private Lava _lavaObject;
    [SerializeField] private Transform _topMountainTrm;

    private float _time;

    private void Update()
    {
        if(_time < _spawnTime)
            _time += Time.deltaTime;
        else
        {
            Spawn();
            _time = 0;
        }
    }

    private void Spawn()
    {
        Instantiate(_lavaObject, _topMountainTrm.position, Quaternion.identity);
    }
}
