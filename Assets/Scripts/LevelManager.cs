using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float timer = 0f;
    public float waitTime = 2f;

    public GameObject currentCheckpoint;
    private GameObject player;
    private PlayerHealth playerHealth; 
    private CharacterMovement characterMovement;
    private Animator animator;
    private LifeManager lifeManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        characterMovement = player.GetComponent<CharacterMovement>();
        animator = player.GetComponent<Animator>();
        lifeManager = FindObjectOfType<LifeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            lifeManager.TakeLife();
            player.transform.position = currentCheckpoint.transform.position;
            playerHealth.CurrentHealth = 100;
            print("Respawn");
            timer = 0;
            playerHealth.HealthSlider.value = playerHealth.CurrentHealth;
            characterMovement.enabled = true;
            animator.Play("Blend Tree");
        }
    }
}
