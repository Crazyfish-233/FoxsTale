using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;
    public float moveSpeed;
    public bool levelLoading, isMoving;

    public LSManager theManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentPoint.transform.position) <= .01f)
        { isMoving = false; }
        if (!isMoving && !levelLoading)
        {
            if (Input.GetAxisRaw("Horizontal") > .5f)
            {
                if (currentPoint.right != null)
                {
                    isMoving = true;
                    NextPoint(currentPoint.right);
                }
            }
            else if (Input.GetAxisRaw("Horizontal") < -.5f)
            {
                if (currentPoint.left != null)
                {
                    isMoving = true;
                    NextPoint(currentPoint.left);
                }
            }
            else if (Input.GetAxisRaw("Vertical") > .5f)
            {
                if (currentPoint.up != null)
                {
                    isMoving = true;
                    NextPoint(currentPoint.up);
                }
            }
            else if (Input.GetAxisRaw("Vertical") < -.5f)
            {
                if (currentPoint.down != null)
                {
                    isMoving = true;
                    NextPoint(currentPoint.down);
                }
            }
            if (currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                LSUIController.instance.ShowInfo(currentPoint);

                if (Input.GetButtonDown("Jump"))
                {
                    levelLoading = true;
                    theManager.LoadLevel();
                }
            }
        }
    }

    public void NextPoint(MapPoint point)
    {
        AudioManager.instance.PlaySFX(5);
        currentPoint = point;
        LSUIController.instance.HideInfo();
    }
}
