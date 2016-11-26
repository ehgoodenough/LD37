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
            // TODO:
            // Check if current block overlaps
            // the previous block. If it is not
            // overlapped, the game is over. If it
            // is, bump the score. Divide the block
            // into two blocks, one that overlaps and
            // one that doesn't. Create a new block which
            // has the dimensions of the overlapping block.
            
            score += 1;
            blocks.Add(InitializeBlock(score));
        }
        
	    GetCurrentBlock().GetComponent<Block>().Move();
	}
    
    // Instantiates a new block, and initializes
    // it with some parameters of how it should spawn.
    private GameObject InitializeBlock(int stack) {
        GameObject block = Instantiate(BlockPrefab);
        block.GetComponent<Block>().Initialize(stack);
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
