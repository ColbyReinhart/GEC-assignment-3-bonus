using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is in charge of manipulating the skybox
public class SkyboxController : MonoBehaviour
{
    public static SkyboxController instance = null;
    public float rotateSpeed = 1.2f;

    private void Awake()
    {
        // Setup singleton instance
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", rotateSpeed * Time.time);
    }
}
