using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform target, farBackground, midBackground;
    private Vector2 lastPost;
    public float minHeight, maxHeight;
    public bool stopFollow;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        lastPost = target.position;
        
        farBackground.position = new Vector3(target.position.x, target.position.y, farBackground.position.z);
        midBackground.position = new Vector3(target.position.x, target.position.y, midBackground.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (stopFollow) return; 
        transform.position= new Vector3(target.position.x,Mathf.Clamp(target.position.y,minHeight,maxHeight),transform.position.z);

        Vector2 moveXY = new Vector2(target.position.x - lastPost.x, target.position.y - lastPost.y);
        
        farBackground.position += new Vector3(moveXY.x, moveXY.y, 0f);
        midBackground.position += new Vector3(moveXY.x,moveXY.y, 0f) * .5f;
        lastPost = target.position;
        
    }
}
