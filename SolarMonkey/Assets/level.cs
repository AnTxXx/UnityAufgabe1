using UnityEngine;
using System.Collections;

public class level : MonoBehaviour {


	public Transform brick;

	public GameObject obstacle; 
	public bool pause=true, gameOver=false;
	public KeyCode space, moveEnemies, moveObstacles;
	public GUIText status;

	public GameObject ship, enemyTop, enemyBottom;

	private bool horizontal=true;
	private int curLevel=0;
	private int brickCount=0;
	private int pauseCounter=0;
	private int rotationCounter=0;
	private const float BORDER_Y=2.8f, BORDER_X=5f;
	private float changeEnemyPos, changeObstaclePos;


	void Start () {
		buildLevel(curLevel);
		buildObstacle(curLevel);
		changeEnemyPos=Time.time;
		changeObstaclePos=Time.time;


	}
	

	void Update () {

		if(gameOver) {
			Time.timeScale = 0;

			if(Input.GetKey(space)) {
				Time.timeScale = 1;
				Application.LoadLevel (Application.loadedLevelName);
			}

		} else {


			if(pauseCounter>0) {
				pause=true;
				pauseCounter--;
				Time.timeScale = 0;
			}
			else {
				pause=false;
				Time.timeScale = 1;
				status.text = "";

				if(Input.GetKey(moveEnemies) && ((Time.time-changeEnemyPos)>0.6f) ) {
					horizontal=!horizontal;
					changeEnemyPos=Time.time;
				}

				if(Input.GetKey(moveObstacles) && ((Time.time-changeObstaclePos)>0.6f) ) {
					changeObstaclePos=Time.time;
					rotationCounter=90;

					GameObject[] gos;
					gos = GameObject.FindGameObjectsWithTag("obstacle");
					foreach (GameObject go in gos) {
						go.GetComponent<obstacle>().changeOrientation();
					}
				}

				if(rotationCounter>0) {
					rotationCounter--;
					GameObject o = GameObject.Find("Obstacles");
					o.transform.Rotate(new Vector3(0,0,1));
				}




			}


			if((brickCount==0) && (pauseCounter==0)) {
				status.text = "Level completed";
				pauseCounter=100;

				ship.transform.position =  new Vector3(0, 0, 0);
				horizontal=true;

				buildLevel(++curLevel);
				buildObstacle(curLevel);
			}

		}

	}

	public void removeBrick() {
		brickCount--;
	}

	public void setPauseCounter(int c, string s) {
		status.text = s;
		pauseCounter=c;
	}

	public void setGameOver() {
		status.text = "GAME OVER";
		gameOver=true;
	}

	
	public bool isPaused() {
		return pause;
	}

	public bool isHorizontal() {
		return horizontal;
	}


	void buildLevel(int level) {

		Vector2 offset = new Vector2(0.7f, 0.7f); // distance between bricks
		//Array bricks = Array.CreateInstance( typeof(Int32), 9 , 9 ); // size of field

		int[][] bricks = new int[9][];

		switch (level%4) {
		case 0:
			bricks[0] = new int[9] {0,0,0, 0,0,0, 0,0,0};
			bricks[1] = new int[9] {0,0,0, 0,0,0, 0,0,0};
			bricks[2] = new int[9] {0,0,0, 0,0,0, 0,0,0};
			bricks[3] = new int[9] {0,0,0, 0,0,0, 0,0,0};
			bricks[4] = new int[9] {1,1,1, 0,0,0, 1,1,1};
			bricks[5] = new int[9] {0,0,0, 0,0,0, 0,0,0};
			bricks[6] = new int[9] {0,0,0, 0,0,0, 0,0,0};
			bricks[7] = new int[9] {0,0,0, 0,0,0, 0,0,0};
			bricks[8] = new int[9] {0,0,0, 0,0,0, 0,0,0};
			break;

		case 1:
			bricks[0] = new int[9] {0,0,0, 0,1,0, 0,0,0};
			bricks[1] = new int[9] {0,0,0, 0,1,0, 0,0,0};
			bricks[2] = new int[9] {0,0,0, 0,1,0, 0,0,0};
			bricks[3] = new int[9] {0,0,0, 0,0,0, 0,0,0};
			bricks[4] = new int[9] {1,1,1, 0,0,0, 1,1,1};
			bricks[5] = new int[9] {0,0,0, 0,0,0, 0,0,0};
			bricks[6] = new int[9] {0,0,0, 0,1,0, 0,0,0};
			bricks[7] = new int[9] {0,0,0, 0,1,0, 0,0,0};
			bricks[8] = new int[9] {0,0,0, 0,1,0, 0,0,0};
			break;
			
			
		case 2:
			bricks[0] = new int[9] {0,0,0, 1,1,1, 0,0,0};
			bricks[1] = new int[9] {0,0,0, 1,1,1, 0,0,0};
			bricks[2] = new int[9] {0,0,0, 1,1,1, 0,0,0};
			bricks[3] = new int[9] {1,1,1, 0,0,0, 1,1,1};
			bricks[4] = new int[9] {1,1,1, 0,0,0, 1,1,1};
			bricks[5] = new int[9] {1,1,1, 0,0,0, 1,1,1};
			bricks[6] = new int[9] {0,0,0, 1,1,1, 0,0,0};
			bricks[7] = new int[9] {0,0,0, 1,1,1, 0,0,0};
			bricks[8] = new int[9] {0,0,0, 1,1,1, 0,0,0};
			break;

		case 3:
			bricks[0] = new int[9] {0,1,1, 1,1,1, 1,1,0};
			bricks[1] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[2] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[3] = new int[9] {1,1,1, 0,0,0, 1,1,1};
			bricks[4] = new int[9] {1,1,1, 0,0,0, 1,1,1};
			bricks[5] = new int[9] {1,1,1, 0,0,0, 1,1,1};
			bricks[6] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[7] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[8] = new int[9] {0,1,1, 1,1,1, 1,1,0};
			break;
		default:
			bricks[0] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[1] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[2] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[3] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[4] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[5] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[6] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[7] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			bricks[8] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			break;
		}


		// remove all bombs
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("bomb");
		foreach (GameObject go in gos) {
			Destroy(go);
		}
		
		// remove all old obstacles
		gos = GameObject.FindGameObjectsWithTag("obstacle");
		foreach (GameObject go in gos) {
			Destroy(go);
		}


		// 9 bricks, start at -5 bricks offset from origin (0,0)
		float left = -5f*offset.x;
		float y=-5f*offset.x, x=left;

		for(int i=0; i<9; i++) {
			x=left;
			y+=offset.y;

			for(int j=0; j<9; j++) {
				x+=offset.x;

				if(bricks[i][j] == 1) {
				
					//allBricks.Add(Instantiate(brick, new Vector3(x, y, 0), Quaternion.identity) as GameObject);

					brickCount++;
					Instantiate(brick, new Vector3(x, y, 0), Quaternion.identity);
					
				}

			}
		}

	}



