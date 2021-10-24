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
			spawned.Add(Instantiate(EnemyPrefab, transform.position, Quaternion.identity));
		}
    }

	void OnDestroy()
	{
		int index;
		while(spawned.Count > 0) {
			index = spawned.Count - 1;
			Destroy(spawned[index]);
			spawned.RemoveAt(index);
		}
	}
}
