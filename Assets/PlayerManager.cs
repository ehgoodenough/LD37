using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    private Rigidbody2D body;

    public float jumpSpeed = 20f;
    public float moveSpeed = 10f;

    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate() {
        // Normalize the delta;
        float delta = Time.deltaTime / (1f / 60f);

        Vector2 targetVelocity = body.velocity;
        targetVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        body.velocity += ((targetVelocity - body.velocity) / 4) * delta;

        // If player has hit the jump key...
        if(Input.GetButton("Jump")) {
            // ...And if the player is standing on the ground...
            if(Physics2D.Raycast(transform.position, Vector2.down, 0.25f, LayerMask.GetMask("Floor")).collider != null) {
                // ...Then make them jump!
                body.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D collider) {
        // ...?!
    }
}
