using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove01 : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent navMesh;
    private Animator animator;
    private Enemy01Health enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<Enemy01Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 12)
        {
            if (!GameManager.instance.GameOver && enemyHealth.IsAlive)
            {
                navMesh.SetDestination(player.position);
                animator.SetBool("isWalking", true);
                animator.SetBool("isIdle", false);
            }
        }
        else if (GameManager.instance.GameOver || !enemyHealth.IsAlive)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", true);
            navMesh.enabled = false;
        }
    }
}
