using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameM : MonoBehaviour
{

	public GameObject logGB;
	private static Text log;

	public GameObject stonePrefab;
	public GameObject woodPrefab;
	public GameObject[] bgTile;

	public GameObject player;

	public int speed;

	Animator anim;
	private int upAnimation = Animator.StringToHash ("player_up");
	private int downAnimation = Animator.StringToHash ("player_down");
	private int leftAnimation = Animator.StringToHash ("player_left");
	private int rightAnimation = Animator.StringToHash ("player_right");
	private int playAnimation;

	private enum KEY_Direction
	{
		NONE,
		KEY_DOWN_LEFT,
		KEY_DOWN_RIGHT,
		KEY_DOWN_TOP,
		KEY_DOWN_DOWN,
		KEY_DOWN_BOT
	}


	private static KEY_Direction movementDirection;
	private int playerX;
	private int playerY;
	private float tileSize;
	private int tileXSize;
	private int tileYSize;
	private string[] tempLevel = new string[] {
		"sssssssssssssssssssssssssssssssssssssss",
		"s000000000000000000000000000000w000000s",
		"swwwwwwwwwwwwwwwwwwwwwwwwwwwwwww000000s",
		"s000000000000000000000000000000w000000s",
		"s000sssss00sssss0sssssss0000000w000000s",
		"s000s000s00s000s0w00000s0000000w000000s",
		"s000s0w0swws0w0sww0ws00w0000000w000000s",
		"s000s000s00s000s0w0ws00wsssss00w000000s",
		"s000sssss00sssss0w0ws00ws000s0sws00000s",
		"s000000000x000000w0wssww0000swsws00000s",
		"swwwwwwwwwwwwwww0w00sswwsssss0sws00000s",
		"s0000000000000000w0wssww0000swsws00000s",
		"s000sssss00sssss0w00s00ws000s0sws00000s",
		"s000s000s00s000s0w0ws00wsssss00w000000s",
		"s000s0w0swws0w0sww00s00w0000000w000000s",
		"s000s000s00s000s0w00000s0000000w000000s",
		"s000sssss00sssss0sssssss0000000w000000s",
		"s000000000000000000000000000000w000000s",
		"s000000000000000000000000000000w000000s",
		"sssssssssssssssssssssssssssssssssssssss"
	};

	private char[,] level;

	private GameObject[,] levelObj;


	static GameM instance;

	public static GameM Instance {
		get {
			return instance;
		}
	}

	void Awake ()
	{
		instance = this;
	}

	void Start ()
	{

		log = logGB.GetComponent<Text> ();

		tileXSize = tempLevel [0].Length;
		tileYSize = tempLevel.Length;
		level = new char[tileYSize, tileXSize];
		levelObj = new GameObject[tileYSize, tileXSize];

		for (int y = 0; y < tileYSize; y++) {
			char[] tempArray = tempLevel [tileYSize - 1 - y].ToCharArray ();
			for (int x = 0; x < tileXSize; x++) {
				level [y, x] = tempArray [x];
			}
		}

		anim = player.GetComponent<Animator> ();

		SpriteRenderer spriteRenderer = stonePrefab.GetComponent<SpriteRenderer> ();
		tileSize = 0.7f;//spriteRenderer.bounds.size.x;
		Debug.Log (tileSize);

		for (int y = 0; y < tileYSize; y++) {
			for (int x = 0; x < tileXSize; x++) {
				if (x % 2 == 0 && y % 2 == 0) {
					GameObject bgTileGO = Instantiate (bgTile [Random.Range (0, 2)]) as GameObject;
					bgTileGO.transform.position = new Vector2 (x * tileSize + tileSize / 2, y * tileSize + tileSize / 2);
				}
				switch (level [y, x]) {
				case 'x': 
					playerX = x;
					playerY = y;
					player.transform.position = new Vector2 (x * tileSize, y * tileSize);
					player_transform_position = player.transform.position;
					break;
				case 's': 
					GameObject stoneGO = Instantiate (stonePrefab) as GameObject;
					stoneGO.transform.position = new Vector2 (x * tileSize, y * tileSize);
					levelObj [y, x] = stoneGO;
					break;
				case 'w': 
					GameObject woodGO = Instantiate (woodPrefab) as GameObject;
					woodGO.transform.position = new Vector2 (x * tileSize, y * tileSize);
					levelObj [y, x] = woodGO;
					break;
				}
			}
		}

	}

	Vector2 player_transform_position;
	Vector2 player_position;

	float moveX = 0;
	float moveY = 0;

	char c;

	void Update ()
	{

		player_position = player.transform.position;

		if ((moveX > 0 && player_position.x >= player_transform_position.x) || (moveX < 0 && player_position.x <= player_transform_position.x)) {
			moveX = 0;
			player.transform.position = player_transform_position;
		}

		if ((moveY > 0 && player_position.y >= player_transform_position.y) || (moveY < 0 && player_position.y <= player_transform_position.y)) {
			moveY = 0;
			player.transform.position = player_transform_position;
		}
			
		player.transform.Translate (new Vector2 (moveX, moveY) * Time.deltaTime * speed);
			
		if (moveX != 0 || moveY != 0) {
			return;
		}

		anim.enabled = false;

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			MoveTop ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			MoveDown ();
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			MoveLeft ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			MoveRight ();
		}
	}

	public void MoveTop ()
	{
		MovePlayer (KEY_Direction.KEY_DOWN_TOP);
	}

	public void MoveDown ()
	{
		MovePlayer (KEY_Direction.KEY_DOWN_DOWN);
	}

	public void MoveLeft ()
	{
		MovePlayer (KEY_Direction.KEY_DOWN_LEFT);
	}

	public void MoveRight ()
	{
		MovePlayer (KEY_Direction.KEY_DOWN_RIGHT);
	}

	public void setLog (string text)
	{
		log.text = text;
	}

	void MovePlayer (KEY_Direction movement)
	{
		if (!anim.enabled) {
			anim.enabled = true;
		}
		moveX = 0;
		moveY = 0;
		if (movement == KEY_Direction.KEY_DOWN_LEFT) {
			playAnimation = leftAnimation;
			c = level [playerY, playerX - 1];
			if (c == 's') {
				return;
			} else if (c == 'w') {
				Destroy (levelObj [playerY, playerX - 1]);
			}
			moveX = -1;
			playerX--;
			
		} else if (movement == KEY_Direction.KEY_DOWN_RIGHT) {
			playAnimation = rightAnimation;
			c = level [playerY, playerX + 1];
			if (c == 's') {
				return;
			} else if (c == 'w') {
				Destroy (levelObj [playerY, playerX + 1]);
			}
			moveX = 1;
			playerX++;
			
		} else if (movement == KEY_Direction.KEY_DOWN_TOP) {
			playAnimation = upAnimation;
			c = level [playerY + 1, playerX];
			if (c == 's') {
				return;
			} else if (c == 'w') {
				Destroy (levelObj [playerY + 1, playerX]);
			}
			moveY = 1;
			playerY++;
			
		} else if (movement == KEY_Direction.KEY_DOWN_DOWN) {
			playAnimation = downAnimation;
			c = level [playerY - 1, playerX];
			if (c == 's') {
				return;
			} else if (c == 'w') {
				Destroy (levelObj [playerY - 1, playerX]);
			}
			moveY = -1;
			playerY--;
		}

		if (moveX != 0) {
			player_transform_position.x = playerX * tileSize;
		}

		if (moveY != 0) {
			player_transform_position.y = playerY * tileSize;
		}

		anim.Play (playAnimation);
	}
		
}