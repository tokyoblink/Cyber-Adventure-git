using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0 : Entity
{

    private void Start()
    {
        lives = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives--;
            Debug.Log("у объекта " + lives);
        }

        if (lives < 1)
            Die();
    }
}