using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int bossHealth = 20;
    public int bossMaxHealth = 20;
    private Animator animator;
    public bool isDead;
    public BossController bossController;

    private CapsuleCollider bossCollider;
    private BoxCollider swordCollider;
    private SphereCollider triggerCollider;

    public Material hurtBossMaterial;
    private GameObject bossModel;

    public AudioClip victoryMusic;
    private AudioManager audioManager;

    public GameObject videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.SetActive(false);
        animator = GameObject.Find("Boss").GetComponent<Animator>();
        bossController = GameObject.Find("Boss").GetComponent<BossController>();
        bossCollider = GameObject.Find("Boss").GetComponent<CapsuleCollider>();
        swordCollider = GameObject.Find("Boss").GetComponentInChildren<BoxCollider>();
        triggerCollider = GameObject.Find("Boss").GetComponentInChildren<SphereCollider>();
        bossModel = GameObject.FindGameObjectWithTag("BossModel");
        audioManager = FindObjectOfType<AudioManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon") && bossHealth > 0)
        {
            animator.SetTrigger("isHit");
            bossHealth--;
            float lifePercentage = (float) bossHealth / bossMaxHealth;
            if (lifePercentage <= 0.3f)
            {
                bossModel.GetComponent<SkinnedMeshRenderer>().material = hurtBossMaterial;
            }
        }
        else
        {
            BossDead();
        }
    }

    private void BossDead()
    {
        isDead = true;
        animator.SetTrigger("isDead");
        bossController.bossAwake = false;
        bossCollider.enabled = false;
        swordCollider.enabled = false;
        triggerCollider.enabled = false;
        audioManager.ChangeMusic(victoryMusic);

        StartCoroutine(playVideo());
    }

    private IEnumerator playVideo()
    {
        yield return new WaitForSeconds(5);
        videoPlayer.SetActive(true);
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(0);

    }
}
