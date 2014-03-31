using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public Camera mainCamera;
	
	public bool startRandom = false;
	
	public GameObject bitObject; 
	GameObject[,] grid; 	
	float width = 2.5f;
	float height = 2.5f;
	
	private float fingerStartTime = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	 
	private bool isSwipe = false;
	private float minSwipeDist = 50.0f;
	private float maxSwipeTime = 0.5f;
	private Vector2 swipeType;
	private int waitTimer;
	public int waitSet = 15;


	// Use this for initialization
	void Start () {
		//randomly populate a grid
		if(startRandom){
			int[,] newGrid = new int[5,5];
			populateGrid(newGrid);
		}else{
			setLevel1();
		}
		
		Vector3 middlePos = grid[2,2].transform.position;
		mainCamera.transform.position = new Vector3(middlePos.x, middlePos.y, middlePos.z - 20);
	}
	
	void populateGrid(int[,] gridList){
		grid = new GameObject[gridList.GetLength(0), gridList.GetLength(1)];
		for(int i = 0;i < gridList.GetLength(0);i++){//col
			for(int j = 0;j < gridList.GetLength(1);j++){//row
				GameObject clone;
				clone = (GameObject)Instantiate(bitObject, new Vector3(width*i, -height*j, 0), Quaternion.identity);
				grid[i,j] = clone;
				
				BitComponent bitObj = grid[i,j].GetComponent<BitComponent>();
				bitObj.setPos(new Vector2(width*i, -height*j));
				
				int spriteNumber = gridList[i,j];
				if(startRandom){
					spriteNumber = Random.Range(0,5);
				}
				
				if(gridList[i,j] != null){
					bitObj.setNum(spriteNumber);
				}else{
					bitObj.setNum(0);
				}
				
				
			}
		}
	}
	
	void setLevel1(){		
		int[,] newGrid = new int[,]{{1,0,0},
									{0,2,0},
									{0,0,1}};
		populateGrid(newGrid);
	}
	
	//
	void addBit(string pos){
		GameObject bit;
		BitComponent bitScript;// = newBit.GetComponent<BitComponent>();
			
		int rand = Random.Range(0, grid.GetLength(0)-1);
		print ("RANDOM "+rand);
		
		switch(pos){
			case "up":
				bit = (GameObject)grid[rand, grid.GetLength(1)-1];
				bitScript = bit.GetComponent<BitComponent>();
				if(bitScript.getNum() == 0){
					bitScript.setNum(1);
				}
				break;
			case "right":
				bitScript = grid[0, rand].GetComponent<BitComponent>();
				if(bitScript.getNum() == 0){
					bitScript.setNum(1);
				}
				break;
			case "down":
				bitScript = grid[rand, 0].GetComponent<BitComponent>();
				if(bitScript.getNum() == 0){
					bitScript.setNum(1);
				}
				break;
			case "left":
				bitScript = grid[grid.GetLength(0)-1, rand].GetComponent<BitComponent>();
				if(bitScript.getNum() == 0){
					bitScript.setNum(1);
				}
				break;
		}
	}
	
	
	//switch the positions of two pieces
	void swapBits(BitComponent A, BitComponent B){
		int numA = A.getNum();
		int numB = B.getNum();
		
		if(numA == numB){//combine
			//B.spriteNum = numA+numB;
			if(B.spriteNum != 0){
				B.spriteNum ++;
			}
			A.spriteNum = 0;
		}else if(numB == 0){//empty
			int tempNum = numB;
			B.spriteNum = numA;
			A.spriteNum = tempNum;
		}
	}
	
	//what happens when right is called
	void activateRight(){
		print ("moving right...");
		for(int i = grid.GetLength(0)-1;i >= 0 ;i--){//col 
			for(int j = grid.GetLength(1)-1;j >= 0;j--){//row
				
				if(i < grid.GetLength(0)-1){
					BitComponent bitA = grid[i,j].GetComponent<BitComponent>();
					BitComponent bitB = grid[i+1,j].GetComponent<BitComponent>();
					swapBits(bitA, bitB);
				}
			}
		}
		
		addBit("right");
	}
	
	void activateLeft(){
		print ("moving left...");
		for(int i = 0;i < grid.GetLength(0);i++){//col 
			for(int j = 0;j < grid.GetLength(1);j++){//row
				
				if(i > 0){
					BitComponent bitA = grid[i,j].GetComponent<BitComponent>();				
					BitComponent bitB = grid[i-1,j].GetComponent<BitComponent>();
					swapBits(bitA, bitB);
				}
				
			}
		}
		
		addBit("left");
	}
	
	void activateUp(){
		print ("moving up...");
		for(int j = 0;j < grid.GetLength(1);j++){//row
			for(int i = 0;i < grid.GetLength(0);i++){//col 
			
				if(j > 0){
			
					BitComponent bitA = grid[i,j].GetComponent<BitComponent>();
					BitComponent bitB = grid[i,j-1].GetComponent<BitComponent>();
					swapBits(bitA, bitB);
				}
				
			}
		}
		
		addBit("up");
	}
	
	void activateDown(){
		print ("moving down...");
		for(int j = grid.GetLength(1)-1;j >= 0;j--){//row
			for(int i = grid.GetLength(0)-1;i >= 0 ;i--){//col 
				if(j < grid.GetLength(1)-1){
				
					BitComponent bitA = grid[i,j].GetComponent<BitComponent>();					
					BitComponent bitB = grid[i,j+1].GetComponent<BitComponent>();
					swapBits(bitA, bitB);
				}
			}
		}
		
		addBit("down");
		
	}
	
	//constantly update grid
	void refreshGrid(){
		/*for(int i = 0;i < grid.GetLength(0);i++){//col
			for(int j = 0;j < grid.GetLength(1);j++){//row
				print (grid[i,j].GetComponent<BitComponent>().getNum());
			}
		}*/
	}
	// Update is called once per frame
	void Update () {
		refreshGrid();
		float horizontalMove = Input.GetAxis("Horizontal");
		swipeType = Vector2.right * horizontalMove;
		if(waitTimer == 0){
			if(horizontalMove > 0){
				activateRight();
				waitTimer = waitSet;
			}else if(horizontalMove < 0){
				activateLeft();
				waitTimer = waitSet;
			}
		}
		
		float verticalMove = Input.GetAxis("Vertical");
		swipeType = Vector2.up * verticalMove;
		if(waitTimer == 0){
			if(verticalMove > 0){
				activateUp();
				waitTimer = waitSet;
			}else if(verticalMove < 0){
				activateDown();
				waitTimer = waitSet;
			}
		}
		
		if (Input.touchCount > 0){
	        foreach (Touch touch in Input.touches){
	            switch (touch.phase){
	                case TouchPhase.Began :
	                    /* this is a new touch */
	                    isSwipe = true;
	                    fingerStartTime = Time.time;
	                    fingerStartPos = touch.position;
	                    break;
	
	                case TouchPhase.Canceled :
	                    /* The touch is being canceled */
	                    isSwipe = false;
	                    break;
	
	                case TouchPhase.Ended :
	                    float gestureTime = Time.time - fingerStartTime;
	                    float gestureDist = (touch.position - fingerStartPos).magnitude;
	                    
	                    if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist){
	                        Vector2 direction = touch.position - fingerStartPos;
	                        swipeType = Vector2.zero;
	                        
	                        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
	                            // the swipe is horizontal:
	                            swipeType = Vector2.right * Mathf.Sign(direction.x);
	                        }else{
							// the swipe is vertical:
	                            swipeType = Vector2.up * Mathf.Sign(direction.y);
	                        }
	                        
	                       //controller.Swipe(swipeType);
							print(swipeType);
	                    }
	
	                    break;
	                
	            }
	        }
	    }
		if(waitTimer > 0){
			waitTimer --;
		}else{
			waitTimer = 0;
		}
	}
	
	void locationSet(int row, int col, GameObject val){
		grid[row,col] = val;
	}
}
