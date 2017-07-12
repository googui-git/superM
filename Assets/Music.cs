using UnityEngine;
using System.Collections;
using UnityEngine.UI;using System.Collections.Generic;

public class Music : MonoBehaviour
{
	static Music instance;

	public Image image;
	public GameObject canvas;
	public GameObject music;

	private List<Transform> childs = new List<Transform> ();
	private List<string> childs_name = new List<string> ();
	private int t_x = 12;
	private int t_y = 6;

	public static Music Instance {
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
		foreach (Transform child in music.transform)  
		{  
			childs.Add(child);
			childs_name.Add (child.name);
		}

		childs_name.Sort ();

		int i = 0;
		for (int y = 0; y < t_y; y++) {
			for (int x = 0; x < t_x; x++) {
				Image stoneGO = Instantiate (image) as Image;
				stoneGO.transform.parent = canvas.transform;
				stoneGO.name = childs_name[i];
				Text text = stoneGO.GetComponentInChildren<Text>();  
				text.text = childs_name[i]; 
				i++;
				stoneGO.transform.position = new Vector2 ((x-t_x/2+0.5f) * 64f+canvas.transform.position.x, (y-t_y/2+0.5f) * 64f+canvas.transform.position.y);
				stoneGO.gameObject.SetActive (true);
			}
		}
	}

	public void play(string name){
		music.transform.FindChild(name).GetComponent<AudioSource>().Play();
	}

	void Update ()
	{


	}

}