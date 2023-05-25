
using UnityEngine;
using System.Collections.Generic;

public class openDoors : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> pressToInteract; // 0 PC 1 Gamepad
    [SerializeField]
    private GameObject doorOpenPrefab;
    private bool PlayerIsNear = false;
    public AudioClip doorOpenSound;
    public CheckDevices checkDevices;



    void Update()
    {
        if (PlayerIsNear)
        {
            if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Joystick1Button2))
            {
                
                pressToInteract[0].SetActive(false);
                pressToInteract[1].SetActive(false);
                Destroy(gameObject);
                GameObject doorCreated = Instantiate(doorOpenPrefab, transform.position, Quaternion.identity);
                doorCreated.GetComponent<AudioSource>().clip = doorOpenSound;
                if(!doorCreated.GetComponent<AudioSource>().isPlaying)
                {
                    doorCreated.GetComponent<AudioSource>().Play();
                }

            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(checkDevices.CheckDevice())
                pressToInteract[1].SetActive(true);
            else
                pressToInteract[0].SetActive(true);
                
            PlayerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(checkDevices.CheckDevice())
                pressToInteract[1].SetActive(false);
            else
                pressToInteract[0].SetActive(false);
            PlayerIsNear = false;
        }
    }

}
