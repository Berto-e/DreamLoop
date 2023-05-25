
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private GameObject uiPauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject controlesMenu;
    [SerializeField] private GameObject titleMenu;
    [SerializeField] private GameObject volumenMenu;
    [SerializeField] private GameObject controlesMenuPc;
    [SerializeField] private GameObject controlesMenuGamepad;
    [SerializeField] private CheckDevices checkDevices;
    private GameObject lastSelectedOption;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject defeatFade;
    [SerializeField] private GameObject leftImage;
    [SerializeField] private GameObject rightImage;
    private GameObject uiPauseMenuCopy;
    private bool hasRun = false;
    private string CurrentOption = "StartButton";
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }




    void Update()
    {
        if (uiPauseMenu != null && !optionsMenu.activeSelf)
            PlayerMenuUpdate();

        if (checkDevices.CheckDevice())
        {
            BackGamepad();
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedOption);
        }
        if (defeatFade != null)
        {
            if (defeatFade.activeSelf)
            {
                if (!hasRun)
                {
                    CurrentOption = "VolverIntentarlo";
                    DefeatFadeUpdate();
                }
            }
            else
            {
                hasRun = false;
                defeatFade.GetComponent<DefeatFade>().hasRun = false;
            }
        }

        DefeatFadeMenu();
        if (audioSource != null)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                if (CurrentOption != EventSystem.current.currentSelectedGameObject.name)
                {
                    audioSource.PlayOneShot(audioSource.clip);
                    CurrentOption = EventSystem.current.currentSelectedGameObject.name;
                }
            }
        }



    }
    void DefeatFadeMenu()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {


            if (EventSystem.current.currentSelectedGameObject.name == "VolverIntentarlo")
            {
                leftImage.transform.localPosition = new Vector3(-621f, -251.42f, 0f);
                rightImage.transform.localPosition = new Vector3(0f, -251.42f, 0f);
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "SalirDefeat")
            {
                leftImage.transform.localPosition = new Vector3(175f, -244.54f, 0f);
                rightImage.transform.localPosition = new Vector3(658f, -244.54f, 0f);
            }
        }


    }

    void DefeatFadeUpdate()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(GameObject.Find("VolverIntentarlo"));
        lastSelectedOption = EventSystem.current.currentSelectedGameObject;
        uiPauseMenuCopy = uiPauseMenu;
        uiPauseMenu = null;
        hasRun = true;

    }

    void PlayerMenuUpdate()
    {

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Joystick1Button7))
        {

            uiPauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("StartButton"));
            lastSelectedOption = EventSystem.current.currentSelectedGameObject;
            Time.timeScale = 0;
            GameObject.Find("Protagonist").GetComponent<AudioSource>().volume = 0f;

        }
    }

    public void ContinueGameButton()
    {
        uiPauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameObject.Find("Protagonist").GetComponent<AudioSource>().volume = 0.8f;

    }

    public void OptionsGameButton()
    {
        if (checkDevices.CheckDevice())
        {
            optionsMenu.SetActive(true);
            if (titleMenu != null)
                titleMenu.SetActive(false);
            if (uiPauseMenu != null)
                uiPauseMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Controles"));
            lastSelectedOption = EventSystem.current.currentSelectedGameObject;
        }
        else
        {
            if (titleMenu != null)
                titleMenu.SetActive(false);
            optionsMenu.SetActive(true);
            if (uiPauseMenu != null)
                uiPauseMenu.SetActive(false);
        }




    }

    public void ExitGameButton()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void ControlesButton()
    {
        if (checkDevices.CheckDevice())
        {
            optionsMenu.SetActive(false);
            controlesMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("PC"));
            lastSelectedOption = EventSystem.current.currentSelectedGameObject;
        }
        else
        {
            optionsMenu.SetActive(false);
            controlesMenu.SetActive(true);
        }
    }

    public void VolverIntentarloButton()
    {
        if (characterController.playerLives > 0)
        {
            Animator animator = characterController.GetComponent<Animator>();
            Rigidbody2D rb = characterController.GetComponent<Rigidbody2D>();
            characterController.isDeath = false;
            characterController.health = 100f;
            animator.SetTrigger("Alive");
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            characterController.GetComponent<AudioSource>().volume = 0.8f;
            GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().volume = 0.5f;
            if (CheckPointsStatic.playerCurrentCheckpoint != null){
                if(CheckPointsStatic.checkPointScene != SceneManager.GetActiveScene().buildIndex)
                    characterController.transform.position = new Vector3(0f, -8f, 0f);
                else
                    characterController.transform.position = CheckPointsStatic.playerCurrentCheckpoint;
            }
            else
                characterController.transform.position = new Vector3(0f, -8f, 0f);
            GameObject.Find("DefeatFade").SetActive(false);
            uiPauseMenu = uiPauseMenuCopy;
        }
        else
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

    }

    public void VolumenButton()
    {
        if (checkDevices.CheckDevice())
        {
            optionsMenu.SetActive(false);
            volumenMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("BackVolumen"));
            lastSelectedOption = EventSystem.current.currentSelectedGameObject;
        }
        else
        {
            optionsMenu.SetActive(false);
            volumenMenu.SetActive(true);
        }
    }

    public void PCButton()
    {
        if (checkDevices.CheckDevice())
        {
            controlesMenu.SetActive(false);
            controlesMenuPc.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("BackPC"));
            lastSelectedOption = EventSystem.current.currentSelectedGameObject;
        }
        else
        {
            controlesMenu.SetActive(false);
            controlesMenuPc.SetActive(true);
        }
    }

    public void GamepadButton()
    {
        if (checkDevices.CheckDevice())
        {
            controlesMenu.SetActive(false);
            controlesMenuGamepad.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("BackGamepad"));
            lastSelectedOption = EventSystem.current.currentSelectedGameObject;
        }
        else
        {
            controlesMenu.SetActive(false);
            controlesMenuGamepad.SetActive(true);
        }
    }

    public void VolverButton()
    {
        if (checkDevices.CheckDevice())
        {
            if (controlesMenuPc.activeSelf)
            {
                controlesMenuPc.SetActive(false);
                controlesMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GameObject.Find("PC"));
                lastSelectedOption = EventSystem.current.currentSelectedGameObject;


            }


            else if (controlesMenuGamepad.activeSelf)
            {
                controlesMenuGamepad.SetActive(false);
                controlesMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GameObject.Find("PC"));
                lastSelectedOption = EventSystem.current.currentSelectedGameObject;

            }

            else if (volumenMenu.activeSelf)
            {
                volumenMenu.SetActive(false);
                optionsMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Controles"));
                lastSelectedOption = EventSystem.current.currentSelectedGameObject;
            }

            else if (controlesMenu.activeSelf)
            {
                controlesMenu.SetActive(false);
                optionsMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Controles"));
                lastSelectedOption = EventSystem.current.currentSelectedGameObject;
            }

            else if (optionsMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                if (uiPauseMenu != null)
                {
                    uiPauseMenu.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("StartButton"));
                    lastSelectedOption = EventSystem.current.currentSelectedGameObject;
                }
                else //MenuInicio
                {
                    if (titleMenu != null)
                        titleMenu.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Inicio"));
                    lastSelectedOption = EventSystem.current.currentSelectedGameObject;
                }


            }
        }
        else
        {
            if (controlesMenuPc.activeSelf)
            {
                controlesMenuPc.SetActive(false);
                controlesMenu.SetActive(true);



            }

            else if (controlesMenuGamepad.activeSelf)
            {
                controlesMenuGamepad.SetActive(false);
                controlesMenu.SetActive(true);


            }

            else if (volumenMenu.activeSelf)
            {
                volumenMenu.SetActive(false);
                optionsMenu.SetActive(true);

            }

            else if (controlesMenu.activeSelf)
            {
                controlesMenu.SetActive(false);
                optionsMenu.SetActive(true);

            }

            else if (optionsMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                if (titleMenu != null)
                    titleMenu.SetActive(true);
                if (uiPauseMenu != null)
                {
                    uiPauseMenu.SetActive(true);
                }

            }
        }
    }

    public void BackGamepad()
    {
        if (Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            if (controlesMenuPc.activeSelf)
            {
                controlesMenuPc.SetActive(false);
                controlesMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GameObject.Find("PC"));
                lastSelectedOption = EventSystem.current.currentSelectedGameObject;


            }

            else if (controlesMenuGamepad.activeSelf)
            {
                controlesMenuGamepad.SetActive(false);
                controlesMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GameObject.Find("PC"));
                lastSelectedOption = EventSystem.current.currentSelectedGameObject;

            }

            else if (volumenMenu.activeSelf)
            {
                volumenMenu.SetActive(false);
                optionsMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Controles"));
                lastSelectedOption = EventSystem.current.currentSelectedGameObject;
            }

            else if (controlesMenu.activeSelf)
            {
                controlesMenu.SetActive(false);
                optionsMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Controles"));
                lastSelectedOption = EventSystem.current.currentSelectedGameObject;
            }

            else if (optionsMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                if (titleMenu != null)
                    titleMenu.SetActive(true);
                if (uiPauseMenu != null)
                {
                    uiPauseMenu.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("StartButton"));
                    lastSelectedOption = EventSystem.current.currentSelectedGameObject;
                }

                else
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Inicio"));
                    lastSelectedOption = EventSystem.current.currentSelectedGameObject;
                }

            }

        }
    }

    public void SetLevel(float sliderValue)
    {
        AudioListener.volume = sliderValue;
    }
}
