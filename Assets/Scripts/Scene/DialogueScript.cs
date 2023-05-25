using System.Collections;

using UnityEngine;
using TMPro;


public class DialogueScript : MonoBehaviour
{
    [SerializeField, TextArea(6, 4)] private string[] dialogueLines;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] GameObject backgroundImage1;
    [SerializeField] GameObject backgroundImage2;
    [SerializeField] private SceneFade sceneFade;
    [SerializeField] GameObject backgroundMusic;
    [SerializeField] GameObject characterController;
    private bool didDialogueStart = false;
    private int lineIndex;
    private bool dialogueEnd = false;
    private AudioSource typingAudio;
    public bool finalBossDialogue = false;





    private void Start()
    {
        typingAudio = GetComponent<AudioSource>();
        if (finalBossDialogue)
        {
            backgroundMusic.GetComponent<AudioSource>().Stop();
            characterController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }


    }
    void Update()
    {
        if (!didDialogueStart)
        {
            StartDialogue();
        }
        else if (dialogueEnd == false && dialogueText.text == dialogueLines[lineIndex])
        {
            NextDialogueLine();
        }
        if (dialogueEnd == true)
        {
            if (sceneFade != null)
                sceneFade.NextScene();
            if (finalBossDialogue)
            {
                characterController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                characterController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                backgroundMusic.GetComponent<AudioSource>().Play();
                Destroy(gameObject);
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            dialogueEnd = true;
        }
    }

    private IEnumerator ShowLine()
    {
        yield return new WaitForSeconds(1.5f);
        if (lineIndex == 2)
        {
            if (backgroundImage1 != null && backgroundImage2 != null)
            {
                backgroundImage1.SetActive(false);
                backgroundImage2.SetActive(true);
            }
        }

        dialogueText.text = string.Empty;
        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            if (!typingAudio.isPlaying)
            {
                typingAudio.Play();
            }
            yield return new WaitForSeconds(0.05f); //typing time
        }
    }




}
