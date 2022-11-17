using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    private void Start()
    {
        _myRb = GetComponent<Rigidbody>();
    }
    protected override void Die()
    {
        Destroy(gameObject);
    }
}
