using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    float randomSpawnRate = 1f;
    public static List<GameObject> Enemys = new List<GameObject>();
    public GameObject enemy;
    void Start()
    {
        StartCoroutine(SpawnHostile());
    }

    IEnumerator SpawnHostile()
    {
        while(true)
        {
            randomSpawnRate = Random.Range(0.5f, 5f);
            yield return new WaitForSeconds(randomSpawnRate);
            // if(Enemys.Count != 6)
            // {
                Enemys.Add(Instantiate(enemy));
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
