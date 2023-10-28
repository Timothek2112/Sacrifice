using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class IPickable : MonoBehaviour
{
    public abstract void Pick();
}
