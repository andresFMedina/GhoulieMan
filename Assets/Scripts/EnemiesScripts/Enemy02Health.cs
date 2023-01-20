using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int currentHealth;

    private float timer = 0f;
    private Animator animator;
    private bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy;
    private AudioSource audioSource;
    public AudioClip hurtAudio;
    public AudioClip killAudio;

    private DropItems dropItems;

    public bool IsAlive
    {
        get { return isAlive; }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        isAlive = true;
        dropItems = GetComponent<DropItems>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (dissapearEnemy)
        {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
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
            animator.Play("Hurt");
            currentHealth -= 10;
            audioSource.PlayOneShot(hurtAudio);
        }

        if(currentHealth <= 0)
        {
            capsuleCollider.enabled = false;
            isAlive = false;
            animator.SetTrigger("Die");
            rigidbody.isKinematic = true;
            audioSource.PlayOneShot(killAudio);
            StartCoroutine(removeEnemy());
            dropItems.Drop();
        }
    }

    IEnumerator removeEnemy()
    {
        yield return new WaitForSeconds(dissapearSpeed);
        dissapearEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
