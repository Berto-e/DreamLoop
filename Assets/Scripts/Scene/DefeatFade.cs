
using UnityEngine;

public class DefeatFade : MonoBehaviour
{

    private AudioSource audioSource;
    public bool hasRun = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (!hasRun)
            {
                audioSource.PlayOneShot(audioSource.clip);
                hasRun = true;
            }
        }
    }

}
