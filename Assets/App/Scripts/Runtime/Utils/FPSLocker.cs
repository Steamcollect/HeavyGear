using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLocker : MonoBehaviour
{
    [SerializeField] private int fpsLock = 60;
    
    private void Awake()
    {
        Application.targetFrameRate = fpsLock;
    }
}
