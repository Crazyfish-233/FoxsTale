using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Transform platform;

    private int currentPoint;
    private bool returning;

    private void Awake()
    {
        if (platform == null) platform = transform;

        if (points == null || points.Length < 2)
        {
            Debug.LogWarning($"{name}: 至少需要 2 个路点，已停用", this);
            enabled = false;
        }
    }

    private void Update()
    {
        Transform target = points[currentPoint];
        if (target == null) return;

        platform.position = Vector3.MoveTowards(platform.position, target.position, moveSpeed * Time.deltaTime);

        
        if ((platform.position - target.position).sqrMagnitude > 0.0025f) return;

        if (!returning)
        {
            currentPoint = Mathf.Min(currentPoint + 1, points.Length - 1);
            if (currentPoint == points.Length - 1) returning = true;
        }
        else
        {
            currentPoint = Mathf.Max(currentPoint - 1, 0);
            if (currentPoint == 0) returning = false;
        }
    }
}
