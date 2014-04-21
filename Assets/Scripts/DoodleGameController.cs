using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoodleGameController : MonoBehaviour {
	public GameObject player;//the doodle jumper
	public GameObject platform;//platform to duplicate
	public Camera maincam;//main camera

	public int platformSpawnCount = 10;
	public float distanceBetweenPlatforms = 2.0f;

	private List<GameObject> platforms = new List<GameObject>();

	// Use this for initialization
	void Start () {
		spawnPlatforms ();
	}
	
	// Update is called once per frame
	void Update () {
		moveCamera ();

		Vector3 playerpos = player.transform.position;
		Vector3 campos = maincam.transform.position;
		//dead player
		if (playerpos.y < campos.y - 13) {
			reset ();
		}

		//build more platforms
		if (platforms.Count > 1) {
				if (player.transform.position.y > platforms [platforms.Count - 1].transform.position.y - 10.0f) {
						despawnPlatforms ();
						spawnPlatforms ();
				}
		}
	}

	void reset(){
		maincam.transform.position = new Vector3 (0, 0, -15);
		player.GetComponent<DoodleJumper> ().reset();
		removeAllPlatforms ();

		spawnPlatforms ();
	}

	void moveCamera(){//handle the camera
		Vector3 playerPos = player.transform.position;
		Vector3 pos = maincam.transform.position;
		float cameraOffset = 3.0f;//how far higher the camera should be relative to the player
		if (playerPos.y+cameraOffset > pos.y) {
			maincam.transform.position = new Vector3 (pos.x, player.transform.position.y+cameraOffset, pos.z);
		}
	}

	void removeAllPlatforms(){


		foreach(GameObject p in platforms){//destroy all platforms
			Destroy (p);
		}
		platforms = new List<GameObject>();
	}

	void despawnPlatforms(){//remove platforms behind player
		for (int i = platforms.Count-1; i>0; i--) {
			if(platforms[i].transform.position.y < player.transform.position.y - 8.0f){
				Destroy (platforms[i]);
				platforms.RemoveAt (i);
			}

		}
	}

	void spawnPlatforms(){//create new platforms
		Vector3 spawnPos = maincam.transform.position;//default spawn position
		if(platforms.Count > 1){//if we're above
			spawnPos = new Vector3(maincam.transform.position.x, platforms[platforms.Count-1].transform.position.y, 0);
		}

		for (int i = 0; i<platformSpawnCount; i++) {
			float xoffset = Random.Range(-5, 5);//randomize position of platform
			float yoffset = Random.Range(-.5f, .5f);//offset y a bit
			float newWidth = Random.Range(2, 4); //randomize size
			Vector3 position = new Vector3(spawnPos.x + xoffset, spawnPos.y+yoffset+distanceBetweenPlatforms*i,0);
			GameObject newPlatform = (GameObject)Instantiate(platform, position, Quaternion.identity);
			newPlatform.transform.localScale = new Vector3(newWidth, .4f, 1);
			platforms.Add (newPlatform);
		}
	}
}
