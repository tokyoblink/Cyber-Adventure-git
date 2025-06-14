using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackCooldown = 4f;
    private float lastAttackTime;
    public int damage = 1;

    private bool playerInRange = false;
    private Hero target;

    private void Update()
    {
        if (playerInRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        if (target != null)
        {
            target.GetDamage();
            Debug.Log("Враг атаковал игрока");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            target = other.GetComponent<Hero>();
            Debug.Log("Игрок в зоне врага");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            target = null;
            Debug.Log("Игрок вышел из зоны врага");
        }
    }
}
