using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private Animator playerAnimator;
    private CharacterMovement characterMovement;
    private BossController bossController;


    private void Awake()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Animator>();
        characterMovement = GameObject.FindGameObjectWithTag("Player")
            .GetComponent <CharacterMovement>();
        bossController = GameObject.FindGameObjectWithTag("Boss")
            .GetComponent<BossController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableBossBattle()
    {
        characterMovement.enabled = true;
        bossController.isInBattle = true;
        playerAnimator.Play("Blend Tree");
    }
}
