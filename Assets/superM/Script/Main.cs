using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
	public GameObject logGB;

	public GameObject player;

	private float speed = 0.5f;
	public bool isMove = false;

	public GameObject stonePrefab;
	public GameObject woodPrefab;
	public GameObject[] bgTile;

	private static Text log;
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

	static Main instance;

	public static Main Instance {
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
			
		SpriteRenderer spriteRenderer = stonePrefab.GetComponent<SpriteRenderer> ();
		tileSize = 0.7f;//spriteRenderer.bounds.size.x;

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

	void Update ()
	{
		if (isMove) {
			return;
		}
		if (Input.GetKey (KeyCode.UpArrow) && !Input.GetKeyUp (KeyCode.UpArrow)) {
			MoveTop ();
		} else if (Input.GetKey (KeyCode.DownArrow) && !Input.GetKeyUp (KeyCode.DownArrow)) {
			MoveDown ();
		} else if (Input.GetKey (KeyCode.LeftArrow) && !Input.GetKeyUp (KeyCode.LeftArrow)) {
			MoveLeft ();
		} else if (Input.GetKey (KeyCode.RightArrow) && !Input.GetKeyUp (KeyCode.RightArrow)) {
			MoveRight ();
		}
	}

	public void Joystick (Vector2 axis)
	{
		if (axis.y > 0 && axis.y > axis.x) {
			MoveTop ();
		} else if (axis.y < 0 && axis.y < axis.x) {
			MoveDown ();
		} else if (axis.x < 0 && axis.x < axis.y) {
			MoveLeft ();
		} else if (axis.x > 0 && axis.x > axis.y) {
			MoveRight ();
		}
	}

	public void MoveTop ()
	{
		if (isMove) {
			return;
		}
		char c = level [playerY + 1, playerX];
		if (c == 's' || c == 'w') {
			return;
			//Destroy (levelObj [playerY, playerX - 1]);
		}
		playerY++;
		isMove = true;
		player.transform.DOMoveY (player.transform.position.y + tileSize, speed).SetEase (Ease.Linear).OnComplete (new TweenCallback (delegate() {
			isMove = false;
		}));
	}

	public void MoveDown ()
	{
		if (isMove) {
			return;
		}
		char c = level [playerY - 1, playerX];
		if (c == 's' || c == 'w') {
			return;
			//Destroy (levelObj [playerY, playerX - 1]);
		}
		playerY--;
		isMove = true;
		player.transform.DOMoveY (player.transform.position.y - tileSize, speed).SetEase (Ease.Linear).OnComplete (new TweenCallback (delegate() {
			isMove = false;
		}));
	}

	public void MoveLeft ()
	{
		if (isMove) {
			return;
		}
		char c = level [playerY, playerX - 1];
		if (c == 's' || c == 'w') {
			return;
			//Destroy (levelObj [playerY, playerX - 1]);
		}
		playerX--;
		isMove = true;
		player.transform.DOMoveX (player.transform.position.x - tileSize, speed).SetEase (Ease.Linear).OnComplete (new TweenCallback (delegate() {
			isMove = false;
		}));
	}

	public void MoveRight ()
	{
		if (isMove) {
			return;
		}
		char c = level [playerY, playerX + 1];
		if (c == 's' || c == 'w') {
			return;
			//Destroy (levelObj [playerY, playerX - 1]);
		}
		playerX++;
		isMove = true;
		player.transform.DOMoveX (player.transform.position.x + tileSize, speed).SetEase (Ease.Linear).OnComplete (new TweenCallback (delegate() {
			isMove = false;
		}));
	}

	public void setLog (string text)
	{
		log.text = text;
	}
}