using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{


    public float health = 50;
    public GameObject healthText;

    public DetectionZone detectionZone;
    public float moveSpeed = 500f;
    public AudioClip slimeJump;
    public AudioClip slimeHit;
    public GameObject coinPrefab;
    public GameObject Fire;
    SpriteRenderer spriteRenderer;
    bool fireisUp = false;
    private Color32 defaultSpriteColor;
    private Animator animator;
    private float maxHealth;
    private Rigidbody2D rb;

    private AudioSource slimeAudio;
    [SerializeField] private SceneFade SceneFade;




    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", true);
        rb = GetComponent<Rigidbody2D>();
        slimeAudio = GetComponent<AudioSource>();
        defaultSpriteColor = spriteRenderer.color;
        maxHealth = health;
    }

    private void FixedUpdate()
    {

        detectedZone();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Defeated();
        }



    }

    public void TakeDamage(float damage)
    {
        FloatingText(damage);
        animator.SetTrigger("hit");
        health -= damage;
        slimeAudio.PlayOneShot(slimeHit);



    }

    public void TakeFireDamage(float fireDamage)
    {
        StartCoroutine(FireDamageRoutine(fireDamage));

    }

    public void Defeated()
    {

        animator.SetBool("isAlive", false);

    }

    public void RemoveEnemy()
    {
        if (gameObject.name == "FinalBoss")
        {
            FinalBattle();
        }
        else
        {
            AttackHitbox attackHitbox = GetComponentInChildren<AttackHitbox>();
            attackHitbox.damageDeal = 0;
            LootDrop();
            Destroy(gameObject);
        }


    }

    private void FloatingText(float damage)
    {
        healthText.GetComponent<TMP_Text>().text = "" + damage.ToString();
        RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
        Vector3 positionOffset = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        positionOffset = new Vector3(positionOffset.x + 10f, positionOffset.y + 30f, positionOffset.z);
        textTransform.transform.position = positionOffset;


        Canvas canvas = GameObject.FindAnyObjectByType<Canvas>();
        textTransform.SetParent(canvas.transform);

    }

    private void detectedZone()
    {
        if (detectionZone.targetObj != null)
        {
            animator.SetBool("isMoving", true);
            Vector2 direction = (detectionZone.targetObj.transform.position - transform.position).normalized;
            //Move towards detected object
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
            if (!slimeAudio.isPlaying)
            {
                slimeAudio.clip = slimeJump;
                slimeAudio.PlayDelayed(0.3f);

            }
        }
        else
            animator.SetBool("isMoving", false);

    }

    public void TakeKnockback(Vector2 knockback)
    {
        rb.AddForce(knockback);
    }



    private void LootDrop()
    {
        float randomNumber = Random.Range(0, 2); // 0 is Coin 1 no drop
        Vector2 lootPos = (Vector2)transform.position;
        if (randomNumber == 0) //return 0 is equal to coinPrefab
        {

            Instantiate(coinPrefab, lootPos, Quaternion.identity);
        }
    }

    IEnumerator FireDamageRoutine(float fireDamage)
    {

        Fire.SetActive(true);
        fireisUp = true;
        spriteRenderer.color = new Color32(255, 33, 0, 255);
        for (float i = 0; i < fireDamage; i++)
        {

            TakeDamage(1);
            yield return new WaitForSeconds(1.5f);
        }
        spriteRenderer.color = defaultSpriteColor;
        fireisUp = false;
        Fire.SetActive(false);
    }

    public void hitColorOn()
    {
        if (!fireisUp)
            spriteRenderer.color = new Color32(255, 33, 0, 255);
    }

    public void hitColorOff()
    {
        if (!fireisUp)
            spriteRenderer.color = defaultSpriteColor;
    }

    public void FinalBattle()
    {
        AttackHitbox attackHitbox = GetComponentInChildren<AttackHitbox>();
        attackHitbox.damageDeal = 0;
        Destroy(gameObject);
        SceneFade.NextScene();
    }








}
