using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject pKey;

    public int keyCount;
     
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        keyCount = 0;   
    }

    public void OnPKey()
    {
        pKey.SetActive(true);
    }
    public void OffPKey()
    {
        pKey.SetActive(false);
    }
}
