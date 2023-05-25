using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Interactable : MonoBehaviour
{

    [SerializeField]
    private GameObject uiMessageIsNearPC; // Press key to continue
    [SerializeField]
    private GameObject uiMessageIsNearGamepad; //Pres key to continue
    [SerializeField]
    private List<GameObject> gameAdvices; // 0 Interactuar PC 1 Attack PC 2 Interactuar Gamepad 3 Attack Gamepad
    private bool isPlayerInRange = false;
    bool messageShown = false;
    private AudioSource messageNotification;
    public CheckDevices checkDevices;


    void Start()
    {
        messageNotification = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlayerInRange && (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Joystick1Button2)) && messageShown == false)
        {
            messageNotification.Play();
            messageShown = true;
            gameObject.transform.Find("Exclamation").gameObject.SetActive(false);
            uiMessageIsNearGamepad.SetActive(false);
            uiMessageIsNearPC.SetActive(false);

            StartCoroutine(WaitMessages());
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isPlayerInRange = true;
        if (checkDevices.CheckDevice())
        {
            if (other.gameObject.CompareTag("Player") && messageShown == false)
            {

                uiMessageIsNearGamepad.SetActive(true);
            }
        }

        if (!checkDevices.CheckDevice())
        {
            if (other.gameObject.CompareTag("Player") && messageShown == false)
            {

                uiMessageIsNearPC.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPlayerInRange = false;
        if (checkDevices.CheckDevice())
        {
            if (other.gameObject.CompareTag("Player") && messageShown == false)
            {

                uiMessageIsNearGamepad.SetActive(false);
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Player") && messageShown == false)
            {

                uiMessageIsNearPC.SetActive(false);
            }
        }
    }

    IEnumerator WaitMessages()
    {
        if (!checkDevices.CheckDevice())
        {
            if (gameObject.name == "PF Props Rune Pillar X3")
            {
                gameAdvices[0].SetActive(true);
                yield return new WaitForSeconds(3);
                gameAdvices[0].SetActive(false);

            }

            else if (gameObject.name == "PF Props Rune Pillar X2")
            {
                gameAdvices[1].SetActive(true);
                yield return new WaitForSeconds(3);
                gameAdvices[1].SetActive(false);

            }

            else if (gameObject.name == "PF Props Rune Pillar CheckPoint")
            {
                gameAdvices[4].SetActive(true);
                yield return new WaitForSeconds(4);
                gameAdvices[4].SetActive(false);

            }
        }
        else
        {
            if (gameObject.name == "PF Props Rune Pillar X3")
            {
                gameAdvices[2].SetActive(true);
                yield return new WaitForSeconds(3);
                gameAdvices[2].SetActive(false);

            }

            else if (gameObject.name == "PF Props Rune Pillar X2")
            {
                gameAdvices[3].SetActive(true);
                yield return new WaitForSeconds(3);
                gameAdvices[3].SetActive(false);

            }
        }

    }


}
