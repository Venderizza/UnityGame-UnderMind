using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private int points;
    [SerializeField] private Animator anim;

    private Transform target;

    public int Damage
    {
        get { return damage; }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        if (FindObjectOfType<Player>() != null)
            target = FindObjectOfType<Player>().transform;

        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0) Destroy(gameObject);
        if (target != null)
        {
            Move();
            Flip();
        }

        if (transform.position.y < -30f) Destroy(gameObject);
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }


    public void GetDamage(int damage)
    {
        health -= damage;
    }

    void Flip()
    {
        if (target.position.x > transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (target.position.x < transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string objTag = collision.gameObject.tag;
        switch (objTag)
        {
            case "Bullet":
                GetDamage(collision.GetComponent<Bullet>().Damage);
                break;
            case "Sword":
                GetDamage(collision.GetComponentInParent<Sword>().Damage);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isAttack", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isAttack", false);
        }
    }

    private void OnDestroy()
    {
        if (health <= 0)
        {
            EventManager.onKilledEnemy?.Invoke(points);
        }
        EventManager.onDeathOfEnemy?.Invoke();
    }
}
