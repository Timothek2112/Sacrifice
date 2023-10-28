using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Goblin : MonoBehaviour, IDamagable
{
    [SerializeField] float HP = 1;
    float speed = 1;
    Rigidbody2D body;
    [SerializeField] GameObject attackFx;
    [SerializeField] List<GameObject> drop = new List<GameObject>();
    public int dropCount = 5;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void GetDamage(float damage)
    {
        HP-=damage;
        var attackFxCreated = Instantiate(attackFx);
        attackFxCreated.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        if (HP <= 0)
            Die();
    }

    public void Die()
    {
        for(int i = dropCount; i > 0; i--)
        {
            var dropItem = Instantiate(drop[Random.Range(0, drop.Count)]);
            dropItem.transform.position = transform.position;
            dropItem.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * 100, ForceMode2D.Impulse);
        }
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        body.velocity *= 0.9f;
    }

    public void Push(float force, Vector2 direction)
    {
        if (force > 1000)
            force = 1000;
        body.AddForce(direction.normalized * force, ForceMode2D.Force);
    }
}
