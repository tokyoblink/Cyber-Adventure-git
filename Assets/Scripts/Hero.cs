using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f; // скорость движения
    [SerializeField] private int lives = 5; // количество жизней
    [SerializeField] private float jumpForce = 8f; // сила прыжка


    private bool isGrounded = false;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    public override void GetDamage()
    {
        lives -= 1;
        Debug.Log(lives);
    }

    //Анимация
    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this; 

    }

    private void Update()
     {
        {
            if (isGrounded) State = States.idle;

            if (Input.GetButton("Horizontal"))
                Run();

            if (Input.GetButtonDown("Jump") && isGrounded)
                Jump();
        }
    }


    // Описание бега
    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;

        if (isGrounded) State = States.run;
    }


    // Описание прыжка
    private void Jump()
    {
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
    jump
}