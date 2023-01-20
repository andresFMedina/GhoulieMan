using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float disapearSpeed = 2f;
    [SerializeField] private int currentHealth;
    // Start is called before the first frame update

    private float timer = 0f;
    private Animator animator;
    private NavMeshAgent navMesh;
    private bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy;
    private BoxCollider weaponCollider;
    private AudioSource audioSource;

    public AudioClip hurtAudio;
    public AudioClip deathAudio;

    public bool IsAlive
    {
        get { return isAlive; }
    }

    void Start()
    {
        weaponCollider = GetComponentInChildren<BoxCollider>();
        animator = GetComponent<Animator>();
        navMesh =  GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        isAlive = true;
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (dissapearEnemy)
        {
            transform.Translate(-Vector3.up * disapearSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "PlayerWeapon")
            {
                takeHit();
                timer = 0f;
            }
        }
    }

    private void takeHit()
    {
        if(currentHealth > 0)
        {
            animator.Play("hurt");
            currentHealth -= 10;
            audioSource.PlayOneShot(hurtAudio);
        }

        if(currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        capsuleCollider.enabled = false;
        navMesh.enabled = false;
        animator.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        weaponCollider.enabled = false;
        audioSource.PlayOneShot(deathAudio);
        StartCoroutine(removeEnemy());
    }

    IEnumerator removeEnemy()
    {
        yield return new WaitForSeconds(disapearSpeed);
        dissapearEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
