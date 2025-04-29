using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private Transform player;
    private Animator enemy;
    private bool isAttacking = false;

    private void Start()
    {
        enemy = GetComponent<Animator>();
    }


    public void EnemyAttack()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= 2 && !isAttacking)
        {
            isAttacking = true;
            Debug.Log("Vurduk!");
            enemy.SetTrigger("Attack");
         
        }
        else 
        {
            isAttacking=false;
            enemy.ResetTrigger("Attack");

        }
       

    }
    private void Update()
    {
        Vector3 direction = player.position - transform.position;

        if (direction.x > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0); 
        else
            transform.localRotation = Quaternion.Euler(0, 180, 0); ;
    }
}
