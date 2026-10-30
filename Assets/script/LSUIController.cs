using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LSUIController : MonoBehaviour
{
    public static LSUIController instance;
    public Image fadeScreen;
    public float fadeSpeed;
    public bool fadeToBlack, blackToFade;
    public GameObject levelInfoPanel;
    public Text levelName,gemFound,gemTarget,bestTime,timeTarget;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 1f);
        BlackToFade();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a >= 1f)
            {
                fadeToBlack = false;
            }
        }

        if (blackToFade)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a <= 0f)
            {
                blackToFade = false;
            }
        }
    }
    public void FadeToBlack()
    {
        fadeToBlack = true;
        blackToFade = false;
    }

    public void BlackToFade()
    {
        blackToFade = true;
        fadeToBlack = false;
    }

    public void ShowInfo(MapPoint levelInfo)
    {
        levelName.text = levelInfo.levelName;
        gemFound.text = "FOUND:"+levelInfo.gemCollected;
        gemTarget.text = "IN LEVEL:" + levelInfo.totalGem;
        timeTarget.text = "TARGET:" + levelInfo.targetTime + "s";

        if (levelInfo.bestTime == 0)
        {
            bestTime.text = "BEST:---";
        }
        else
        {
            bestTime.text = "BEST:" + levelInfo.bestTime.ToString("F1") + "s";
        }

        levelInfoPanel.gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        levelInfoPanel.gameObject.SetActive(false);
    }
}
