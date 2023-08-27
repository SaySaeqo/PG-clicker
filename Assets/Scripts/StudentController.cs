using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentController : MonoBehaviour
{
    public float moveSpeed;
    public float opacitySpeed;

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color -= new Color(0f, 0f, 0f, opacitySpeed * Time.deltaTime);

        if (GetComponent<SpriteRenderer>().color == new Color(0f, 0f, 0f, 0.01f))
        {
            Destroy(gameObject);
        }
    }
}
