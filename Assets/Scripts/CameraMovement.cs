using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector2 boundaries;

    private GameObject player;

    void Start()
    {
        player = GameController.GetInstance().player;
    }

    void Update()
    {
        if(player.transform.position.x > boundaries.x && player.transform.position.x < boundaries.y) {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }    
    }
}
