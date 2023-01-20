using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Attack : MonoBehaviour
{
    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator animator;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider weaponCollider;
    private Enemy01Health enemyHealth;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameManager.instance.Player;
        weaponCollider = GetComponentInChildren<BoxCollider>();
        weaponCollider.enabled = false;
        enemyHealth = GetComponent<Enemy01Health>();
        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        print("Player in range " + playerInRange);
    }

    IEnumerator attack()
    {
        if(playerInRange && !GameManager.instance.GameOver)
        {
            weaponCollider.enabled = true;
            animator.SetTrigger("isAttacking");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        weaponCollider.enabled = false;
        yield return null;
        StartCoroutine(attack());
    }
}
