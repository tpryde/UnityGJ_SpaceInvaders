using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const string ENEMY_PROJECTILE_KEY = "BASIC_ENEMY";

    [SerializeField] private ProjectileController _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;

    private Custom.ProjectileTokenPool _projectilePool;

    private void Awake()
    {
        _projectilePool = Custom.ProjectileTokenPool.RequestReference();
    }

    private void Update()
    {
        
    }

    private void AttemptShot()
    {
        _projectilePool.RequestProjectile(_projectilePrefab, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation, ENEMY_PROJECTILE_KEY);
    }
}
