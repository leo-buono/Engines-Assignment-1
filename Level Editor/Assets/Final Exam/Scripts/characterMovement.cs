using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterMovement : MonoBehaviour
{
    private Queue<GameObject> laserPool = new Queue<GameObject>();
    public GameObject laser;
    public Text score;
    string scoreWord = "Score: ";
    public static int scoreInt = 0;
    public float forceScale = 2f;
    public float speed = 1;
    public int laserPoolSize = 15;

    GameObject laserTemp;

    void Start()
    {
        for (int i = 0; i < laserPoolSize; i++)
        {
            laserTemp = Instantiate(laser);
            laserTemp.SetActive(false);
            laserPool.Enqueue(laserTemp);
        }
        laserTemp = null;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(Time.deltaTime * -speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            fireLaser();
        }
        if(Laser.hasCollided)
        {
            scoreInt += 100;
            score.text = scoreWord + scoreInt;
            Laser.hasCollided = false;
        }
    }
    void fireLaser()
    {
        Vector3 forceDir = new Vector3(0, 1.5f, 0);
        laserTemp = laserPool.Dequeue();
        laserTemp.GetComponent<Laser>().Init(forceDir, forceScale, transform.position);
        laserPool.Enqueue(laserTemp);
        // GameObject laserFire = Instantiate(laser, this.transform.position + forceDir, Quaternion.identity);
        // laserFire.GetComponent<Rigidbody>().AddForce(forceDir * forceScale);
        //Object booling
    }
}
