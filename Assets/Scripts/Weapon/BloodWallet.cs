using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BloodWallet
{
    public static BloodWallet _instance;
    public static BloodWallet instance
    {
        get
        {
            if( _instance == null )
                _instance = new BloodWallet();
            return _instance;
        }
    }

    public float _blood = 0;
    public float blood
    {
        get
        {
            return _blood;
        }
        set
        {
            _blood = value;
            EventManager.Instance.BloodChanged(value, maxBlood);
        }
    }

    public float maxBlood = 100;
    
    public BloodWallet()
    {
        blood = 0;
    }

    public void AddBlood(float blood)
    {
        this.blood += blood;
        if(this.blood > maxBlood)
            this.blood = maxBlood;
    }

    public void RemoveBlood(float blood)
    {
        this.blood -= blood;
    }

    public bool CheckBlood(float blood)
    {
        return this.blood >= blood;
    }
}
