using UnityEngine;
using System.Collections;

public class StatsGUI : MonoBehaviour {
	
	public GameObject gameController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		
		if (GUI.Button (new Rect (4,4,100,50), "I am a button \n" + Input.mousePosition.x)) {
			print ("You clicked the button!");
		}
	}
}
