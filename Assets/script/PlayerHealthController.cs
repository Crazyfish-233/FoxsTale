using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int currentHealth, maxHealth;
    public static PlayerHealthController instance;
    public float invincibleCount, invincibleLength;
    private SpriteRenderer theSR;
    public GameObject deathEffect;
    private bool isDead;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCount > 0)
        {
            invincibleCount -= Time.deltaTime;
            if (invincibleCount < 0)
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1);
        }
    }
    public void DealDamage()
    {
        if (isDead) return;

        if (invincibleCount <= 0)
        {
            currentHealth--;
            UIController.Instance.UpdateHealthDisplay();
            if (currentHealth <= 0)
            {
                Kill();
            }
            else
            {
                invincibleCount = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
                PlayerController.instance.KnockBack();
                AudioManager.instance.PlaySFX(9);
            }
            
        }

    }
    public void HealPlayer()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.Instance.UpdateHealthDisplay();
    }

    public void Kill()
    {
        if (isDead) return;
        isDead = true;

        currentHealth = 0;
        UIController.Instance.UpdateHealthDisplay();
        Instantiate(deathEffect, transform.position, transform.rotation);
        AudioManager.instance.PlaySFX(8);
        LevelManager.instance.RespawnPlayer();
    }

    public void Revive()
    {
        isDead = false;
        currentHealth = maxHealth;
        invincibleCount = 0;
        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
        UIController.Instance.UpdateHealthDisplay();
    }

}
