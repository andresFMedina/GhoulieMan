using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 10;
    [SerializeField] private int currentHealth;

    private Rigidbody rb;
    private SphereCollider sphereCollider;
    private AudioSource audioSource;
    public AudioClip killAudio;
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = startingHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.instance.GameOver)
        {
            if(other.CompareTag("PlayerWeapon"))
            {
                takeHit();
            }
        }
    }

    private void takeHit()
    {
        if(currentHealth > 0)
        {
            GameObject newExplosionEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(newExplosionEffect, 1);
            currentHealth -= 10;
        }

        if(currentHealth <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        sphereCollider.enabled = false;
        audioSource.PlayOneShot(killAudio);
        Destroy(gameObject);
    }
}
