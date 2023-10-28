using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void GetDamage(float damage);
    void Die();
    void Push(float force, Vector2 direction);
}
