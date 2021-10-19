using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuEventController : MonoBehaviour
{
    public GameObject loadingScreen;
    private void Start()
    {
        loadingScreen.SetActive(false);
    }

    public void LoadGame()
    {
        Debug.Log("Loading game");
        StartCoroutine(StartGameRoutine());
    }

    IEnumerator StartGameRoutine()
    {
        loadingScreen.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Particle_Test");

        while (!asyncLoad.isDone)
        {
            // still loading
            yield return null;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
