using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid_manager : MonoBehaviour {

	private class grid_matrix {
    	private GameObject[ , ] myArray;
		private int size_;
		private int offset_;

		public void set_size(int size) {
			size_ = size;
			offset_ = size / 2;
			myArray = new GameObject[size, size];
		}

    	public GameObject this[int index, int index2] {
       		get{return myArray[index + offset_, index2 + offset_];}
			set{myArray[index + offset_, index2 + offset_] = value;}
    	}
	}

	public GameObject tile;

	GameObject create_hex(int x, int y, float size) {
		float width = size * 2.0f ;
		float horiz = width * 3.0f / 4.0f;
		float height = 28.0f;
		float vert = height / 2.0f;

		GameObject obj = Instantiate (tile);
		Vector3 new_pos = new Vector3((horiz/32.0f)*x, (vert/32.0f*2)*y+(vert/32.0f)*x);
		Debug.Log((vert/32.0f)*y);
		obj.transform.position = new_pos;
		return obj;
	}

	// Use this for initialization
	void Start () {

		grid_matrix grid = new grid_matrix();
		grid.set_size(10);

		for (int i = 0; i < 8; i++) {
			
		}

		grid[0, 0] = create_hex(0, 0, 16.0f);
		grid[1, 0] = create_hex(1, 0, 16.0f);
		grid[1, -1] = create_hex(1, -1, 16.0f);
		grid[-1, 1] = create_hex(-1, 1, 16.0f);
		grid[-1, 0] = create_hex(-1, 0, 16.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
