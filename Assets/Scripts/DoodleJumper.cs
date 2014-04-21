using UnityEngine;
using System.Collections;

public class DoodleJumper : MonoBehaviour {

	private Vector3 velocity = new Vector3(0, 5f, 0);
	public float speed = 5.0f;
	public float jump = 15.0f;
	public float gravity = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis("Horizontal");
		velocity.x = move * speed;

		//make character fall
		this.transform.Translate(velocity * Time.deltaTime);

		if (velocity.y > -10) {//don't fall too far
			velocity = new Vector3 (velocity.x, velocity.y - gravity, velocity.z);
		}
	}
	
	void OnCollisionEnter(Collision c){
		if (c.collider.tag == "Platform") {
			if(this.transform.position.y > c.transform.position.y){
				velocity = new Vector3(velocity.x, jump, velocity.z);
			}
		}
	}

	public void reset(){
		velocity = new Vector3(0, 5f, 0);
		this.transform.position = new Vector3 (0, 0, 0);
	}
}
