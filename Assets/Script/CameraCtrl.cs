using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    private void Update()
    {
        // 플레이어와 카메라의 위치를 빼주기
        Vector2 dir = player.transform.position - this.transform.position;
        Vector2 moveVector = new Vector2(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime);
        this.transform.Translate(moveVector);
    }
}
