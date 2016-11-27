using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private float maxSpeed = 10.0f;
	private Rigidbody2D body;

	void Start() {
		body = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
        // Normalize the delta.
        float delta = Time.deltaTime / (1f / 60f);
        
        ///////////////
        // Position //
        /////////////
        
		Vector2 movement = new Vector2(0f, 0f);
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;

        Vector2 targetVelocity = movement * maxSpeed;
        body.velocity += (targetVelocity - body.velocity) / 4 * delta;
        
        ///////////////
        // Rotation //
        /////////////
        
        // Vector3 mousePosition = Input.mousePosition;
        // mousePosition.z = Camera.main.transform.position.z;
        // mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // 
        // float dx = mousePosition.x - transform.position.x;
        // float dy = mousePosition.y - transform.position.y;
        // 
        // float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        // body.rotation = angle;
        
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
	}
}
