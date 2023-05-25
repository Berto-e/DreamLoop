
using UnityEngine;


public class NextLevel : MonoBehaviour
{

    public SceneFade sceneFade;
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            sceneFade.nextScene = true;
        }
    }
}
