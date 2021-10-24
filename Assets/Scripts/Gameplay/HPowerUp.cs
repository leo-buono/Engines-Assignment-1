using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPowerUp : MonoBehaviour
{
    static float HPup = 3f;
    static float speed = 180;

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
