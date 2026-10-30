using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRider : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayers;
    [SerializeField] private float normalThreshold = 0.5f;
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;
    private readonly ContactPoint2D[] contactBuffer = new ContactPoint2D[8];
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    
    void LateUpdate()
    {
        if (currentPlatform == null) return;
        Vector3 delta = currentPlatform.position - lastPlatformPosition;
        lastPlatformPosition = currentPlatform.position;
        rb.position += (Vector2)delta;
    }
    private void OnDisable()
    {
        currentPlatform = null;
        lastPlatformPosition = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & platformLayers) == 0) return;

        int count = collision.GetContacts(contactBuffer);
        for (int i = 0; i < count; i++)
        {

            ContactPoint2D c = contactBuffer[i];
            if (c.normal.y <= normalThreshold) continue;

            currentPlatform = collision.transform;
            lastPlatformPosition = currentPlatform.position;
            return;

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (currentPlatform == collision.transform) currentPlatform = null;
    }
}

