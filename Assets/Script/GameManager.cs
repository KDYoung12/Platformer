using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int keyCount;
     
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        keyCount = 0;   
    }
}
