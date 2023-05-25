using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public float damageDeal = 10f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.CompareTag("Player"))
            {
                CharacterController player = other.GetComponent<CharacterController>();
                player.TakeDamage(damageDeal);

            }
        }

    }


   

    
}
