using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, IDamagable
{
    [SerializeField] private WeaponAttachment _weaponAttachment;
    [SerializeField] private int _health = 1;

    public WeaponAttachment WeaponAttachment => _weaponAttachment;

    public bool DealDamage(int dmg)
    {
        _health -= dmg;
        if(_health <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
