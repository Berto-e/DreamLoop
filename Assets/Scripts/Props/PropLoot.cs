using System.Collections;
using UnityEngine;

public class PropLoot : MonoBehaviour
{
    [SerializeField] private float health = 50f;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject potionPrefab;
    private bool hit = false;
    private SpriteRenderer sp;
    private AudioSource audioSource;


    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            hit = false;
            StartCoroutine(HitColorRoutine());
        }

        if (health <= 0)
        {
            LootDrop();
            Destroy(gameObject);
        }

    }
    public void TakeDamage(float damage)
    {

        health -= damage;
        hit = true;
    }

    private void LootDrop()
    {
        float randomNumber = Random.Range(0, 2); // 0 is Coin 1 no drop
        Vector2 lootPos = (Vector2)transform.position;
        if (randomNumber == 0) //return 0 is equal to coinPrefab
        {

            Instantiate(coinPrefab, lootPos, Quaternion.identity);
        }
        else if (randomNumber == 1)
        {
            Instantiate(potionPrefab, lootPos, Quaternion.identity);
        }
    }

    IEnumerator HitColorRoutine()
    {
        sp.color = new Color32(152, 11, 11, 255);
        audioSource.Play();
        yield return new WaitForSeconds(0.3f);
        sp.color = new Color32(255, 255, 255, 255);
    }
}
