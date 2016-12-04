using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public float attackRange = 3f;

	void Update() {
        
        // Initalize the attack as zero.
        Vector2 attackingDirection = Vector2.zero;
        
        // Listen for inputs to begn an attack. An
        // attack can be either to the left or right.
        if(Input.GetButtonDown("Left")) {
            attackingDirection = Vector2.left;
        } if(Input.GetButtonDown("Right")) {
            attackingDirection = Vector2.right;
        }
        
        // If the attack is no longer zero, we
        // begin an attack in that direction.
        if(attackingDirection != Vector2.zero) {
            // Send out a raycast in the direction of the attack. Only raycast within range. Only raycast against the enemy layer.
            RaycastHit2D hit = Physics2D.Raycast(transform.position, attackingDirection, attackRange, LayerMask.GetMask("Enemy"));
            
            // If the raycast hit an enemy...
            if(hit.collider != null) {
                    
                // Move towards the enemy. We want to 
                // teleport in front of the enemy. So we
                // set our position to their position, then
                // take one step backwards.
                transform.position = hit.transform.position;
                transform.Translate(attackingDirection * -1);
                
                // Hit the enemy. This should stun them
                // so they no longer try to attack you.
                hit.collider.gameObject.SendMessage("GetHit");
                                
                // Turn on the physics for the enemy.
                hit.rigidbody.isKinematic = false;
                
                // Calculate the force and torque to push the enemy away.
                Vector2 force = new Vector2(40f * attackingDirection.x, 15f);
                float torque = -10f * attackingDirection.x;
                
                // Push the enemey away. They'll go flying through the air!
                hit.rigidbody.AddForce(force, ForceMode2D.Impulse);
                hit.rigidbody.AddTorque(torque, ForceMode2D.Impulse);
            } else {
                // TODO: If the player tries to attack an
                // enemy that isn't yet in range, then they
                // should "choke", and be stunned for a bit.
                Debug.Log("You choked!");
            }
        }
        
        // The "hitzone" is the gray box rendered below the player. This is
        // meant to show the players where their attacks are in range. If
        // that public variable, `attackRange`, should be updated, we want
        // our hitzone box to reflect that.
        transform.FindChild("Hitzone").transform.localScale = new Vector2(attackRange * 2f, 0.5f);
	}
    
    void OnTriggerEnter2D(Collider2D collider) {
        // TODO: If the player is touched by an
        // enemy, then they should die and restart.
        Debug.Log("You died!");
    }
}
