using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luciole : MonoBehaviour
{

    private Vector2 luciolePosition;

    private float originalX;
    public float speedX = 1f;
    public float moveStrengthX = 1f;

    private float originalY;
    public float speedY = 1f;
    public float moveStrengthY = 1f;


    void Start()
    {

        this.originalX = this.transform.position.x;
        this.originalY = this.transform.position.y;

        speedX = UnityEngine.Random.Range(0.2f, 0.3f);
        moveStrengthX = UnityEngine.Random.Range(-1.5f, 1.5f);

        speedY = UnityEngine.Random.Range(0.5f, 0.8f);
        moveStrengthY = UnityEngine.Random.Range(-0.2f, 0.2f);

    }

    void Update()
    {
        luciolePosition = transform.position;
        luciolePosition.x = (Mathf.Sin(Time.time * speedX) * moveStrengthX);
        luciolePosition.y = (Mathf.Sin(Time.time * speedY) * moveStrengthY);
        transform.position = new Vector3(
            originalX + ((float)Math.Sin(Time.time * speedX) * moveStrengthX),
            originalY + ((float)Math.Sin(Time.time * speedY) * moveStrengthY),
            transform.position.z
            );
    }


}
