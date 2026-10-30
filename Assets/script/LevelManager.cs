using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int gemCollected;
    public string levelToLoad;
    public float timeInLevel;
    private bool isRespawning;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GameState.Reset();
        Time.timeScale = 1f;
        timeInLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        if (isRespawning) return;
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {

        isRespawning = true;

        float fadeDuration = 1f / UIController.Instance.fadeSpeed;

        GameState.InputBlocked = true;                 
        PlayerController.instance.gameObject.SetActive(false);
        UIController.Instance.FadeToBlack();

        yield return new WaitForSeconds(fadeDuration);

        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;
        PlayerController.instance.theRB.velocity = Vector2.zero;
        PlayerHealthController.instance.Revive();
        PlayerController.instance.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);         

        UIController.Instance.BlackToFade();

        GameState.InputBlocked = false;
        isRespawning = false;
    }

    public void LevelEnd()
    {
        StartCoroutine(LevelEndCo());
    }

    private IEnumerator LevelEndCo()
    {
        AudioManager.instance.PlayLevelVictory();

        GameState.InputBlocked = true;
        CameraController.instance.stopFollow = true;

        UIController.Instance.levelComplete.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        UIController.Instance.FadeToBlack();

        yield return new WaitForSeconds(1f / UIController.Instance.fadeSpeed + 2f);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            if (gemCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemCollected);
            }
        }
        else 
        { 
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemCollected); 
        }

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            if (timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }
        SceneManager.LoadScene(levelToLoad);

    }
}
