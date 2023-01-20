using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float range = 15f;
    [SerializeField] private float timeBetween = 1f;

    private GameObject player;
    private bool playerInRange;

    public Rigidbody enemyPrefab;

    private Rigidbody clone;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;

        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < range)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
        print("Player in spawner range " + playerInRange);
    }

    public IEnumerator SpawnEnemies()
    {
        if(playerInRange && !GameManager.instance.GameOver)
        {
            clone = Instantiate(enemyPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(timeBetween);
        }
        yield return null;
        StartCoroutine(SpawnEnemies());
    }
}
