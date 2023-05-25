using UnityEngine;

public class DetectionZone : MonoBehaviour
{

    public Collider2D targetObj;
   
 private void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Player"))
    {
        targetObj = other;
    }
 }

  void OnTriggerExit2D(Collider2D other) {
    if(other.CompareTag("Player"))
    {
        targetObj = null;
    }
 }
}
