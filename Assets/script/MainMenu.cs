using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string startScene;
    public string continueScene;
    
    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(startScene+"_unlocked"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinueGame()
    {
        GameState.Reset();
        Time.timeScale = 1f;

        SceneManager.LoadScene(continueScene);
    }
    public void NewGame()
    {
        GameState.Reset();
        Time.timeScale = 1f;

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(startScene);
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
