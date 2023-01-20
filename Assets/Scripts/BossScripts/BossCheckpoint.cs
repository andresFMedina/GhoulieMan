using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheckpoint : MonoBehaviour
{
    public BoxCollider boxCollider;
    private BossController bossController;
    private CharacterMovement characterMovement;
    private Animator playerAnimator;
    private SmoothFollow smoothFollow;

    public AudioClip bossMusic;
    private AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        bossController = GameObject.FindGameObjectWithTag("Boss")
            .GetComponent<BossController>();
        characterMovement = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<CharacterMovement>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Animator>();
        smoothFollow = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent <SmoothFollow>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (bossMusic != null)
            {
                audioManager.ChangeMusic(bossMusic);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            boxCollider.isTrigger = false;
            bossController.bossAwake = true;
            characterMovement.enabled = false;
            playerAnimator.Play("Idle");
            smoothFollow.bossCameraActive = true;            
        }
    }
}
