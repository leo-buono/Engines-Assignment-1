using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.InteropServices;

public class HPowerUp : MonoBehaviour
{
    [DllImport("ModDLL")]
    public static extern float getHPup();

    static float HPup = 3f;
    static float speed = 180;

    private void Start()
    {
        HPup = getHPup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Player>().ChangeHealth(HPup);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * speed, Vector3.up);
    }
}
