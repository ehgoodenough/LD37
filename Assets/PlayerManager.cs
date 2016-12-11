using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    private Rigidbody2D body;

    public float jumpSpeed = 20f;
    public float moveSpeed = 10f;

    private const float NEGLIBILE_DISTANCE = 0.01f;

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
        if(Input.GetKeyDown("s") || Input.GetKeyDown("down")) {
            // ...And if the player isn't yet holding anything.
            if(held == null) {
                // ...And if the player is standing on something.
                RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.25f, Vector2.down, 0.25f, LayerMask.GetMask("Dirt"));
                if(hit.collider != null && hit.collider.gameObject.name != "Ground") {
                    // ...Then pick it up!
                    hit.rigidbody.isKinematic = true;
                    hit.collider.enabled = false;

                    hit.transform.parent = transform;
                    hit.transform.localPosition = new Vector2(0, 3);

                    held = hit.collider.gameObject;
                }
            } else {
                held.GetComponent<Collider2D>().enabled = true;
                held.GetComponent<Rigidbody2D>().isKinematic = (held.tag == "Rock");
                // held.GetComponent<Rigidbody2D>().isKinematic = false;

                transform.Translate(new Vector2(0f, 0.5f));
                held.transform.localPosition = new Vector2(0f, 0f);
                held.transform.parent = null;

                held = null;
            }
        }

        if(Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.25f, LayerMask.GetMask("Dirt")).collider != null) {
            Camera.main.transform.Translate(0f, (transform.position.y - Camera.main.transform.position.y) / 8f, 0f);
        }

        // if(Physics2D.Raycast(transform.position, Vector2.down, 0.25f, LayerMask.GetMask("Dirt")).collider != null) {
        //     if(Mathf.Abs(transform.position.y - Camera.main.transform.position.y) < NEGLIBILE_DISTANCE) {
        //         Vector3 targetPosition = Camera.main.transform.position;
        //
        //         // targetPosition.y = transform.position.y;
        //         // Camera.main.transform.position = targetPosition.y;
        //     } else {
        //         Camera.main.transform.Translate(0f, (transform.position.y - Camera.main.transform.position.y) / 0.125f, 0f);
        //     }
        // }

        // if(Physics2D.Raycast(transform.position, Vector2.down, 0.25f, LayerMask.GetMask("Dirt")).collider != null) {
        //     float targetY = transform.position.y;
        //     float yPosDifference = targetY - Camera.main.transform.position.y;
        //     if(yPosDifference > 0){
        //         int moveDirection = 1f;
        //     } else{
        //         int moveDirection = -1f;
        //     }
        //     float translateAmount = (yPosDifference)/8.0f;
        //     if(translateAmount > yPosDifference){
        //         translateAmount = yPosDifference;
        //     Camera.main.transform.Translate(0f, translateAmount, 0f);
        // }

	}

    void OnTriggerEnter2D(Collider2D collider) {
        // ...?!
    }
}
