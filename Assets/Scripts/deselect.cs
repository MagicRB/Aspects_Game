using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class deselect : MonoBehaviour, IPointerClickHandler {

	public GameObject aspect_scroll;

	public void OnPointerClick (PointerEventData eventData) {
		aspect_scroll.GetComponent<AspectScroll> ().selected_aspect = "NULL";
		aspect_scroll.GetComponent<AspectScroll> ().change_selected ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
