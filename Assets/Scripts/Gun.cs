using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //ÑÒÐÅËÜÁÀ
    [SerializeField] private GameObject bullet;
    private int bullets;
    [SerializeField] private int maxBullets = 20;

    [SerializeField] private float attackDelay = 1.0f;
    [SerializeField] private AudioSource shotSound;
    private float delay;
    private bool shootPermission;

    //public AudioSource shotSound;

    private void Start()
    {
        bullets = maxBullets;
        EventManager.onChangeNumbOfBullets?.Invoke(maxBullets);
    }
    void Update()
    {
        CheckAttack();
    }

    private void OnEnable()
    {
        EventManager.onPick += RefillBullets;
        
    }
    private void OnDestroy()
    {
        EventManager.onPick -= RefillBullets;
    }
    private void CheckAttack()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1") && delay <= 0 && bullets > 0)
        {
            shootPermission = true;

            Shoot();
            delay = attackDelay;
            bullets -= 1;
            EventManager.onChangeNumbOfBullets?.Invoke(bullets);
        }
        if (bullets <= 0)
        {
            shootPermission = false;
        }

        EventManager.onAllowShoot?.Invoke(shootPermission);

    }

    void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        shotSound.Play();
    }

    void RefillBullets(string tag, int num)
    {
        if (tag == "pickBullet" && (bullets + num) <= maxBullets)
        {
            bullets += num;
            EventManager.onChangeNumbOfBullets?.Invoke(bullets);
        }
        else if (tag == "pickBullet" && (bullets + num) > maxBullets)
        {
            bullets = maxBullets;
            EventManager.onChangeNumbOfBullets?.Invoke(bullets);
        } 
    }
}
