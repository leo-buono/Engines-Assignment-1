using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPHUD : MonoBehaviour
{
	public SpriteRenderer fill;
	public Vector2 size = Vector2.right * 2f + Vector2.up * 0.5f;
	public EnemyAI enemyRef;

	private void Start()
	{
		fill.size = size;
	}

    void Update()
    {
		Vector2 newVec = size;
		newVec.x = enemyRef.health / enemyRef.maxHealth * size.x;
		fill.size = newVec;
    }
}
