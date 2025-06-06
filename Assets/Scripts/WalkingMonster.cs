using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : Entity
{
    private float speed = 2f;
    private Vector3 dir;
    private SpriteRenderer sprite;

    private void Start()
    {
        dir = transform.right; // направление вправо
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Проверка на наличие препятствий спереди (с учётом направления)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            transform.position + Vector3.up * 0.1f + dir * 0.7f,
            0.1f
        );

        if (colliders.Length > 0)
        {
            dir *= -1f; // разворот при препятствии
            if (sprite != null)
                sprite.flipX = dir.x < 0; // разворот спрайта
        }

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }
}
