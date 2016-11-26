using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
    
    public GameObject BlockPrefab;
    
    private GameObject block;
    private List<GameObject> blocks;
    
    private int score = 0;
    
	void Start() {
        GameObject block = Instantiate(BlockPrefab);
        block.GetComponent<Block>().Initialize(this.score);
        
        blocks = new List<GameObject>();
        blocks.Add(block);
	}
	
	void Update() {
	    if(Input.GetButtonDown("Action")) {
            score += 1;
            blocks.Add(InitializeBlock(score));
        }
        
	    GetCurrentBlock().GetComponent<Block>().Move();
	}
    
    // Instantiates a new block, and initializes
    // it with some parameters of how it should spawn.
    private GameObject InitializeBlock(int height) {
        GameObject block = Instantiate(BlockPrefab);
        block.GetComponent<Block>().Initialize(height);
        return block;
    }
    
    // Returns the block that we're actively
    // stacking on top of all our other blocks.
    // This is the block that is moving.
    private GameObject GetCurrentBlock() {
        return blocks[blocks.Count - 1];
    }
    
    // Returns the block at the top of the stack.
    private GameObject GetPreviousBlock() {
        return blocks[blocks.Count - 2];
    }
}
