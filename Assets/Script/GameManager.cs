using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI keyText;

    public int keyScore;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        keyScore = 0;
    }
    private void Update()
    {
        keyText.text = "Key : " + keyScore;
    }
}
