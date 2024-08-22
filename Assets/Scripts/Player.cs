using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 moveVector;
    [SerializeField] private float speed;
    [SerializeField] private float jumpforce;

    //¿Õ»Ã¿÷»»
    public Animator anim;
    private int state = 0;
    // 0 - stand, 1 - run, 2 - jump

    //«ƒŒ–Œ¬‹≈
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private int armor = 0;

    //public int HP
    //{
    //    get { return health; }
    //}

    private Rigidbody2D rb;

    // GROUND
    public bool OnGround;
    public Transform WhereGround;
    public float radius = 0.5f;
    public LayerMask Ground;

    // CROUCH
    public Collider2D stand;

    private bool isTouched;

    // DAMAGE
    //_________________________________¬¬Œƒ » Œ¡ÕŒ¬Õ≈Õ»≈_________________________________//

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetBool("SwordAttack", true);
        anim.SetBool("Shoot", false);

        health = maxHealth;
        EventManager.onChangePlayerHealth?.Invoke(health);
    }

    void Update()
    {
        if (health <= 0 || transform.position.y < -30f)
        {
            Die();
        }

        FindGround();
        if (Input.GetMouseButtonDown(0)) { state = 3; }
        else if (Input.GetKeyDown(KeyCode.Space) && OnGround) { Jump(); }
        else if (Input.GetAxis("Horizontal") != 0 && (OnGround || isTouched)) { Run(); }
        else if (Input.GetAxis("Horizontal") == 0 && OnGround) { state = 0; }

        Flip();
       
    }

    private void LateUpdate()
    {
        SetAnimation();
       
    }


    void OnEnable()
    {
        EventManager.onSelectWeapon += SetWeaponAnimation;
        EventManager.onAllowShoot += isAllowShoot;
        EventManager.onPick += SetHealth;
        EventManager.onPick += SetArmor;

        EventManager.onChangePlayerHealth?.Invoke(maxHealth);
    }
    void OnDisable()
    {
        EventManager.onSelectWeapon -= SetWeaponAnimation;
        EventManager.onAllowShoot -= isAllowShoot;
        EventManager.onPick -= SetHealth;
        EventManager.onPick -= SetArmor;
    }

    //____________________________________Ã≈’¿Õ» »____________________________________//

    void Run()
    {
        state = 1;
        moveVector.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(0, jumpforce);
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void FindGround()
    {
        OnGround = Physics2D.OverlapCircle(WhereGround.position, radius, Ground);
        if (!OnGround) state = 2;
    }
    void EndAttack()
    {
        anim.SetBool("isAttack", false);
    }

    void SetAnimation()
    {
        switch (state)
        {
            case 0:
                anim.SetBool("ParamGround", true);
                anim.SetFloat("moveX", 0);
                break;
            case 1:
                anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
                anim.SetBool("ParamGround", OnGround);
                break;
            case 2:
                anim.SetBool("ParamGround", OnGround);
                anim.SetFloat("moveX", 0);
                break;
            case 3:
                anim.SetBool("isAttack", true);
                Invoke("EndAttack", 0.2f);
                break;
        }
    }

    void isAllowShoot(bool permission)
    {
        anim.SetBool("bulletsAvailable", permission);
    }

    void SetWeaponAnimation(int weaponState)
    {
        switch (weaponState)
        {
            case 0:
                anim.SetBool("SwordAttack", true);
                anim.SetBool("Shoot", false);
                break;
            case 1:
                anim.SetBool("Shoot", true);
                anim.SetBool("SwordAttack", false);
                break;
        }
    }
    void GetDamage(int damage)
    {
        health -= damage;
        EventManager.onChangePlayerHealth?.Invoke(health);
    }

    void SetHealth (string tag, int num)
    {
        if (tag == "pickHP" && (health + num) <= maxHealth)
        {
            health += num;
            EventManager.onChangePlayerHealth?.Invoke(health);
        }
        else if (tag == "pickHP" && (health + num) > maxHealth)
        {
            health = maxHealth;
            EventManager.onChangePlayerHealth?.Invoke(health);
        } 
    }
    void SetArmor (string tag, int num)
    {
        if (tag == "pickArmor")
        {
            armor = num;
            EventManager.onSetArmor?.Invoke(armor);
        }
    }

    void Die()
    {
        health = 0;
        EventManager.onChangePlayerHealth?.Invoke(health);
        EventManager.onPlayerDeath?.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetDamage(collision.gameObject.GetComponent<Enemy>().Damage - armor);
            armor = 0;
            EventManager.onSetArmor?.Invoke(armor);
            isTouched = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isTouched = false;
        }
    }

}
