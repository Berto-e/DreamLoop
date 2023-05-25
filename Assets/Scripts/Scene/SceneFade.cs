using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private float transitionTime = 1.5f;
    private Animator anim;
    public bool nextScene = false;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (nextScene == true)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(SceneLoad(nextSceneIndex));
    }

    public IEnumerator SceneLoad(int sceneIndex)
    {
        anim.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }

    public void NextScene()
    {
        nextScene = true;
    }
}
