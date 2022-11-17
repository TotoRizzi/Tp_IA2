using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _timeToDestroy;
    float _currentTimeToDestroy;

    private void Update()
    {
        _currentTimeToDestroy += Time.deltaTime;
        if (_currentTimeToDestroy > _timeToDestroy) Destroy(gameObject);

        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
