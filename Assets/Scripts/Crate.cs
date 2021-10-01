using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
	private Rigidbody2D rb;
	private bool solid = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate()
	{
		//when solid, do best to ignore other physics
		if (solid) {
			rb.velocity = Vector2.up * rb.velocity.y;
		}
	}

	public void Launch(Vector2 direction) {
		rb.velocity = direction;
		solid = false;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		//cancel all velocity when hitting walls
		//rb.velocity = Vector2.zero;
		solid = true;
	}

	//attack
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == 6) {
			collider.enabled = false;
			Launch(collider.offset * 10 + Vector2.up * 5);
		}
	}
}
