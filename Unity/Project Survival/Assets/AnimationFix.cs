using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFix : MonoBehaviour
{
    public Shooting shootRef;

    public void Reload()
    {
        shootRef.Reloaded();
    }

    public void Block(int i)
    {
        if(i < 5)
        {
            shootRef.block = true;
        }
        else shootRef.block = false;
    }

    public void Switch()
    {
        shootRef.switching = false;
    }
}
