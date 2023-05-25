using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class openChests : MonoBehaviour
{
    [SerializeField]
    private GameObject UIeventPC;
    [SerializeField]
    private GameObject UIeventGamepad;
    [SerializeField]
    private GameObject chestOpenPrefab;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject potionPrefab;
    [SerializeField]
    public AudioClip chestOpenSound;
    private Vector2 lootPos;
    private bool PlayerIsNear = false;
    private AudioSource chestAudio;
    public CheckDevices checkDevices;


    private void Start()
    {
        lootPos = new Vector2(transform.position.x + 1f, transform.position.y + 0.25f);
    }

    void Update()
    {
        if (PlayerIsNear)
        {
            if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Joystick1Button2))
            {
                UIeventGamepad.SetActive(false);
                UIeventPC.SetActive(false);
                Destroy(gameObject);
                //newChestPrefab
                GameObject chestCreated = Instantiate(chestOpenPrefab, transform.position, Quaternion.identity);
                chestCreated.GetComponent<AudioSource>().clip = chestOpenSound;
                if (!chestCreated.GetComponent<AudioSource>().isPlaying)
                {
                    chestCreated.GetComponent<AudioSource>().Play();
                }
                //RandomDrop
                float drop = LootDrop();
                if (drop == 0) //return 0 is equal to coinPrefab
                {

                    Instantiate(coinPrefab, lootPos, Quaternion.identity);
                }
                else if (drop == 1) // return 1 is equal to potionPrefab
                {
                    Instantiate(potionPrefab, lootPos, Quaternion.identity);

                }

            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!checkDevices.CheckDevice())
                UIeventPC.SetActive(true);
            else
                UIeventGamepad.SetActive(true);
            PlayerIsNear = true;


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!checkDevices.CheckDevice())
                UIeventPC.SetActive(false);
            else
                UIeventGamepad.SetActive(false);
            PlayerIsNear = false;
        }
    }

    private float LootDrop()
    {
        float randomNumber = Random.Range(0, 2); // 0 is Coin 1 is Potion
        return randomNumber;
    }

}
