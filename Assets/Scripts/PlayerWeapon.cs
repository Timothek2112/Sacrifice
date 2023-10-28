using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public AbstractWeapon weaponInHand;
    public List<AbstractWeapon> weaponInventory;

    public void SetDamage(float damagePercent)
    {
        foreach(var weapon in weaponInventory)
        {
        }
    }

    public void SetReload(float time)
    {
        foreach(var weapon in weaponInventory)
        {
        }
    }

    public void SetCooldown(float cooldown)
    {
        foreach(var weapon in weaponInventory)
        {
        }
    }

    public void SetBloodCost(float cost)
    {
        foreach(var weapon in weaponInventory)
        {
        }   
    }

    void Start()
    {
    }

    void Update()
    {
        
    }
}
