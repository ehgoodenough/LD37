using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject DirtBlock;
	public GameObject RockBlock;

	void Start() {
		for(var x = -15f; x <= +15f; x += 1f) {
			for(var y = -1f; y >= -9f; y -= 1f) {
				GameObject block = Instantiate(Random.value < 0.5 ? RockBlock : DirtBlock);
				block.transform.position = new Vector2(x, y);
			}
		}
	}
}
