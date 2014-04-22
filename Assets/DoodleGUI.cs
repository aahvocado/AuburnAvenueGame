using UnityEngine;
using System.Collections;

public class DoodleGUI : MonoBehaviour {

	public GameObject jumper;

	private float displayPoints = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DoodleJumper g = jumper.GetComponent<DoodleJumper> ();
		displayPoints = g.getPoints ();
	}

	void OnGUI(){
		DoodleJumper g = jumper.GetComponent<DoodleJumper> ();
		if (GUI.Button (new Rect (4,4,100,30), "Distance: \n" + displayPoints)) {
			//print ("You clicked the button!");
		}

		if (GUI.Button (new Rect (4,44,100,30), "Captures: \n" + g.getCaptures())) {
			//print ("You clicked the button!");
		}
	}
}
