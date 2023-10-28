using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPart : IPickable
{
    public float blood = 10;
    public float stopFactor = 0.95f;

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity *= stopFactor;
    }

    public override void Pick()
    {
        BloodWallet.instance.AddBlood(blood);
        Destroy(gameObject);
    }
}
