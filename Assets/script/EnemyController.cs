using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed,waitTime,moveTime;
    public Transform LeftPoint,RightPoint;
    private bool movingRight;
    private float moveCount, waitCount;
    private Rigidbody2D theRB;
    public SpriteRenderer theSR;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        theRB= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        LeftPoint.parent = null;
        RightPoint.parent = null;
        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitCount <= 0)
        {
            if (moveCount > 0)
            {
                if (!movingRight)
                {
                    theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                    if (transform.position.x < LeftPoint.position.x)
                    {
                        theSR.flipX = true;
                        movingRight = true;
                    }
                }
                if (movingRight)
                {
                    theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                    if (transform.position.x > RightPoint.position.x)
                    {
                        theSR.flipX = false;
                        movingRight = false;
                    }
                }

                moveCount -= Time.deltaTime;
                
            }
            if (moveCount <= 0)
            {
                theRB.velocity = new Vector2(0f, theRB.velocity.y);
                waitCount = Random.Range(waitTime * .5f, waitTime * 1.5f);
            }
            anim.SetBool("isMoving", true);
        }
        else if(waitCount>0)
        {
            waitCount -= Time.deltaTime;
            
            if (waitCount<=0) { moveCount = moveTime*Random.Range(.5f,  1f); }
            anim.SetBool("isMoving", false);
        }
    }
}
