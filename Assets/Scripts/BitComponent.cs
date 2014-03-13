using UnityEngine;
using System.Collections;

public class BitComponent : MonoBehaviour {
	private string n = "blank"; //component name
	public Sprite[] mats;
	public int spriteNum = 0;
	private Vector2 pos;

	// Use this for initialization
	void Start () {
		//spriteNum = (int)Random.Range(0,mats.Length);
		
		switch(spriteNum){
			case 0:
				n = "empty";
				break;
			case 1:
				n = "1";
				break;
			case 2:
				n = "2";
				break;
			case 3:
				n = "3";
				break;
			case 4:
				n = "4";
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		SpriteRenderer spRe = transform.GetComponent<SpriteRenderer>();
		if(spriteNum > mats.Length){
			spriteNum = mats.Length-1;
		}
		spRe.sprite = mats[spriteNum];
		
		switch(spriteNum){
			case 0:
				transform.localScale = new Vector3(.2f,.2f,.2f);
				break;
			case 1:
				transform.localScale = new Vector3(.62f,.62f,.62f);
				break;
			case 2:
				transform.localScale = new Vector3(.2f,.2f,.2f);
				break;
			case 3:
				transform.localScale = new Vector3(.2f,.2f,.2f);
				break;
			case 4:
				transform.localScale = new Vector3(.2f,.2f,.2f);
				break;
		}
	}
	
	public Vector2 setPos(Vector2 p){
		pos = p;
		return pos;
	}
	public int setNum(int n){
		spriteNum = n;
		return spriteNum;
	}
	public int getNum(){
		return spriteNum;
	}
	public string getName(){
		return n;
	}
}
