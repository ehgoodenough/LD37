using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
    private const float GRABDISTANCE = 2f;

    private Rigidbody2D body;
    private Vector2 cursorDirection;
    private GameObject highlightedBlock;

    public float jumpSpeed = 20f;
    public float moveSpeed = 10f;
    public float throwSpeed = 15f;

    private const float NEGLIBILE_DISTANCE = 0.01f;

    private GameObject held;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        cursorDirection = Vector2.down;
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
            if(Physics2D.CircleCast(transform.position, 0.25f, Vector2.down, 0.25f, LayerMask.GetMask("Dirt")).collider != null) {
                if(body.velocity.y <= 0) {
                    // ...Then jump!
                    body.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                }
            }
        }

        Vector3 worldMouseLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
        worldMouseLocation.z = 0;
        cursorDirection = new Vector2(worldMouseLocation.x - transform.position.x, worldMouseLocation.y - (transform.position.y + transform.localScale.y / 2));

        if(held == null)
        {
            RaycastHit2D sampleHit = Physics2D.Raycast(transform.position + new Vector3(0, transform.localScale.y/2, 0), cursorDirection, GRABDISTANCE, LayerMask.GetMask("Dirt"));
            if (sampleHit)
            {
                if (sampleHit.collider.gameObject.tag == "Rock" || sampleHit.collider.gameObject.tag == "Dirt")
                {
                    setNewHighlightedBlock(sampleHit.collider.gameObject);
                }
            }
            else
            {
                unsetHighlightedBlock();
            }
        }

        if (Input.GetKeyDown("space") || Input.GetKeyDown("s") || Input.GetKeyDown("down")) {
            if(held == null) {
                if(highlightedBlock != null && highlightedBlock.name != "Ground") {
                    highlightedBlock.GetComponent<Rigidbody2D>().isKinematic = true;
                    highlightedBlock.GetComponent<BoxCollider2D>().enabled = false;

                    highlightedBlock.transform.parent = transform;
                    highlightedBlock.transform.localPosition = new Vector2(0, 3);
                    highlightedBlock.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    highlightedBlock.GetComponent<Rigidbody2D>().angularVelocity = 0f;
                    highlightedBlock.transform.rotation = Quaternion.identity;

                    held = highlightedBlock;
                    //unsetHighlightedBlock();
                }
            } else {
                held.GetComponent<BoxCollider2D>().enabled = true;
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

    void setNewHighlightedBlock(GameObject newBlock)
    {
        if (highlightedBlock != null)
        {
            unsetHighlightedBlock();
        }
        highlightedBlock = newBlock;
        highlightedBlock.GetComponent<SpriteRenderer>().color = new Color(.4f, .6f, .4f, 1);
    }

    void unsetHighlightedBlock()
    {
        if(highlightedBlock != null)
        {
            if(highlightedBlock.tag == "Rock")
            {
                highlightedBlock.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
            } else if(highlightedBlock.tag == "Dirt")
            {
                highlightedBlock.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            highlightedBlock = null;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        // ...?!
    }
}
