using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public enum bossStates { shooting, hurt, moving,ended };
    public bossStates currentState;

    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    public Transform minePoint;
    public GameObject mine;
    public float timeBetweenMines;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float shotCounter;
    [SerializeField] private float timeBetweenShots = 1.5f;
    [SerializeField] private float minTimeBetweenShots = 0.35f;

    [Header("Difficulty")]
    [SerializeField] private float shotSpeedUp = 1.2f;
    [SerializeField] private float mineSpeedUp = 1.2f;
    [SerializeField] private float minTimeBetweenMines = 0.25f;

    private float baseTimeBetweenShots;
    private float baseTimeBetweenMines;
    private int hitCount;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitBox;

    [Header("Health")]
    public float health;
    public GameObject explosion,winPlatform;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = bossStates.shooting;
        baseTimeBetweenShots = timeBetweenShots;
        baseTimeBetweenMines = timeBetweenMines;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case bossStates.shooting:
                shotCounter -= Time.deltaTime;
                if( shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    GameObject newBullet =Instantiate(bullet,firePoint.position,firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }

                break;


            case bossStates.hurt:
                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;
                    if (hurtCounter < 0)
                    {

                        
                       currentState = bossStates.moving;
                        mineCounter = 0f;
                       
                    }
                }

                break;


            case bossStates.moving:
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        moveRight = false;
                        theBoss.localScale = Vector3.one;
                        EndMoving();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        moveRight = true;
                        theBoss.localScale =new Vector3(-1f,1f,1f);
                        EndMoving();
                    }
                }

                mineCounter-= Time.deltaTime;
                if(mineCounter<=0){
                    Instantiate(mine, minePoint.position, minePoint.rotation);
                    mineCounter = timeBetweenMines;

                }

                break;


            
        }

#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
#endif

    }

    public void TakeHit()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;
        anim.SetTrigger("Hit");

        health--;
        AudioManager.instance.PlaySFX(0);
        if (health <= 0)
        {
            
            gameObject.SetActive(false);
            Instantiate(explosion, theBoss.position, theBoss.rotation);
            AudioManager.instance.StopBossMusic();
            winPlatform.SetActive(true);
            currentState = bossStates.ended;
        }
        else
        {
            hitCount++;

            timeBetweenShots = Mathf.Max(minTimeBetweenShots,baseTimeBetweenShots / Mathf.Pow(Mathf.Max(1f, shotSpeedUp), hitCount));

            timeBetweenMines = Mathf.Max(minTimeBetweenMines,baseTimeBetweenMines / Mathf.Pow(Mathf.Max(1f, mineSpeedUp), hitCount));
        }

        BossTankMine.ExplodeAll();
    }
    public void EndMoving()
    {
        currentState = bossStates.shooting;
        shotCounter = 0f;
        anim.SetTrigger("StopMoving");
        hitBox.SetActive(true);
    }
}
