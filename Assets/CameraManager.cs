using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    
    public float speed = 8;
    
    void FixedUpdate() {
        // Normalize the delta.
        float delta = Time.deltaTime / (1f / 60f);
        
        // Get the position of the player.
        GameObject player = GameObject.Find("Player");
        Vector3 targetPosition = player.transform.position;
        
        targetPosition.z = this.transform.position.z;
        this.transform.position += ((targetPosition - this.transform.position) / this.speed) * delta;
    }
}
