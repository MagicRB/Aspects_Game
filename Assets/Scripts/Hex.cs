using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour {

	public Vector2 position_in_grid;
	public grid_manager.grid_matrix grid;
	public grid_manager.aspect aspect;

	SpriteRenderer connect_top;
	SpriteRenderer connect_bottom;
	SpriteRenderer connect_left_bottom;
	SpriteRenderer connect_left_top;
	SpriteRenderer connect_right_bottom;
	SpriteRenderer connect_right_top;


	public void aspect_update(bool causeUpdate) {
		if (aspect.name != "NULL") {
			Sprite sp = Sprite.Create (aspect.texture, new Rect (0, 0, aspect.texture.width, aspect.texture.height), new Vector2 (0.5f, 0.5f));
			this.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().sprite = sp;
			this.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().transform.localScale = new Vector3 (2.0f, 2.0f, 2.0f);

			this.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		} else {
			this.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		}

		try {
			GameObject touch = grid [(int)position_in_grid.x, (int)position_in_grid.y + 1];
			if (touch) {
				if (touch.GetComponent<Hex> ().aspect.name == aspect.component1 || touch.GetComponent<Hex> ().aspect.name == aspect.component2 || aspect.derivatives.Contains (touch.GetComponent<Hex> ().aspect.name)) {
					connect_top.enabled = true;
				} else {
					connect_top.enabled = false;
				}
				if (causeUpdate) touch.GetComponent<Hex> ().aspect_update (false);
			}
		} catch (System.IndexOutOfRangeException e) {}

		try {
			GameObject touch = grid [(int)position_in_grid.x, (int)position_in_grid.y - 1];
			if (touch) {
				if (touch.GetComponent<Hex> ().aspect.name == aspect.component1 || touch.GetComponent<Hex> ().aspect.name == aspect.component2 || aspect.derivatives.Contains (touch.GetComponent<Hex> ().aspect.name)) {
					connect_bottom.enabled = true;
				} else {
					connect_bottom.enabled = false;
				}
				if (causeUpdate) touch.GetComponent<Hex> ().aspect_update (false);
			}
		} catch (System.IndexOutOfRangeException e) {}

		try {
			GameObject touch = grid [(int)position_in_grid.x - 1, (int)position_in_grid.y + 1];
			if (touch) {
				if (touch.GetComponent<Hex> ().aspect.name == aspect.component1 || touch.GetComponent<Hex> ().aspect.name == aspect.component2 || aspect.derivatives.Contains (touch.GetComponent<Hex> ().aspect.name)) {
					if (causeUpdate) touch.GetComponent<Hex> ().aspect_update (false);
					connect_left_top.enabled = true;
				} else {
					connect_left_top.enabled = false;
				}
				if (causeUpdate) touch.GetComponent<Hex> ().aspect_update (false);
			}
		} catch (System.IndexOutOfRangeException e) {}

		try {
			GameObject touch = grid [(int)position_in_grid.x - 1, (int)position_in_grid.y];
			if (touch) {
				if (touch.GetComponent<Hex> ().aspect.name == aspect.component1 || touch.GetComponent<Hex> ().aspect.name == aspect.component2 || aspect.derivatives.Contains (touch.GetComponent<Hex> ().aspect.name)) {
					connect_left_bottom.enabled = true;
				} else {
					connect_left_bottom.enabled = false;
				}
				if (causeUpdate) touch.GetComponent<Hex> ().aspect_update (false);
			}
		} catch (System.IndexOutOfRangeException e) {}

		try {
			GameObject touch = grid [(int)position_in_grid.x + 1, (int)position_in_grid.y];
			if (touch) {
				if (touch.GetComponent<Hex> ().aspect.name == aspect.component1 || touch.GetComponent<Hex> ().aspect.name == aspect.component2 || aspect.derivatives.Contains (touch.GetComponent<Hex> ().aspect.name)) {
					if (causeUpdate) touch.GetComponent<Hex> ().aspect_update (false);
					connect_right_top.enabled = true;
				} else {
					connect_right_top.enabled = false;
				}
				if (causeUpdate) touch.GetComponent<Hex> ().aspect_update (false);
			}
		} catch (System.IndexOutOfRangeException e) {}

		try {
			GameObject touch = grid [(int)position_in_grid.x + 1, (int)position_in_grid.y - 1];
			if (touch) {
				if (touch.GetComponent<Hex> ().aspect.name == aspect.component1 || touch.GetComponent<Hex> ().aspect.name == aspect.component2 || aspect.derivatives.Contains (touch.GetComponent<Hex> ().aspect.name)) {
					connect_right_bottom.enabled = true;
				} else {
					connect_right_bottom.enabled = false;
				}
				if (causeUpdate) touch.GetComponent<Hex> ().aspect_update (false);
			}
		} catch (System.IndexOutOfRangeException e) {}

	}

	// Use this for initialization
	void Start () {

		aspect = new grid_manager.aspect ();
		aspect.name = "NULL";
		aspect.game_object = this.gameObject;

		connect_top = this.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ();
		connect_bottom = this.transform.GetChild (2).gameObject.GetComponent<SpriteRenderer> ();
		connect_left_bottom = this.transform.GetChild (3).gameObject.GetComponent<SpriteRenderer> ();
		connect_left_top = this.transform.GetChild (4).gameObject.GetComponent<SpriteRenderer> ();
		connect_right_bottom = this.transform.GetChild (5).gameObject.GetComponent<SpriteRenderer> ();
		connect_right_top = this.transform.GetChild (6).gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
