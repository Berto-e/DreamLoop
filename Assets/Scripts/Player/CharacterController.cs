using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using TMPro;



public class CharacterController : MonoBehaviour
{

    //Public vars
    public float speed;
    public float health;
    public float power;

    public bool isDeath = false;

    public TMP_Text coinsText;
    [Header("AudioClips")]
    public AudioClip swordSound;
    public AudioClip swordFireSound;
    public AudioClip coinCollect;
    public AudioClip potionCollect;
    public AudioClip hitSound;
    public GameObject blood;
    public int attackMode = 1; //1 normal mode 2 fire mode
    public int coins = 0;
    public GameObject powerMessage;
    public int playerLives;
    public GameObject defeatFade;

    public GameObject FireUI;
    //Private vars
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isAttacking = false;
    private bool isAttackingFire = false;

    private AudioSource audioSource;

   

    private Color currentSpriteColor;
    private Color hitColor;
    private SpriteRenderer sr;
    private Image srAvatar;
    bool hit = false;

    private float maxHealth = 100f;
    private float maxPower = 100f;


    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        hitColor = new Color32(152, 85, 85, 255);
        sr = GetComponent<SpriteRenderer>();
        currentSpriteColor = sr.color;
        srAvatar = GameObject.Find("PlayerImage").GetComponent<Image>();
        health = PlayerStatic.healthStatic;
        power = PlayerStatic.powerStatic;
        playerLives = PlayerStatic.playerLivesStatic;

    }


    private void Update()
    {
        if (!isDeath)
        {

            //Animations
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("IsMoving", movement.magnitude > 0);
            animator.SetBool("isAttacking", isAttacking);
            animator.SetBool("isAttackingFire", isAttackingFire);

            //hit Animation
            if (hit)
            {
                hit = false;
                StartCoroutine(hitColorRoutine());
            }

            //Death Animation
            if (health <= 0)
            {
                Defeat();
            }

            //HealthBarUpdate
            HealthBarUpdate();
            //PowerBarUpdate
            PowerBarUpdate();
            //CoinsUpdate
            CoinsUpdate();
            //AttackModeUpdate
            AttackModeUpdate();
            //PlayerLivesSprites
            PlayerLivesSprites();

            if (attackMode == 2)
            {
                FireUI.SetActive(true);

            }
            else if (attackMode == 1)
            {
                FireUI.SetActive(false);
            }

            //Health&PowerUpdate
            PlayerStatic.healthStatic = health;
            PlayerStatic.powerStatic = power;
            PlayerStatic.playerLivesStatic = playerLives;

        }


    }
    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

    }

    private void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<Vector2>();
    }

    public void OnFire()
    {
        if (!isDeath)
        {
            if (attackMode == 1)
            {
                if (!isAttacking)
                {
                    audioSource.PlayOneShot(swordSound);
                    isAttacking = true;

                }
            }
            else if (attackMode == 2)
            {
                if (!isAttackingFire && power > 0)
                {
                    audioSource.PlayOneShot(swordFireSound);
                    isAttackingFire = true;

                }
            }
        }
    }

    private void LockMovement()
    {
        speed = 0.0f;
    }

    private void UnlockMovement()
    {
        speed = 5f;
    }


    private void canAttack()
    {
        if (attackMode == 1)
            isAttacking = true;
        else if (attackMode == 2)
            isAttackingFire = true;
    }
    private void stopAttack()
    {
        if (attackMode == 1)
            isAttacking = false;
        else if (attackMode == 2)
            isAttackingFire = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.CompareTag("Coin"))
            {
                coins += 5;
                audioSource.PlayOneShot(coinCollect);
                coinsText.text = "" + coins.ToString();
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Potion"))
            {
                audioSource.PlayOneShot(potionCollect);
                PowerUpdate();
                Destroy(other.gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {

        health -= damage;
        hit = true;

    }

    IEnumerator hitColorRoutine()
    {
        audioSource.volume = 0.4f;
        sr.color = hitColor;
        srAvatar.color = hitColor;
        blood.SetActive(true);
        audioSource.PlayOneShot(hitSound);
        yield return new WaitForSeconds(0.3f);
        blood.SetActive(false);
        sr.color = currentSpriteColor;
        srAvatar.color = currentSpriteColor;
        audioSource.volume = 0.8f;

    }

    private void CoinsUpdate()
    {
        if (coins >= 10 && health < maxHealth)
        {
            float extraHealth = 25;
            if (health + extraHealth > maxHealth)
            {
                extraHealth = (health + extraHealth) - maxHealth;
                extraHealth = 25 - extraHealth;
            }
            health += extraHealth;

            //reset Coins
            coins = 0;
            coinsText.text = "" + coins.ToString();
        }

    }

    private void PowerUpdate()
    {
        if (power < 100)
        {
            float extraPower = 15;
            if (power + extraPower > 100)
            {
                extraPower = (power + extraPower) - 100;
                extraPower = 15 - extraPower;
            }
            power += extraPower;
        }

    }

    private void HealthBarUpdate()
    {
        Image healthBar = GameObject.Find("health").GetComponent<Image>();
        healthBar.fillAmount = 1.0f - (1.0f - health / maxHealth);
    }

    private void AttackModeUpdate()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (attackMode == 1 && !isAttacking)
            {
                if (power > 0)
                    attackMode = 2;
                else
                    StartCoroutine(PowerMessage());
            }
            else if (attackMode == 2 && !isAttackingFire)
            {
                attackMode = 1;
            }
        }


        if (Input.GetKeyUp(KeyCode.Joystick1Button3))
        {
            if (attackMode == 1 && !isAttacking)
            {
                if (power > 0)
                    attackMode = 2;
                else
                    StartCoroutine(PowerMessage());
            }
            else if (attackMode == 2 && !isAttackingFire)
            {
                attackMode = 1;
            }
        }

        if (attackMode == 2 && power <= 0)
        {
            attackMode = 1;
        }


    }

    private void Defeat()
    {
        isDeath = true;
        animator.SetTrigger("Death");
        playerLives--;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.GetComponent<AudioSource>().volume = 0f;
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().volume = 0f;
        defeatFade.SetActive(true);


    }

    IEnumerator PowerMessage()
    {
        powerMessage.SetActive(true);
        yield return new WaitForSeconds(2f);
        powerMessage.SetActive(false);
    }

    private void PowerBarUpdate()
    {
        Image powerBar = GameObject.Find("power").GetComponent<Image>();
        powerBar.fillAmount = 1.0f - (1.0f - power / maxPower);
    }

    private void PowerVar()
    {
        if (power - 15 > 0)
            power -= 15;
        else if (power - 15 <= 0)
            power = 0;
    }

    void PlayerLivesSprites()
    {
        if (playerLives == 2)
        {
            GameObject heart = GameObject.Find("heart1");
            heart.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }
        else if (playerLives == 1)
        {
            GameObject heart = GameObject.Find("heart1");
            heart.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
            heart = GameObject.Find("heart2");
            heart.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }
        else if (playerLives == 0)
        {
            GameObject heart = GameObject.Find("heart1");
            heart.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
            heart = GameObject.Find("heart2");
            heart.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
            heart = GameObject.Find("heart3");
            heart.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }

    }

}
