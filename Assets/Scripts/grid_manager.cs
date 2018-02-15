using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid_manager : MonoBehaviour {

	public GameObject tile;

	// Use this for initialization
	void Start () {
		float size = 16.0f;
		float width = size * 2.0f ;
		float horiz = width * 3.0f / 4.0f;
		float height = 28.0f;
		float vert = height / 2.0f;

		GameObject[ , , ] grid = new GameObject[10, 10, 10];

		Debug.Log (height);

		int x = 1;
		int y = 0;
		int z = -1;

		grid[0+5, 0+5, 0+5] = Instantiate (tile);
		grid[x+5, y+5, z+5] = Instantiate (tile);
		Vector3 new_pos = new Vector3((horiz/32.0f)*x, vert/32.0f*y);
		grid[x+5, y+5, z+5].transform.position = new_pos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
