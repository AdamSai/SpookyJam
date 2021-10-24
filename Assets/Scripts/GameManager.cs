using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;
    public int score = 0;

    public InputController inputController { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogWarning("One too many GameManagers in the scene");
        }
    }

    private void Start()
    {
        inputController = GetComponent<InputController>();
    }

    public void PlayGame()
    {
        //TODO: Link all variables needed for game or make sure to tick a bool so those scripts can be used
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
