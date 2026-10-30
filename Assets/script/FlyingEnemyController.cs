using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    public int currentPoint;
    private bool canReturn;
    public SpriteRenderer theSR;
    public float distanceToAttackPlayer, chaseSpeed;
    private Vector3 attackTarget;
    public float waitAttack;
    public float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[currentPoint].position) < .05f)
                {
                    if (!canReturn)
                    {
                        currentPoint++;

                    }
                    else if (canReturn)
                    {
                        currentPoint--;
                    }

                    if (currentPoint == points.Length - 1)
                    {
                        canReturn = true;
                    }
                    else if (currentPoint == 0)
                    {
                        canReturn = false;
                    }

                    theSR.flipX = canReturn;

                }

            }
            else
            {
                if (attackTarget == Vector3.zero)
                {
                    attackTarget = PlayerController.instance.transform.position;
                }

                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, attackTarget) < .1f)
                {
                    attackCounter = waitAttack;
                    attackTarget = Vector3.zero;
                }
            }
        }
    }
}