using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private ParticleSystem _boomEffect;

    private void Update()
    {
        transform.Find("Meteor").rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.CompareTag("PullObj")) 
        {
            Destroy(gameObject);
            Instantiate(_boomEffect, transform.position, Quaternion.identity);
        }
    }
}
