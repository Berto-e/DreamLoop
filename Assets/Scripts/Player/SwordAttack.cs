
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    public float damage = 10f;
    public float fireDamage = 20f;
    public float knockbackForce = 500f;
    public CharacterController characterController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.CompareTag("Enemy"))
            {
                //Calculate Direction between character and slime
                Vector3 parentPosition = GameObject.Find("Protagonist").GetComponent<Transform>().position;
                Vector2 direction = (Vector2)(other.gameObject.transform.position - parentPosition).normalized;
                Vector2 knockback = direction * knockbackForce;

                Enemy enemy = other.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                enemy.TakeKnockback(knockback);
                if (characterController.attackMode == 2)
                    enemy.TakeFireDamage(fireDamage);


            }
            else if (other.CompareTag("PropLoot"))
            {
                PropLoot propLoot = other.GetComponent<PropLoot>();
                propLoot.TakeDamage(damage);
            }
        }

    }

    private void Update()
    {
        if (characterController.attackMode == 1)
        {
            damage = 10f;
            knockbackForce = 500f;
            fireDamage = 0f;
        }
        else if (characterController.attackMode == 2)
        {
            damage = 30f;
            knockbackForce = 800f;
            fireDamage = 20f;
        }
    }
}



