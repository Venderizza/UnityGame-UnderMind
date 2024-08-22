using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject damagePoint;
    [SerializeField] private float attackDelay = 0.6f;
    private float delay = 0.0f;

    public int Damage
    {
        get { return damage; }
    }

    [SerializeField] private AudioSource swordSound;

    void Update()
    {
        CheckAttack();
    }

    private void CheckAttack()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1") && delay <= 0)
        {
            Attack();
            delay = attackDelay;
        }

    }

    void Attack()
    {
        damagePoint.SetActive(true);
        Invoke("EndAttack", 0.3f);
        swordSound.Play();
    }
    void EndAttack()
    {
        damagePoint.SetActive(false);
    }
}
