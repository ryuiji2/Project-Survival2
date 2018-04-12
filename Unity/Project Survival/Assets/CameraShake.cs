using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Range(0,2)]
    public float trauma;
    public float maxTrauma = 0.75f;
    public float timeMultiplier = 1f;

    public void FixedUpdate()
    {
        if (trauma > 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x +trauma, 0, 0);
            trauma -= Time.deltaTime * timeMultiplier;
        }
        else trauma = 0;
    }

    public void Shake(float t)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if ((trauma + t) <= maxTrauma)
            {
                trauma += t;
            }
        }
    }
}
