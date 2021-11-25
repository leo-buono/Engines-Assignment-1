using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.InteropServices;

public class Player : MonoBehaviour
{
    //Dll
    [DllImport("ModDLL")]
    public static extern float getMaxHP();
    [DllImport("ModDLL")]
    public static extern float getSpeed();
    [DllImport("ModDLL")]
    public static extern float getDamage();

	public GameObject prefab;
	private float speed = 5f;
	public float jumpStrength = 10f;

	private Vector2 movement = Vector2.zero;
	private bool grounded = true;
	private bool onWall = false;
	private bool wallOnRight = false;

	public float wallSlideSpeed = 1f;
	public float accelSpeed = 5f;
	public float deccelSpeed = 5f;

	private Rigidbody2D rb;
	public Collider2D attackBox;
	public Transform childTrans;

	public float health = 25f;
	public float maxHealth { get; private set; }
	public Transform worldBottom;
	private float stunTime = 0;
	public Transform spawnPoint;
	
	public GameObject bombPrefab;
	public int bombCount = 5;
	private Queue<GameObject> bombPool = new Queue<GameObject>();

	GameObject temp;
	Vector2 tempVec;

	// Start is called before the first frame update
	void Start()
    {
        //DLL mods
        maxHealth = getMaxHP();
        speed = getSpeed();

        rb = GetComponent<Rigidbody2D>();

		health = maxHealth;

		//initialize explosives
		if (bombCount <= 0)
			bombCount = 1;
		for (int i = 0; i < bombCount; ++i) {
			temp = Instantiate(bombPrefab);
			temp.SetActive(false);
			bombPool.Enqueue(temp);
		}

		temp = null;
	}

    // Update is called once per frame
    void Update()
    {
		if (EditorManager.GetPaused()) {
			return;
		}

		if (transform.position.y <= worldBottom.position.y) {
			//kill player
			ChangeHealth(-maxHealth);
			return;
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
			if (wallOnRight)	movement.x = Mathf.Min(movement.x, 0f);
			else				movement.x = Mathf.Max(movement.x, 0f);

			if (Input.GetButtonDown("Jump") && !grounded) {
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
			if (Input.GetButtonDown("Jump")) {
				movement.y = jumpStrength;
				grounded = false;
			}

		}
		//attack
		if (Input.GetButtonDown("Attack")) {
			attackBox.offset = Vector2.right * hori + Vector2.up * verti;
			childTrans.localPosition = Vector3.right * hori + Vector3.up * verti;
			childTrans.localScale = Vector3.one * 1.5f + Vector3.back * 0.7f;
			attackBox.enabled = true;
		}
		if (Input.GetButtonUp("Attack")) {
			attackBox.enabled = false;
			childTrans.localPosition = Vector3.zero;
			childTrans.localScale = Vector3.one * 0.9f + Vector3.back * 0.4f;
		}
		if (Input.GetButtonDown("Bomb")) {
			tempVec = Vector2.right * hori + Vector2.up * verti;
			temp = bombPool.Dequeue();
			temp.GetComponent<Explosive>().Init(
				(tempVec * (Vector2.right * speed + Vector2.up * 3f)) + Vector2.up * 10f,
				transform.position + (Vector3)tempVec
			);
			bombPool.Enqueue(temp);
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
			ChangeHealth(-EnemyAI.getEnemyDamage());
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		int count = collision.contactCount;
		for (int i = 0; i < count; ++i) {
			tempVec = collision.GetContact(i).normal;

			//grounded check if not already grounded
			if (!grounded)	grounded = tempVec.y > 0.5f;

			//wall check for wall slide only if in air and not on wall
			if (!onWall)	onWall = Mathf.Abs(tempVec.x) > 0.9f;
			wallOnRight = tempVec.x < 0f;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		//wall is gone if you move, it'll get fixed if you are still touching wall
		onWall = false;

		grounded = false;
	}

	public void ChangeHealth(float change) {
		health += change;
		if (health > maxHealth)
			health = maxHealth;
		else if (health <= 0) {
			// health = maxHealth;
			// transform.position = spawnPoint.position;
			// rb.velocity = Vector2.zero;
			SceneChanger.LoadDed();
		}
		else if (change < 0) {
			ActionEventPlayer.instance.Damaged();
		}
	}

	public void Particles() {
		Destroy(Instantiate(prefab, transform.position, Quaternion.identity), 2f);
	}

	void OnEnable()
	{
		ActionEventPlayer.instance.damaged += Particles;
	}
	void OnDisable()
	{
		ActionEventPlayer.instance.damaged -= Particles;
	}
}
