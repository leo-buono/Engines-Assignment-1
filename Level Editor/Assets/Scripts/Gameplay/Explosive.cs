using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
	public Collider2D explosion;
	public Rigidbody2D bod;
	public float delay = 0.25f;
	private bool activated = false;
	private float counter = 0;

	void Update() {
		if (counter > 0 && !EditorManager.GetPaused()) {
			counter -= Time.deltaTime;
			if (counter <= 0) {
				gameObject.SetActive(false);
			}
		}
	}

	public void Init(Vector2 velo, Vector3 position) {
		gameObject.SetActive(true);
		explosion.gameObject.SetActive(false);
		activated = false;
		transform.position = position;
		transform.rotation = Quaternion.identity;
		bod.velocity = velo;
		bod.AddTorque(velo.x * -0.5f);
	}

	public void OnCollisionEnter2D(Collision2D collider) {
		explosion.enabled = true;
		explosion.gameObject.SetActive(true);

		//dirty flag to avoid double activation
		if (!activated) {
			activated = true;
			counter = delay;
		}
	}
}
