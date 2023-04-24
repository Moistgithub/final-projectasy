using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    public scoremanager scoremanager;

    private void Start()
    {

        scoremanager = FindObjectOfType<scoremanager>();
        LoadGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
    public void SaveGame()
    {
        if(scoremanager != null)
            PlayerPrefs.SetInt("SavedCoinsCollected", scoremanager.currentscore);
    }
    public void LoadGame()
    {
        if (scoremanager == null)
            return;

        Debug.Log(scoremanager.currentscore);
        scoremanager.currentscore = PlayerPrefs.GetInt("SavedCoinsCollected");
        Debug.Log(scoremanager.currentscore);

    }

    public string RetryScene = "";
    public string NextSceneName = "";
    public string MainMenuName = "";
    public void  LoadScene()
    {
       SaveGame();
       SceneManager.LoadScene(NextSceneName);

    }
    public void Retry()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentScene);
    }
    public void Mainmenu()
    {
        if (RetryScene == "")
            return;
        SceneManager.LoadScene(MainMenuName);
    }
}
