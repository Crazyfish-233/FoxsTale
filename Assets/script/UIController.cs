using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public Image[] Hearts;
    public Sprite HeartFull, HeartEmpty, HeartHalf;
    public Text gemText;
    public Image fadeScreen;
    public float fadeSpeed;
    public bool fadeToBlack, blackToFade;
    public Text levelComplete;
    private const int HealthPerHeart = 2;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UpdateGemCount();
        fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 1f);
        BlackToFade();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack) { 
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

    public void UpdateHealthDisplay()
    {
        if (Hearts == null || Hearts.Length == 0) return;

        int health = Mathf.Max(0, PlayerHealthController.instance.currentHealth);

        for (int i = 0; i < Hearts.Length; i++)
        {
            Hearts[i].sprite = GetHeartSprite(health, i);
        }
    }

    private Sprite GetHeartSprite(int health, int heartIndex)
    {
        int value = health - heartIndex * HealthPerHeart;

        if (value >= HealthPerHeart) return HeartFull;   
        if (value == HealthPerHeart/2f) return HeartHalf;   
        return HeartEmpty;                  
    }

    public void UpdateGemCount()
    {
        gemText.text=LevelManager.instance.gemCollected.ToString();
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

}
