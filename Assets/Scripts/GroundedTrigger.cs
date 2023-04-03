using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script should be added to any trigger meant to check if it's parent
// is grounded or not.
public class GroundedTrigger : MonoBehaviour
{
    public bool grounded = false;
    private int currentCollisions = 0;

    private void OnTriggerEnter(Collider other)
    {
        currentCollisions++;
        grounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        currentCollisions--;
        grounded = currentCollisions != 0;
    }
}
