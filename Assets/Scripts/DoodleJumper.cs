using UnityEngine;
using System.Collections;

public class DoodleJumper : MonoBehaviour {

	public Vector3 velocity = new Vector3(0, 5f, 0);
	public float gravity = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//make character fall
		this.transform.Translate(velocity * Time.deltaTime);

		if (velocity.y > -10) {//don't fall too far
			velocity = new Vector3 (velocity.x, velocity.y - gravity, velocity.z);
		}
	}
	
	void OnCollisionEnter(Collision c){
		if (c.collider.tag == "Platform") {
			velocity = new Vector3(velocity.x, 15f, velocity.z);
		}
	}
}
