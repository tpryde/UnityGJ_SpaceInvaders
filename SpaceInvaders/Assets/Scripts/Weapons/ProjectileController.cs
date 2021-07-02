using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private System.Action<IDamagable, bool> _onCollision;
    
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
            bool wasDestroyed = damagable.DealDamage(_projectileDamage);
            _onCollision?.Invoke(damagable, wasDestroyed);
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

    public void ListenForCollision(System.Action<IDamagable, bool> callback)
    {
        _onCollision = callback;
    }
}