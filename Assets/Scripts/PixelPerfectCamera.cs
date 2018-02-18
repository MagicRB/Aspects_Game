using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {
	public int pixelsPerUnit;
	public int unitsOnScreen;
	public GameObject aspect_content;

	private int lastWidth;
	private int lastHeight;
	private int currentWidth;
	private int currentHeight;

	Camera camera;

	int GetNearestMultiple(int value, int multiple)
	{
		int rem = value % multiple;
		int result = value - rem;
		if (rem > (multiple / 2))
			result += multiple;

		return result;
	}

	// Use this for initialization
	void Start () {
		calculate_display ();
	}

	void calculate_display() {

		camera = gameObject.GetComponent<Camera>();

		if (Screen.width > Screen.height) {

			int tempUnitSize = Screen.width / unitsOnScreen;

			float finalUnitSize = GetNearestMultiple(tempUnitSize, pixelsPerUnit);

			camera.orthographicSize = Screen.height / (finalUnitSize * 2.0f);

			aspect_content.GetComponent<RectTransform> ().position = new Vector3(0, 40);
			aspect_content.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 80);
			aspect_content.GetComponent<GridLayoutGroup> ().cellSize = new Vector3 (80, 80);
		} else if (Screen.height > Screen.width) {

			aspect_content.GetComponent<RectTransform> ().position = new Vector3(0, 80);
			aspect_content.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 160);
			aspect_content.GetComponent<GridLayoutGroup> ().cellSize = new Vector3 (160, 160);


			int tempUnitSize = Screen.height / unitsOnScreen;

			float finalUnitSize = GetNearestMultiple(tempUnitSize, pixelsPerUnit);

			camera.orthographicSize = Screen.height / (finalUnitSize * 2.0f);

		}

	
	}
	
	// Update is called once per frame
	void Update () {
		currentWidth = Screen.width;
		currentHeight = Screen.height;

		if (lastWidth != currentWidth || lastHeight != currentHeight) {
			calculate_display ();
		}
		lastWidth = currentWidth;
		lastHeight = currentHeight;
	}
}
