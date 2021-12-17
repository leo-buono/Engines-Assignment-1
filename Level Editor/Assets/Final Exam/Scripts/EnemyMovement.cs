using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    float firerate = 1f;
    public GameObject laser;
    IEnumerator Shoot()
    {
        while(true)
        {
            firerate = Random.Range(1f, 5f);
            yield return new WaitForSeconds(firerate);
            GameObject LaserFire = Instantiate(laser, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);
            LaserFire.GetComponent<Rigidbody>().velocity = new Vector3(0, -1.5f * 2, 0);
        }
    }
    float T = 0f;
    Vector3 startPos;
    Vector3 endPos;
    int opposite = 1;
    public float speedScale = 10;
    void Start()
    {
        StartCoroutine(Shoot());
        int random = Random.Range(-1, 4);
        speedScale = Random.Range(0.1f, 1);
        startPos = new Vector3(-8, random, 0);
        endPos = new Vector3(8, random, 0);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPos, endPos, T);
        T += Time.deltaTime * speedScale * opposite;
        if(T > 0.98)
        {
           opposite = -1;
        }
        else if(T < 0.01)
        {
            opposite = 1;
        }
    }
}
