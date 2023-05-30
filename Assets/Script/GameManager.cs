using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject pKey;

    public int keyCount;

    public TextMeshProUGUI goldText;
     
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        keyCount = 0;   
    }

    private void Update()
    {
        goldText.text = "GOLD : " + GameObject.FindWithTag("Player").GetComponent<Player>().gold.ToString();
    }

    public void OnPKey()
    {
        Debug.Log("OnPKey");
        pKey.SetActive(true);
    }

    public void OffPKey()
    {
        pKey.SetActive(false);
    }
}
