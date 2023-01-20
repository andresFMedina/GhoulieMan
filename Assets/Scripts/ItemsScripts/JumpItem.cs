using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : MonoBehaviour
{
    private GameObject player;
    private CharacterMovement characterMovement;

    private SphereCollider sphereCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float jumpForceAdded = 200f;
    [SerializeField] private int duration = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        characterMovement = player.GetComponent<CharacterMovement>();

        sphereCollider = GetComponent<SphereCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            StartCoroutine(pickupItem());
        }
    }

    public IEnumerator pickupItem()
    {
        characterMovement.jumpSpeed += jumpForceAdded;
        spriteRenderer.enabled = false;
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(duration);
        characterMovement.jumpSpeed -= jumpForceAdded;
        Destroy(gameObject);
        yield return null;
    }


}
