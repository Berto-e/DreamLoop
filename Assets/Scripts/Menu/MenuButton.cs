using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private SceneFade sceneFade;
    public void StartGame()
    {
        sceneFade.NextScene();
    }

    public void SalirButton()
    {
        Application.Quit();
    }

   
}
