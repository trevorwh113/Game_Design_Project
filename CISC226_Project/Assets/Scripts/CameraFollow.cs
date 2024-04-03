using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // distance from player
    private Vector3 offset = new Vector3(0f,1f, -10f);          // 1 tile up for the y position
    // defines approx time it takes for camera to reach the target
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    //public NumCoinsText coinsText;

    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        // gradually change a vector to a desired goal over time
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
