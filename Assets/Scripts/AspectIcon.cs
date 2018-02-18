using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AspectIcon : MonoBehaviour, IPointerClickHandler {
	public GameObject scrollView;
	public grid_manager.aspect aspect;

	public void OnPointerClick (PointerEventData eventData) {
		scrollView.GetComponent<AspectScroll> ().selected_aspect = aspect.name;
		scrollView.GetComponent<AspectScroll> ().change_selected ();
	}

	public void update_aspect_icon () {
		Sprite sp = Sprite.Create (aspect.texture, new Rect (0, 0, aspect.texture.width, aspect.texture.height), new Vector2 (0.5f, 0.5f));
		this.gameObject.GetComponent<Image> ().sprite =  sp;
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
