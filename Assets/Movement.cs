using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed = 700.0f;
	private Rigidbody body;

	void Start() {
		this.body = GetComponent<Rigidbody>();
	}

	void Update() {
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3(x, 0.0f, y);

		this.body.AddForce(movement * speed * Time.deltaTime);
	}
}
