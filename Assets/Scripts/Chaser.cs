using UnityEngine;
using System.Collections;

public class Chaser : MonoBehaviour {
	public Camera maincam; //get camera just to check pos

	private Vector3 velocity = new Vector3(0,0,0);
	public float flyspeed = 2.0f;//speed this flies up
	public float speed = 1.5f;//speed this moves left and right

	public bool isChasing = false;

	private string action = "left";

	// Use this for initialization
	void Start () {
		reset ();
		this.renderer.material.color = Color.yellow;
	}
	//completely restart chaser at bottom
	public void reset(){
		this.transform.position = new Vector3 (0, 0, 0);

	}
	//yay we did it
	public void caught(){
		respawn ();
		speed += 2f;
	}

	//chaser now visible, show facts, run from player etc
	public void respawn(){
		this.transform.position =  new Vector3(0, maincam.transform.position.y + 4.2f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (isChasing) {
			this.gameObject.SetActive (true);
			//flyAround ();
			move ();
		} else {
			this.gameObject.SetActive (false);
		}
	}

	void flyAround(){
		Vector3 pos = this.transform.position;
		Vector3 campos = maincam.transform.position;
		float leftoffset = 4.0f;
		float rightoffset = -4.0f;
		//move the way you should
		if (action == "left" && pos.x > campos.x + leftoffset) {
			this.transform.Translate(new Vector3(-flyspeed, 0, 0) * Time.deltaTime);
		} else if(action == "right" && pos.x < campos.x + rightoffset){
			this.transform.Translate(new Vector3(flyspeed, 0, 0)* Time.deltaTime);
		}
		//change directions
		if (action == "left" && pos.x < campos.x + leftoffset) {
			action = "right";
		}else if(action == "right" && pos.x < campos.x + rightoffset){
			action = "left";
		}
	}

	void move(){
		velocity = new Vector3 (0, flyspeed, 0);
		this.transform.Translate(velocity * Time.deltaTime);
	}
}
