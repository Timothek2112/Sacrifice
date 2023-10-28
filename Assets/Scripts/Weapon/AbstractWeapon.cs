using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    public WeaponType type;
    public Dictionary<string, float> bonuses = new Dictionary<string, float>();

    public abstract void Attack(IDamagable damagable, Transform enemyTransform);
    public abstract void Reload();
}
