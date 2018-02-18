using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AspectScroll : MonoBehaviour {

	public string selected_aspect;
	public GameObject selected_display;
	public Dictionary<string, grid_manager.aspect> aspects;
	public Texture2D cross;

	public void change_selected() {
		if (selected_aspect == "NULL") {
			selected_display.GetComponent<Image> ().sprite = Sprite.Create (cross, new Rect (0, 0, cross.width, cross.height), new Vector2 (0.5f, 0.5f));
		} else {
			selected_display.GetComponent<Image> ().sprite = Sprite.Create (aspects [selected_aspect].texture, new Rect (0, 0, aspects [selected_aspect].texture.width, aspects [selected_aspect].texture.height), new Vector2 (0.5f, 0.5f));
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
