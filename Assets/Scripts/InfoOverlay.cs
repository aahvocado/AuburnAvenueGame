using UnityEngine;
using System.Collections;

public class InfoOverlay : MonoBehaviour {

	public int textNum = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<TextMesh>().text = "Auburn Avenue";
		switch (textNum) {
		case 1:
			this.GetComponent<TextMesh>().text = "the historic Main \nStreet of Black Atlanta,";
			break;
		case 2:
			this.GetComponent<TextMesh>().text = "traces its roots to \nthe 19th century.";
			break;
		case 3:
			this.GetComponent<TextMesh>().text = "After the Civil War,";
			break;
		case 4:
			this.GetComponent<TextMesh>().text = "freed Blacks established \nShermantown,";
			break;
		case 5:
			this.GetComponent<TextMesh>().text = "a neighborhood bordered on \nthe south by Wheat Street.";
			break;
		case 6:
			this.GetComponent<TextMesh>().text = "Shermantown attracted both \nBlack and White settlers";
			break;
		case 7:
			this.GetComponent<TextMesh>().text = "and the neighborhood expanded.";
			break;
		}
	}
}
