using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.InteropServices;

public class EnemyAI : MonoBehaviour
{
    //Dll
    [DllImport("ModDLL")]
    public static extern float getEnemyDamage();
    [DllImport("ModDLL")]
    public static extern float getEnemySpeed();
    [DllImport("ModDLL")]
    public static extern float getEnemyHP();

    private float speed = 5f;
	private float health = 5f;
	public bool movingRight = true;
	public bool stunned = false;
	public EnemySpawner spawner;

	private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //DLL
        speed = getEnemySpeed();
        health = getEnemyHP();
    }

    void FixedUpdate()
    {
		if (!stunned) {
			//jsut direction
			Vector2 movement = rb.velocity;
        	if (movingRight)	movement.x = speed;
			else				movement.x = -speed;

			rb.velocity = movement;
		}
    }

	void OnTriggerEnter2D(Collider2D collider)
	{
		//if player trigger
		if (collider.gameObject.layer == 6) {
			health -= Player.getDamage();
			if (health < 0) {
				Destroy(gameObject);
				ActionAudioPlayer.instance.Die();
				return;
			}
			stunned = true;
			//knockback
			rb.velocity = collider.offset * 5f + Vector2.up * 2.5f;
		}
		//if explosive
		else if (collider.gameObject.layer == 9) {
			health -= Player.getDamage();
			if (health < 0) {
				Destroy(gameObject);
				ActionAudioPlayer.instance.Die();
				return;
			}
			stunned = true;
			//knockback
			rb.velocity = ((Vector2)(
				transform.position - collider.gameObject.transform.position
			)).normalized * 7.5f;

			//force bounce
			if (rb.velocity.y <= 5f) {
				Vector2 temp = rb.velocity;
				temp.y = 5f;
				rb.velocity = temp;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 normal = collision.GetContact(0).normal;

		//change direction
		if (Mathf.Abs(normal.x) > 0.5f)
			movingRight = normal.x > 0.5f;

		stunned = false;
	}

	void OnEnable() {
		if (spawner != null) {
			spawner.AddEnemy(gameObject);
		}
	}

	void OnDisable() {
		if (spawner != null) {
			spawner.RemoveEnemy(gameObject);
		}
	}

	void OnDestroy() {
		//remove self from spawner
		if (spawner != null) {
			spawner.RemoveEnemy(gameObject);
		}
	}
}
