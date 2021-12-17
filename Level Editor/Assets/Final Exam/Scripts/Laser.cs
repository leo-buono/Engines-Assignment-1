using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Laser : MonoBehaviour
{
    static public bool hasCollided = false; 
    // Start is called before the first frame update

    IEnumerator laserDeathByTime()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    public void Init(Vector3 _force, float _scale, Vector3 _pos)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Rigidbody>().velocity = _force * _scale;
		transform.position = _pos;
		transform.rotation = Quaternion.identity;
        StartCoroutine(laserDeathByTime());
    }

    void OnCollisionEnter(Collision collider) 
    {
        if(collider.transform.tag == "Enemy")
        {
            //Do thing
            gameObject.SetActive(false);
            Destroy(collider.gameObject);
            hasCollided = true;
        }
        else if(collider.transform.tag == "Backwall")
        {
            gameObject.SetActive(false);
        }
        else if(collider.transform.tag == "Laser")
        {
            gameObject.SetActive(false);
        }
    }
}
