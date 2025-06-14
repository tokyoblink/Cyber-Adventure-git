using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Entity
{

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource attackMob;
    [SerializeField] private float speed = 4f; // скорость движения
    [SerializeField] private int health = 5; // количество жизней
    [SerializeField] private float jumpForce = 8f; // сила прыжка
    public bool isGrounded = false;


    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;

    public bool isAttacking = false;
    public bool isRecharged = true;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;


    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    //Анимация
    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    private void Awake()
    {
        lives = 5;
        health = lives;
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        isRecharged = true;
    }


    public override void GetDamage()
    {
        health -= 1;
        Debug.Log("Получен урон! Осталось жизней: " + health);
        if (health == 0)
        {
            foreach (var h in hearts)
                h.sprite = deadHeart;
            Die();
        }
    }

    private void Attack()
    {
        if (isGrounded && isRecharged)
        {
            State = States.attack;
            isAttacking = true;
            isRecharged = false;


            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.4f);
        isRecharged = true;
    }



private void OnDrawGizmos()
{
    if (attackPos == null) return;

    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(attackPos.position, attackRange);
}

    public void OnAttack()
{
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position,
        attackRange, enemy);

        if (colliders.Length == 1)
            attackMob.Play();
    
    for (int i = 0; i < colliders.Length; i++)
            {
                Entity entity = colliders[i].GetComponent<Entity>();
                if (entity != null && entity != this)
                    entity.GetDamage();
            }
}

    private void Update()
    {
         if (!isAttacking && Input.GetButton("Horizontal"))
            Run();
        else if (isGrounded && !isAttacking)
            State = States.idle;

        if (!isAttacking && isGrounded && Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetButtonDown("Fire1"))
            Attack();

        if (health > lives)
            health = lives;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;

            if (i < lives)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position,
        transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
        State = States.run;
    }

    // Описание прыжка
    private void Jump()
    {
        jumpSound.Play();
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        State = States.jump;
        isGrounded = false;
    }

    // Проверка на землю (для прыжка)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

}

//Анимация
public enum States
{
    idle,
    run,
    jump,
    attack
}