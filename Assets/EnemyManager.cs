using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
    
    public float speed = 0.05f;
    public Vector2 direction = Vector2.left;
    
    private bool hasBeenHit = false;
    
	void Update() {
        // If has not yet been hit...
        if(hasBeenHit == false) {
            
            // Normalize the delta.
            float delta = Time.deltaTime / (1f / 60f);
            
            // Move towards the player.
            transform.Translate(direction * speed * delta);
        }
	}
    
    void GetHit() {
        hasBeenHit = true;
    }
}
