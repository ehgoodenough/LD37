using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBlockScript : MonoBehaviour {

    public GameObject Player;
    PlayerManager playerScript;

    // Use this for initialization
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        playerScript.tryToHighlight(this.gameObject);
    }
}