using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // скорость движения
    [SerializeField] private int lives = 5; // количество жизней
    [SerializeField] private float jumpForce = 8f; // сила прыжка


    private bool isGrounded = false;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;


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

    }

    private void Update()
     {
        if (Input.GetButton("Fire3"))
        {
            Sit();
        }
        else
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
    
    // Описание Приседания
    private void Sit()
    {
        State = States.sit;
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
    sit
}