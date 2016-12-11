using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    private Rigidbody2D body;

    public float jumpSpeed = 20f;
    public float moveSpeed = 10f;
    public float throwSpeed = 15f;

    private const float NEGLIBILE_DISTANCE = 0.01f;

    private GameObject held;

    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

	void Update() {
        // Normalize the delta;
        float delta = Time.deltaTime / (1f / 60f);

        Vector2 targetVelocity = body.velocity;
        targetVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        body.velocity += ((targetVelocity - body.velocity) / 4) * delta;

        // If player has hit the jump key...
        if(Input.GetKey("w") || Input.GetKey("up")) {
            // ...And if the player is standing on the ground...
            if(Physics2D.Raycast(transform.position, Vector2.down, 0.25f, LayerMask.GetMask("Dirt")).collider != null) {
                if(body.velocity.y <= 0)
                {
                    // ...Then jump!
                    body.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                }                
            }
        }

        if(Input.GetKeyDown("space") || Input.GetKeyDown("s") || Input.GetKeyDown("down")) {
            if(held == null) {
                Vector2 direction = Vector2.down;
                if(Input.GetKey("a") || Input.GetKey("left")) {
                    direction = Vector2.left;
                } if(Input.GetKey("d") || Input.GetKey("right")) {
                    direction = Vector2.right;
                }
                Vector2 position = transform.position;
                position.y += 1f; // maybe just move the anchor of the player to the halfway point instead of re-calculating it here?
                RaycastHit2D hit = Physics2D.CircleCast(position, 0.4f, direction, 1f, LayerMask.GetMask("Dirt"));
                if(hit.collider != null && hit.collider.gameObject.name != "Ground") {
                    hit.rigidbody.isKinematic = true;
                    hit.collider.enabled = false;

                    hit.transform.parent = transform;
                    hit.transform.localPosition = new Vector2(0, 3);
                    hit.rigidbody.velocity = Vector2.zero;
                    hit.rigidbody.angularVelocity = 0;
                    hit.transform.rotation = Quaternion.identity;

                    held = hit.collider.gameObject;
                }
            } else {
                held.GetComponent<Collider2D>().enabled = true;
                held.GetComponent<Rigidbody2D>().isKinematic = (held.tag == "Rock");

                held.transform.parent = null;

                if(held.tag == "Rock") {
                    // transform.Translate(new Vector2(0f, 0.5f));
                    // held.transform.localPosition = new Vector2(0f, 0f);

                    held.transform.position = new Vector3((Mathf.Round(held.transform.position.x)), (Mathf.Round(held.transform.position.y)), 0f);
                } else {
                    if((Input.GetKey("a") || Input.GetKey("left"))) {
                        held.GetComponent<Rigidbody2D>().velocity = new Vector2(-throwSpeed, 5f);
                    } else if((Input.GetKey("d") || Input.GetKey("right"))) {
                        held.GetComponent<Rigidbody2D>().velocity = new Vector2(+throwSpeed, 5f);
                    } else {
                        held.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, throwSpeed);
                    }
                }


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
