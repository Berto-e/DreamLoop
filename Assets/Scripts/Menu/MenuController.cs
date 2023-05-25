using UnityEngine;
using UnityEngine.EventSystems;


public class MenuController : MonoBehaviour
{
    public GameObject leftImage;
    public GameObject rightImage;
    public AudioClip selectSound;
    private RectTransform leftRect;
    private RectTransform rightRect;
    private AudioSource audioSource;
    GameObject defaultSelection;
    string CurrentOption;

    [SerializeField] private SceneFade sceneFade;

    private void Start()
    {

        defaultSelection = GetComponent<EventSystem>().firstSelectedGameObject;
        leftRect = leftImage.GetComponent<RectTransform>();
        leftRect.localPosition = new Vector3(-242f, 3.6881f, 0.0f);
        rightRect = rightImage.GetComponent<RectTransform>();
        rightRect.localPosition = new Vector3(261.55f, 0f, 0.0f);
        audioSource = GetComponent<AudioSource>();
        CurrentOption = "Inicio";

    }

    private void Update()
    {
        if (GetComponent<EventSystem>().currentSelectedGameObject != null)
        {
            if (GetComponent<EventSystem>().currentSelectedGameObject.name == "Inicio")
            {
                leftRect.localPosition = new Vector3(-242f, 3.6881f, 0.0f);
                rightRect.localPosition = new Vector3(261.55f, 0f, 0.0f);

            }

            else if (GetComponent<EventSystem>().currentSelectedGameObject.name == "Opciones")
            {
                leftRect.localPosition = new Vector3(-260.8209f, -131.25f, 0.0f);
                rightRect.localPosition = new Vector3(399f, -131.25f, 0.0f);

            }

            else if (GetComponent<EventSystem>().currentSelectedGameObject.name == "Salir")
            {
                leftRect.localPosition = new Vector3(-234f, -288.06f, 0.0f);
                rightRect.localPosition = new Vector3(233f, -288.06f, 0.0f);

            }

            if (CurrentOption != GetComponent<EventSystem>().currentSelectedGameObject.name)
            {
                audioSource.PlayOneShot(selectSound);
                CurrentOption = GetComponent<EventSystem>().currentSelectedGameObject.name;
            }

        }

    }

    public void CatchMouseClicks(GameObject setSelection)
    {

        EventSystem.current.SetSelectedGameObject(setSelection);

    }


    public void StartGame()
    {
        sceneFade.NextScene();
    }

    public void SalirButton()
    {
        Application.Quit();
    }

}
