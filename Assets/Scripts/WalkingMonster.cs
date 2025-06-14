using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : Entity
{
    public float speed = 1;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool movingRight = true;

    // Задержка перед поворотом
    private float flipCooldown = 0.5f;
    private float lastFlipTime = 0f;

    void Start()
    {
        lives = 3;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Движение
        rb.linearVelocity = new Vector2((movingRight ? 1 : -1) * speed, rb.linearVelocity.y);

        // Проверка столкновений
        bool wallHit = Physics2D.Raycast(wallCheck.position, Vector2.right * (movingRight ? 1 : -1), 0.1f, groundLayer);
        bool groundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);

        // Поворот при стене или обрыве (с задержкой)
        if ((wallHit || !groundAhead) && Time.time > lastFlipTime + flipCooldown)
        {
            Flip();
            lastFlipTime = Time.time;
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}