using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control the movement of a platform between two artitrary transforms
public class Platform : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public float travelTime = 3f; // one-way

    private Vector3 movementPerSec;
    private float secondsTraveled = 0f;

    private void Awake()
    {
        Vector3 travelPath = endPos.position - startPos.position;
        movementPerSec = travelPath / travelTime;
    }

    private void Start()
    {
        transform.position = startPos.position;
    }

    private void Update()
    {
        transform.position += movementPerSec * Time.deltaTime;
        secondsTraveled += Time.deltaTime;

        if (secondsTraveled >= travelTime)
        {
            secondsTraveled = 0f;
            movementPerSec *= -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.parent = transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.parent = null;
    }
}
