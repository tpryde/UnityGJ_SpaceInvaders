using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{
    private const string ENEMY_PROJECTILE_KEY = "BASIC_ENEMY";

    [SerializeField] private ProjectileController _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private Custom.Rangef _coolDownRange;
    [SerializeField, Space(5)] private int _health;

    private Custom.ProjectileTokenPool _projectilePool;
    private float _secondsUntilNextShotAttempt;
    private AudioSource enemyAudio;
    public AudioClip explosionSound;

    #region Unity

    private void Awake()
    {
        _projectilePool = Custom.ProjectileTokenPool.RequestReference();
        _secondsUntilNextShotAttempt = _coolDownRange.GetRandom();
    }
    private void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
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

    #endregion

    public void DealDamage(int dmg)
    {
        _health -= dmg;
        if (_health <= 0)
        {
            Destroy(gameObject, 1f);
            enemyAudio.PlayOneShot(explosionSound);
        }
        
    }

    private void AttemptShot()
    {
        ProjectileController controller = _projectilePool.RequestProjectile(_projectilePrefab, _projectileSpawnPoint.position, Quaternion.Euler(0f, 0f, 180f), ENEMY_PROJECTILE_KEY);
        controller?.Initialize(6f, 1);
    }
}
