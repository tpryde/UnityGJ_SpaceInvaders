﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float _remainingLifeTime = 3f; // Measured in scaled seconds;
    private float _projectileSpeed;
    private int _projectileDamage;

    #region Unity

    private void FixedUpdate()
    {
        float scaledDeltaTime = Custom.GameData.GameTimeDelta;

        transform.position += scaledDeltaTime * _projectileSpeed * transform.up;
        _remainingLifeTime -= scaledDeltaTime;

        if(_remainingLifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.DealDamage(_projectileDamage);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {

    }

    #endregion

    public void Initialize(float speed, int damage)
    {
        _projectileSpeed = speed;
        _projectileDamage = damage;
    }
}