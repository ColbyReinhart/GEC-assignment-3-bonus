// Jimmy Vegas Unity Tutorials
// This Script will rotate our gem

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling gem functionality
public class Gem : MonoBehaviour
{
    public int rotateSpeed = 360; // Degrees per second

	private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
	}
}