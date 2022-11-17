using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected Rigidbody _myRb;
    protected float _currentHP;
    [SerializeField] protected float _maxHP;
    protected void TakeDamage(float damage)
    {
        _currentHP -= damage;
        if (_currentHP <= 0)
            Die();
    }

    protected abstract void Die();
}
