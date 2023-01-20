using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("HeadTransform").transform;
    }

    private void FixedUpdate()
    {
        TrackTarget();
    }

    private void TrackTarget()
    {
        float targetX = target.position.x;
        float targetY = target.position.y;

        transform.position = new Vector2(targetX, targetY);
    }
}
