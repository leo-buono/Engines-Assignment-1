using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject EnemyPrefab;
	public float timerMin = 5f;
	public float timerMax = 10f;
	public float counter = 0f;
	public int maxCount = 5;
	private List<GameObject> spawned = new List<GameObject>();

	GameObject temp;

    // Start is called before the first frame update
    void Start()
    {
        counter = Random.Range(timerMin, timerMax);
    }

    // Update is called once per frame
    void Update()
    {
		if (spawned.Count >= maxCount || EditorManager.GetPaused())	return;

		counter -= Time.deltaTime;
        if (counter <= 0) {
        	counter = Random.Range(timerMin, timerMax);
			temp = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
			temp.GetComponent<EnemyAI>().spawner = this;
			spawned.Add(temp);
			temp = null;
		}
    }

	// void OnDisable()
	// {
		//kill all children
	// 	int index = 0;
	// 	while(spawned.Count > 0) {
	// 		index = spawned.Count - 1;
	// 		if (spawned[index] != null)
	// 			Destroy(spawned[index]);
	// 		spawned.RemoveAt(index);
	// 	}
	// }

	public void AddEnemy(GameObject enemy) {
		spawned.Add(enemy);
	}

	public void RemoveEnemy(GameObject enemy) {
		spawned.Remove(enemy);
	}
}
