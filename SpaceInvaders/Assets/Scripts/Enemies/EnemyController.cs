using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const string ENEMY_PROJECTILE_KEY = "BASIC_ENEMY";

    [SerializeField] private ProjectileController _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private Custom.Rangef _coolDownRange;

    private Custom.ProjectileTokenPool _projectilePool;
    private float _secondsUntilNextShotAttempt;

    private void Awake()
    {
        _projectilePool = Custom.ProjectileTokenPool.RequestReference();
        _secondsUntilNextShotAttempt = _coolDownRange.GetRandom();
    }

    private void Update()
    {
        _secondsUntilNextShotAttempt -= Custom.GameData.GameTimeDelta;
        if(_secondsUntilNextShotAttempt <= 0)
        {
            AttemptShot();
            _secondsUntilNextShotAttempt = _coolDownRange.GetRandom();
        }
    }

    private void AttemptShot()
    {
        ProjectileController controller = _projectilePool.RequestProjectile(_projectilePrefab, _projectileSpawnPoint.position, Quaternion.Euler(0f, 0f, 180f), ENEMY_PROJECTILE_KEY);
        controller?.Initialize(6f, 1);
    }
}
