using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTriggerZone : MonoBehaviour
{
    [Header("Projectile to be spawned")]
    public GameObject Projectile;
    [Header("Random sprites to choose from")]
    public Sprite[] sprites;
    [Space]
    public int damage;
    [Space]
    public int numberOfProjectiles;
    [Space]
    [Tooltip("Set to negative to spawn the opposite direction")]
    public float offset;
    [Space]
    public float verticalOffset;
    [Space]
    public float spawnDelay;
    [Space]
    public bool randomTorque;
    [Space]
    public bool randomForce;
    private float timesSpawned = 0;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            InvokeRepeating("SpawnProjectile", 1, spawnDelay);
        }
    }

    void SpawnProjectile()
    {
        GameObject instantiatedProjectile = Instantiate(Projectile, new Vector3(transform.position.x + (timesSpawned * offset), transform.position.y + verticalOffset, transform.position.z), Quaternion.identity);
        int randomSprite = Random.Range(0, sprites.Length);
        instantiatedProjectile.GetComponent<SpriteRenderer>().sprite = sprites[randomSprite];
        instantiatedProjectile.GetComponent<stalactite>().damage = damage;
        if (randomTorque)
        {
            instantiatedProjectile.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            instantiatedProjectile.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100f, 100f), ForceMode2D.Impulse);
        }
        if (randomForce)
        {
            instantiatedProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), 0), ForceMode2D.Impulse);
        }
        timesSpawned += 1;

        if (timesSpawned >= numberOfProjectiles)
        {
            CancelInvoke();
        }
    }
}
