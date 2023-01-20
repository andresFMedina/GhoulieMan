using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave01Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 50;
    [SerializeField] private float timeSinceLastHit = 0.2f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int currentHealth;

    private float timer = 0f;
    private Animator animator;
    private bool isAlive;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private bool dissapearEnemy;
    private AudioSource audioSource;
    public AudioClip hurtAudio;
    public AudioClip killAudio;
    private DropItems dropItems;
    public GameObject explosionEffect;

    public bool IsAlive
    {
        get { return isAlive; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
        audioSource = GetComponent<AudioSource>();
        dropItems = GetComponent<DropItems>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
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
            animator.Play("Hurt");
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
        GameObject newExplosionEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(newExplosionEffect, 1);
        boxCollider.enabled = false;
        animator.SetTrigger("EnemyDie");
        audioSource.PlayOneShot(killAudio);

        StartCoroutine(removeEnemy());
        dropItems.Drop();
    }

    private IEnumerator removeEnemy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
