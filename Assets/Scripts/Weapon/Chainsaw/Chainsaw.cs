using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class Chainsaw : AbstractWeapon
{
    public float damage = 1;
    public bool isInHand = false;
    public float force = 0;
    public float maxForce = 1;
    public float fadingForce = 0.1f;
    public float startForce = 0.5f;
    public float reloadTime = 1;
    public float fadingPauseTime = 0.1f;
    public float pushForce = 2000;
    public float cooldown = 0.2f;
    public float bloodCost = 5;

    bool canAttack = true;
    bool canReload = true;

    private void Awake()
    {
        StartCoroutine(Fading());
        type = WeaponType.chainsaw;
    }

    private void Update()
    {
        bool reloadBtn = Input.GetButton("Reload");
        if (reloadBtn)
            Reload();
    }

    public override void Attack(IDamagable damagable, Transform enemyTransform)
    {
        if (!isInHand) return;
        if (!canAttack) return;
        if (force == 0) return;
        var bonusDamage = 0f;
        var bonusPushForce = 0f;
        if (bonuses.Keys.Contains("damage"))
        {
            bonusDamage = damage / 100 * bonuses["damage"];
        }
        if (bonuses.Keys.Contains("pushForce"))
        {
            bonusPushForce = pushForce / 100 * bonuses["pushForce"];
        }
        damagable.GetDamage(damage * force + bonusDamage);
        damagable.Push(pushForce * force + bonusPushForce, (enemyTransform.position - transform.position).normalized);
        StartCoroutine(AttackPause());
    }

    public override void Reload()
    {
        if (!canReload) return;
        if (!isInHand) return;
        if (!BloodWallet.instance.CheckBlood(bloodCost)) return;
        AddForce(startForce);
        BloodWallet.instance.RemoveBlood(bloodCost);
        if (force > maxForce) SetForce(maxForce);
        StartCoroutine(StartReload());
    }

    public IEnumerator Fading()
    {
        yield return new WaitForSeconds(fadingPauseTime);
        Fade();
        StartCoroutine(Fading());
    }

    public void Fade()
    {
        if (isInHand && force > 0)
            SubstractForce(fadingForce);
        if (force < 0) SetForce(0);
    }

    public IEnumerator StartReload()
    {
        canReload = false;
        yield return new WaitForSeconds(reloadTime);
        canReload = true;
    }

    public IEnumerator AttackPause()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    public void AddForce(float force)
    {
        this.force += force;
        EventManager.Instance.ChainsawForceChanged(this);
    }

    public void SetForce(float force)
    {
        this.force = force;
        EventManager.Instance.ChainsawForceChanged(this);
    }

    public void SubstractForce(float force)
    {
        this.force -= force;
        EventManager.Instance.ChainsawForceChanged(this);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Damagable"))
        {
            var target = collision.gameObject.GetComponent<IDamagable>();
            Attack(target, collision.transform);
        }
    }
}
