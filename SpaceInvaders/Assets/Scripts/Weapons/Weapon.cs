using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponAttachment
{

};

public class Weapon
{
    private readonly float Base_Cool_Down;
    private readonly float Base_Projectile_Speed;
    private readonly int Base_Projectile_Damage;

    private GameObject _projectilePrefab;
    private float _currentCoolDown;

    private float ProjectileSpeed => Base_Projectile_Speed;
    private int ProjectileDamage => Base_Projectile_Damage;

    private Weapon() { }
    public Weapon(GameObject prefab, float speed, float cooldown, int damage)
    {
        Base_Projectile_Speed = speed;
        Base_Projectile_Damage = damage;
        Base_Cool_Down = cooldown;

        _projectilePrefab = prefab;
    }

    public virtual void Fire(Vector2 position, Quaternion rotation)
    {
        if (OnCoolDown() <= 0f)
        {
            GameObject go = GameObject.Instantiate(_projectilePrefab, position, rotation);
            if (go.TryGetComponent<ProjectileController>(out ProjectileController controller))
            {
                controller.Initialize(ProjectileSpeed, ProjectileDamage);
            }

            _currentCoolDown = Base_Cool_Down;
        }
    }
    public virtual float OnCoolDown() { return _currentCoolDown; }
    public virtual void UpdateWeapon(float delta)
    {
        _currentCoolDown -= delta;
    }

    public void IncludeWeaponAttachment(WeaponAttachment attachment) { }
    public void IncludeWeaponAttachment(WeaponAttachment attachment, float seconds) { }
};
