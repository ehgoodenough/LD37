using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed = 25.0f;
	private Rigidbody body;

	void Start() {
		this.body = GetComponent<Rigidbody>();
	}

	void Update() {
		float x = Input.GetAxis ("Horizontal") * 25.0f;
		float z = Input.GetAxis ("Vertical") * 25.0f;
		float y = this.body.velocity.y;

		Vector3 movement = new Vector3(x, y, z);
		this.body.velocity = movement;

		// float x = Input.GetAxis ("Horizontal");
		// float z = Input.GetAxis ("Vertical");
		//
		// Vector3 movement = new Vector3(x, 0.0f, z);
		// this.body.AddForce(movement * 10000);
		//
		// if(this.body.velocity.magnitude > this.speed) {
		// 	this.body.velocity = this.body.velocity.normalized * this.speed;
		// }
	}
}
