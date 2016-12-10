using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    private Rigidbody2D body;

    public float jumpSpeed = 20f;
    public float moveSpeed = 10f;

    private GameObject held;

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
        if(Input.GetKey("w") || Input.GetKey("up")) {
            // ...And if the player is standing on the ground...
            if(Physics2D.Raycast(transform.position, Vector2.down, 0.25f, LayerMask.GetMask("Dirt")).collider != null) {
                // ...Then jump!
                body.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }

        // If the player has hit the pickup key...
        if(Input.GetKey("s") || Input.GetKey("down")) {
            // ...And if the player isn't yet holding anything.
            if(held == null) {
                // ...And if the player is standing on something.
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.25f, LayerMask.GetMask("Dirt"));
                if(hit.collider != null && hit.collider.gameObject.name != "Ground") {
                    // ...Then pick it up!
                    hit.rigidbody.isKinematic = true;
                    hit.collider.enabled = false;

                    hit.transform.parent = transform;
                    hit.transform.localPosition = new Vector2(0, 3);

                    held = hit.gameObject;
                }
            } else {
                held.transform.parent = null;
                held.rigidbody.isKinematic = true;
                held.collider.enabled = false;
                
                held = null;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D collider) {
        // ...?!
    }
}
