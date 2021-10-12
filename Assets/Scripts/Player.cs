using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 5f;
	public float jumpStrength = 10f;

	private Vector2 movement = Vector2.zero;
	private bool grounded = true;
	private bool onWall = false;
	private bool wallOnRight = false;

	public float wallSlideSpeed = 1f;
	public float accelSpeed = 5f;
	public float deccelSpeed = 5f;

	private Rigidbody2D rb;
	private BoxCollider2D attackBox;
	private Transform childTrans;

	public float maxHealth = 25f;
	public float worldBottom = 0;
	private float stunTime = 0;
	public Transform spawnPoint;

	// Start is called before the first frame update
	void Start()
    {
		childTrans = GetComponentsInChildren<Transform>()[1];
		rb = GetComponent<Rigidbody2D>();
		//get the trigger box
		BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
		for (int i = 0; i < colliders.Length; ++i) {
			if (colliders[i].isTrigger) {
				attackBox = colliders[i];
				break;
			}
		}
		SingletonHealth.instance.health = maxHealth;
	}

    // Update is called once per frame
    void Update()
    {
		if (transform.position.y <= worldBottom) {
			transform.position = spawnPoint.position;
			rb.velocity = Vector2.zero;
				ActionAudioPlayer.PlaySound();
			SingletonHealth.instance.health = maxHealth;
		}
		
		if (stunTime > 0f) {
			stunTime -= Time.deltaTime;
			if (stunTime <= 0f)
				stunTime = 0f;
		}


		float hori = Input.GetAxis("Horizontal");
		float verti = Input.GetAxis("Vertical");
		
		//reset velocity
		movement.y = rb.velocity.y;

		movement.x = rb.velocity.x;
		//deccel early
		if (grounded)
			movement.x = Mathf.Lerp(movement.x, 0, Time.deltaTime * deccelSpeed);
		
		movement.x = Mathf.Clamp(movement.x + hori * speed * Time.deltaTime * accelSpeed, -speed, speed);

		if (onWall) {
			SmoothFollow.advancedFocus = false;
			if (wallOnRight)	movement.x = Mathf.Min(movement.x, 0f);
			else				movement.x = Mathf.Max(movement.x, 0f);

			if (Input.GetKeyDown(KeyCode.Space)) {
				//diagonal jump
				movement.y = jumpStrength;
				if (wallOnRight)	movement.x = -speed;
				else				movement.x = speed;
				onWall = false;
			}
			//constant fall speed
			else if (movement.y < -wallSlideSpeed)
				movement.y = -wallSlideSpeed;
		}

		if (grounded) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				movement.y = jumpStrength;
				grounded = false;
			}

		}
		//attack
		if (Input.GetKeyDown(KeyCode.E)) {
			attackBox.offset = Vector2.right * hori + Vector2.up * verti;
			childTrans.localPosition = Vector3.right * hori + Vector3.up * verti;
			childTrans.localScale = Vector3.one * 1.5f + Vector3.back * 0.7f;
			attackBox.enabled = true;
		}
		if (Input.GetKeyUp(KeyCode.E)) {
			attackBox.enabled = false;
			childTrans.localPosition = Vector3.zero;
			childTrans.localScale = Vector3.one * 0.9f + Vector3.back * 0.4f;
		}

		//update camera stuff
		SmoothFollow.advancedFocus = Mathf.Abs(movement.x) > 1f;
		SmoothFollow.movingRight = movement.x > 0;

		rb.velocity = movement;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (stunTime == 0f && collision.gameObject.layer == 8) {
			stunTime = 0.5f;
			if (--SingletonHealth.instance.health <= 0) {
				transform.position = spawnPoint.position;
				rb.velocity = Vector2.zero;
				SingletonHealth.instance.health = maxHealth;
				ActionAudioPlayer.PlaySound();
			}
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		int count = collision.contactCount;
		for (int i = 0; i < count; ++i) {
			Vector2 normal = collision.GetContact(i).normal;

			//grounded check if not already grounded
			if (!grounded)	grounded = normal.y > 0.5f;

			//wall check for wall slide only if in air and not on wall
			if (!onWall)	onWall = Mathf.Abs(normal.x) > 0.9f && !grounded;
			wallOnRight = normal.x < 0f;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		//wall is gone if you move, it'll get fixed if you are still touching wall
		onWall = false;

		grounded = false;
	}
}
