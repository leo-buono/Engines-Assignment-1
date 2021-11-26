using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class EnemyHPHUD : MonoBehaviour
{
	public SpriteRenderer fill;
	public Vector2 size = Vector2.right * 2f + Vector2.up * 0.5f;
	public EnemyAI enemyRef;

	[DllImport("UIDLL-smoothed")]
	private static extern Color GetColour(float percentage);
	private void Start()
	{
		fill.size = size;
	}

    void Update()
    {
		Vector2 newVec = size;
		newVec.x = enemyRef.health / enemyRef.maxHealth * size.x;
		fill.size = newVec;
		fill.color = GetColour(enemyRef.health / enemyRef.maxHealth);
    }
}
