using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed = 10;

    [SerializeField] Rigidbody2D bulletRB;
    [SerializeField] private float destroyTime = 5f;

    public int Damage
    {
        get { return damage; }
    }

    void Start()
    {
        bulletRB.velocity = -transform.right * speed;
        Invoke("Destroy", destroyTime);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy();
    }
}
