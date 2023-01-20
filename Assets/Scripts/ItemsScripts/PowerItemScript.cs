using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemScript : MonoBehaviour
{
    private GameObject player;
    private AudioSource audioSource;
    private ParticleSystem particleSystem;
    private PlayerHealth playerHealth;

    private MeshRenderer meshRenderer;
    private ParticleSystem brainParticleSystem;
    public GameObject pickupEffect;

    private ItemExplode itemExplode;
    private SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.enabled = true;

        particleSystem = player.GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        brainParticleSystem = GetComponentInChildren<ParticleSystem>();

        itemExplode = GetComponent<ItemExplode>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            StartCoroutine(InvincibleRoutine());
            meshRenderer.enabled = false;
        }
    }

    public IEnumerator InvincibleRoutine()
    {
        itemExplode.Pickup();
        particleSystem.enableEmission = true;
        playerHealth.enabled = false;
        brainParticleSystem.enableEmission = false;
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(10f);
        particleSystem.enableEmission = false;
        playerHealth.enabled = true;
        Destroy(gameObject);
    }

    //void Pickup()
    //{
    //    Instantiate(pickupEffect, transform.position, transform.rotation);
    //}
}