	void buildObstacle(int level) {
		
		Vector2 offset = new Vector2(0.9f, 0.9f); // distance between bricks

		int[][] obstacles = new int[9][];
		
		switch (level) {

		case 0:
			obstacles[0] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[1] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[2] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[3] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[4] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[5] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[6] = new int[9] {0,0,2, 1,1,1, 1,1,1};
			obstacles[7] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[8] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			break;

			
		case 1:
			obstacles[0] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[1] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[2] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[3] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[4] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[5] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[6] = new int[9] {0,0,2, 1,1,1, 1,1,1};
			obstacles[7] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[8] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			break;


		case 2:
			obstacles[0] = new int[9] {0,0,2, 0,0,2, 0,0,0};
			obstacles[1] = new int[9] {0,0,2, 0,0,2, 0,0,0};
			obstacles[2] = new int[9] {0,0,2, 1,1,2, 0,2,0};
			obstacles[3] = new int[9] {1,1,2, 0,0,0, 0,2,0};
			obstacles[4] = new int[9] {0,0,2, 0,0,0, 0,2,0};
			obstacles[5] = new int[9] {1,1,2, 0,0,0, 0,0,0};
			obstacles[6] = new int[9] {0,0,2, 1,1,1, 1,1,1};
			obstacles[7] = new int[9] {0,0,2, 0,0,0, 2,0,0};
			obstacles[8] = new int[9] {0,0,2, 1,1,1, 2,0,0};
			break;

		case 3:
			obstacles[0] = new int[9] {0,0,2, 0,0,2, 0,0,0};
			obstacles[1] = new int[9] {0,0,2, 0,0,2, 0,0,0};
			obstacles[2] = new int[9] {0,0,2, 0,0,2, 0,0,0};
			obstacles[3] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			obstacles[4] = new int[9] {0,0,2, 0,0,0, 0,2,0};
			obstacles[5] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			obstacles[6] = new int[9] {0,0,2, 0,0,0, 2,0,0};
			obstacles[7] = new int[9] {0,0,2, 0,0,0, 2,0,0};
			obstacles[8] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			break;

		case 4:
			obstacles[0] = new int[9] {0,0,2, 2,0,2, 2,0,0};
			obstacles[1] = new int[9] {0,0,2, 2,0,2, 2,0,0};
			obstacles[2] = new int[9] {0,0,2, 2,0,2, 2,0,0};
			obstacles[3] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			obstacles[4] = new int[9] {0,0,2, 0,0,0, 0,2,0};
			obstacles[5] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			obstacles[6] = new int[9] {0,0,2, 2,0,2, 2,0,0};
			obstacles[7] = new int[9] {0,0,2, 2,0,2, 2,0,0};
			obstacles[8] = new int[9] {1,1,1, 1,1,1, 1,1,1};
			break;
			
			
		default:
			obstacles[0] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[1] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[2] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[3] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[4] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[5] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[6] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[7] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			obstacles[8] = new int[9] {0,0,2, 0,0,0, 0,0,0};
			break;
		}
		
		
		// 9 bricks, start at -5 bricks offset from origin (0,0)
		float left = -5f*offset.x;
		float y=-5f*offset.x, x=left;

		GameObject parentObstacles = GameObject.Find("Obstacles");

		for(int i=0; i<9; i++) {
			x=left;
			y+=offset.y;
			
			for(int j=0; j<9; j++) {
				x+=offset.x;
				
				if(obstacles[i][j] == 1) {

					GameObject g = Instantiate(obstacle, new Vector3(x, y, 0), Quaternion.identity) as GameObject;

					g.transform.parent = parentObstacles.transform;
					g.GetComponent<obstacle>().setAsHorizontal(true);


				} else if(obstacles[i][j] == 2) {
					GameObject g = Instantiate(obstacle, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
					
					g.transform.parent = parentObstacles.transform;
					g.GetComponent<obstacle>().setAsHorizontal(false);

				} 
			}
		}

	}
}	
