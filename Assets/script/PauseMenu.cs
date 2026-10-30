using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    
    public string levelSelect, mainMenu;
    public GameObject pauseScreen;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { 
            PauseUnpause();
        }
    }
    public void PauseUnpause()
    {
        SetPaused(!GameState.IsPaused);
    }

    public void SetPaused(bool paused)
    {
        GameState.IsPaused = paused;
        pauseScreen.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
    }

    public void LevelSelect()
    {
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        GameState.IsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelSelect);
        
    }

    public void MainMenu()
    {
        GameState.IsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
        
    }
}
