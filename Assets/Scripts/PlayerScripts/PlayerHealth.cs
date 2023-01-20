using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2.0f;
    [SerializeField] int currentHealth;
    [SerializeField] private float timer = 0.0f;
    [SerializeField] Slider healthSlider;
    private Animator animator;

    private CharacterMovement characterMovement;
    private AudioSource audioSource;
    private ParticleSystem particleSystem;
    private LevelManager levelManager;

    public bool isDead;

    internal void KillBox()
    {
        currentHealth = 0;
        healthSlider.value = 0;
    }

    public AudioClip hurtAudio;
    public AudioClip deathAudio;
    public AudioClip pickItem;

    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (value < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    public Slider HealthSlider { get { return healthSlider; } }

    public void PowerUpHealth()
    {
        if(currentHealth <= 80)
        {
            currentHealth += 20;
        } else if(currentHealth < startingHealth)
        {
            CurrentHealth = startingHealth;
        }

        healthSlider.value = currentHealth;
        audioSource.PlayOneShot(pickItem);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        currentHealth = startingHealth;
        characterMovement = GetComponent<CharacterMovement>();
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        PlayerKill();
    }

    public void TakeHit()
    {
        if (currentHealth > 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            animator.Play("murderer_hurt");
            currentHealth -= 10;
            healthSlider.value = currentHealth;
            audioSource.PlayOneShot(hurtAudio);
        }

        if (currentHealth <= 0)
        {
            //GameManager.instance.PlayerHit(currentHealth);
            animator.SetTrigger("isDead");
            characterMovement.enabled = false;
            audioSource.PlayOneShot(deathAudio);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if (other.tag == "Weapon")
            {
                TakeHit();
                timer = 0;
            }
        }
    }

    public void PlayerKill()
    {
        if(currentHealth <= 0)
        {
            //audioSource.PlayOneShot(deathAudio);
            characterMovement.enabled = false;
            levelManager.RespawnPlayer();
        }
    }
}
