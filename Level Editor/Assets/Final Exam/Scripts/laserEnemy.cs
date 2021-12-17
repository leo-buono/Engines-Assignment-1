using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserEnemy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(laserDeathByTime());
    }
    IEnumerator laserDeathByTime()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Player")
        {
            characterMovement.scoreInt = 0;
            Application.Quit();
        }
    }
}
