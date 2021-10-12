using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public float speed = 5f;
	public float health = 5f;
	public bool movingRight = true;
	public bool stunned = false;

	private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
			--health;
			if (health < 0) {
				Destroy(gameObject);
				ActionAudioPlayer.PlaySound();
				return;
			}
			stunned = true;
			//knockback
			rb.velocity = collider.offset * 5f + Vector2.up * 2.5f;
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
}
