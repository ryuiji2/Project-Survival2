using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFix : MonoBehaviour
{
    public Shooting shootRef;

    public void Reload()
    {
        print("Wa");
        shootRef.Reloaded();
    }
}
