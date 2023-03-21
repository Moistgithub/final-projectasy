using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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

    public string NextSceneName = "";
    public void  LoadScene()
    {
       SaveGame();
       SceneManager.LoadScene(NextSceneName);

    }
}
