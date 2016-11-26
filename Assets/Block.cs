using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    
    public float speed = 0.1f;
    private Vector3 direction = Vector3.right;
    
    public void Initialize(int stack) {
        if(stack % 2 == 0) {
            this.transform.position = new Vector3(-10f, stack + 0.5f, 0f);
            this.direction = new Vector3(+1, 0, 0);
        } else {
            this.transform.position = new Vector3(0f, stack + 0.5f, +10f);
            this.direction = new Vector3(0, 0, -1);
        }
    }
	
	public void Move() {
        // Normalize the delta.
        float delta = Time.deltaTime / (1f / 60f);
        
        // Move the block.
	    this.transform.Translate(this.direction * this.speed * delta);
	}
}
