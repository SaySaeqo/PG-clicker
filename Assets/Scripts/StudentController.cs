using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentController : MonoBehaviour
{
    public float moveSpeed;
    public float opacitySpeed;
    private float fallSpeed;

    void Start(){
        var MAX_FALL_SPEED = 3f;
        fallSpeed = UnityEngine.Random.Range(-0,MAX_FALL_SPEED);
    }

    // Update is called once per frame
    void Update()
    {
        var RIGHT = 180;
        transform.Translate(transform.rotation.y == RIGHT ? new Vector3(moveSpeed,-fallSpeed,0)* Time.deltaTime : new Vector3(-moveSpeed,-fallSpeed,0)*Time.deltaTime);
        GetComponent<SpriteRenderer>().color -= new Color(0f, 0f, 0f, opacitySpeed * Time.deltaTime);

        if (GetComponent<SpriteRenderer>().color.a < 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
