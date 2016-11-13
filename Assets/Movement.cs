using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private float speed = 15.0f;
	private Rigidbody body;

	void Start() {
		this.body = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
    float delta = Time.deltaTime / (1f / 60f);
    
		Vector3 movement = new Vector3(0f, 0f, 0f);
    
    // movement.x = Input.GetAxis("Horizontal");
    // movement.z = Input.GetAxis("Vertical");
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.z = Input.GetAxisRaw("Vertical");
    
    movement = movement.normalized * this.speed * delta;
    
    movement = Camera.main.transform.TransformDirection(movement);
    
    this.body.velocity = movement; // does not preserve velocity.
		// this.body.velocity += movement; // does not slow down yet.
    // this.body.AddForce(movement); // feels unweildy and clunky.
    
		if(this.body.velocity.magnitude > this.speed) {
			this.body.velocity = this.body.velocity.normalized * this.speed;
		}
	}
}
