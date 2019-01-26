using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resources : MonoBehaviour {

    private Player player;

    public TextMeshProUGUI scrap;
    public TextMeshProUGUI power;
    public TextMeshProUGUI people;

    // Use this for initialization
    void Start () {
        player = GameManager.instance.player;
	}
	
	// Update is called once per frame
	void Update () {
        scrap.text = player.scrap.ToString();
        power.text = player.energy.ToString();
        people.text = player.people.ToString();
	}
}
