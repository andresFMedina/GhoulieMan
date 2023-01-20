using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool bossAwake;
    private Animator animator;

    public bool isInBattle;
    public bool isAttacking;
    public float idleTimer = 0f;
    public float idleWaitTime = 10f;

    private BossHealth bossHealth;
    public float attackTimer = 0f;
    public float attackWaitTime = 2.0f;

    private BoxCollider swordTrigger;
    public GameObject bossHealthBar;

    private SmoothFollow smoothFollow;
    private GameObject player;
    private PlayerHealth playerHealth;

    private BoxCollider bossCheckPoint;
    private ParticleSystem particleSystem;

    void Start()
    {
        animator = GetComponent<Animator>();
        bossHealth = GetComponent<BossHealth>();
        swordTrigger = GameObject.Find("Boss").GetComponentInChildren<BoxCollider>();
        bossHealthBar.SetActive(false);
        smoothFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>();
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        bossCheckPoint = GameObject.Find("BossCheckPoint").GetComponent<BoxCollider>();
        particleSystem = GameObject.Find("RockPS").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossAwake)
        {
            print("BossAwake");
            animator.SetBool("bossAwake", true);
            bossHealthBar.SetActive(true);

            if (isInBattle)
            {
                if (!isAttacking)
                {
                    idleTimer += Time.deltaTime;
                }
                else
                {
                    idleTimer = 0f;
                    attackTimer += Time.deltaTime;
                    if(attackTimer >= attackWaitTime)
                    {
                        BossAttack attack = (BossAttack)Random.Range(1, 4);
                        Attack(attack);
                    }
                }
                if(idleTimer > idleWaitTime)
                {
                    print("Attack!");
                    isAttacking = true;
                    idleTimer = 0f;
                    idleWaitTime = Random.Range(3, 5);
                }
            }
            else
            {
                idleTimer = 0;
            }
        }
        BossReset();
    }

    private void BossReset()
    {
        if(playerHealth.CurrentHealth == 0)
        {
            bossAwake = false;
            smoothFollow.bossCameraActive = false;
            bossCheckPoint.isTrigger = true;
            animator.Play("Idle");
            animator.SetBool("bossAwake", false);
        }
    }

    private void Attack(BossAttack bossAttack)
    {
        isAttacking = false;
        animator.SetTrigger($"bossAttack{(int)bossAttack}");
        attackTimer = 0f;
        print("Boss Smash");
        attackWaitTime = Random.Range(3, 5);
        switch (bossAttack)
        {
            case BossAttack.Attack1:
            case BossAttack.Attack2:
                swordTrigger.enabled = true;
                break;
            case BossAttack.Attack3:
                StartCoroutine(fallingRocks());
                break;
        }
    }

    private IEnumerator fallingRocks()
    {
        yield return new WaitForSeconds(2);
        var emission = particleSystem.emission;
        emission.enabled = true;
        particleSystem.Play();
        yield return new WaitForSeconds(3);
        emission.enabled = false;
    }

    private enum BossAttack
    {
        Attack1 = 1,
        Attack2,
        Attack3,
    }
}
