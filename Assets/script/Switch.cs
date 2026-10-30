using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject Door;
    private SpriteRenderer theSR;
    public Sprite downSprite, upSprite;
    public bool open;
    // Start is called before the first frame update
    void Start()
    {
        theSR= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            open = !open;
            theSR.sprite = open ? downSprite : upSprite;
            Door.SetActive(!open);
        }
    }
}
