using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class grid_manager : MonoBehaviour {

	public GameObject tile_prefab;
	public GameObject aspect_icon_prefab;
	public TextAsset aspect_list;
	public GameObject aspect_scroll_content;
	public GameObject aspect_scroll;


	public class grid_matrix {
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

	public class aspect {
		public string name;
		public bool primal;
		public string keywords;
		public string component1;
		public string component2;
		public GameObject game_object;
		public List<string> derivatives = new List<string> ();
		public Texture2D texture;
	}


	private Dictionary<string, aspect> aspect_dictionary = new Dictionary<string, aspect>();
	List<string> aspect_option_list = new List<string> ();
	grid_matrix grid = new grid_matrix();


	GameObject create_hex(int x, int y, float size) {
		float width = size * 2.0f ;
		float horiz = width * 3.0f / 4.0f;
		float height = 28.0f;
		float vert = height / 2.0f;

		GameObject obj = Instantiate (tile_prefab);
		Vector3 new_pos = new Vector3((horiz/32.0f)*x, (vert/32.0f*2)*y+(vert/32.0f)*x);
		obj.transform.position = new_pos;
		Hex h = obj.GetComponent (typeof(Hex)) as Hex;
		h.position_in_grid.x = x;
		h.position_in_grid.y = y;
		h.grid = grid;
		return obj.gameObject;
	}

	void draw_hex_from_hexes(int vertical_radius, ref grid_matrix grid) {
		for (int x = -vertical_radius; x <= vertical_radius; x++) {
			if (x < 0) {
				for (int y = -vertical_radius - x; y <= vertical_radius; y++) {
					grid [x, y] = create_hex (x, y, 16.0f);
				}
			} else if (x > 0) {
				for (int y = -vertical_radius; y <= vertical_radius - x; y++) {
					grid [x, y] = create_hex (x, y, 16.0f);
				}
			} else {
				for (int y = -vertical_radius; y <= vertical_radius; y++) {
					grid [x, y] = create_hex (x, y, 16.0f);
				}
			}
		}
	}

	void build_reverse_recipe_table() {
		foreach (string asp in aspect_dictionary.Keys) {
			Debug.Log (asp);
			if (aspect_dictionary [asp].primal == false) {
				aspect_dictionary [aspect_dictionary [asp].component1].derivatives.Add (asp);
				aspect_dictionary [aspect_dictionary [asp].component2].derivatives.Add (asp);
			}
		}
	}

	void populate_scrollable_aspect_list() {
		foreach (aspect asp in aspect_dictionary.Values) {
			GameObject g = Instantiate (aspect_icon_prefab);
			g.transform.SetParent(aspect_scroll_content.transform);
			g.GetComponent<AspectIcon> ().aspect = asp;
			g.GetComponent<AspectIcon> ().update_aspect_icon ();
			g.GetComponent<AspectIcon> ().scrollView = aspect_scroll;
		}
	}

	// Use this for initialization
	void Start () {
		
		grid.set_size(9);

		draw_hex_from_hexes (4, ref grid);


		XmlDocument aspect_list_doc = new XmlDocument ();
		aspect_list_doc.LoadXml (aspect_list.text);
		XmlNodeList aspects_list = aspect_list_doc.GetElementsByTagName ("aspect");

		foreach (XmlNode aspect in aspects_list) {
			XmlNodeList aspect_data = aspect.ChildNodes;
			string name = "NULL";
			aspect asp = new aspect ();
			foreach (XmlNode aspect_child in aspect_data) {
				if (aspect_child.Name == "keywords") {
					asp.keywords = aspect_child.InnerText;
				} else if (aspect_child.Name == "isPrimal") {
					if (aspect_child.InnerText == "true") {
						asp.primal = true;
					} else {
						asp.primal = false;
					}
				} else if (aspect_child.Name == "name") {
					name = aspect_child.InnerText;

					aspect_option_list.Add (name);

					asp.name = name;

					Texture2D tex = null;
					byte[] fileData;

					string file_name = Application.persistentDataPath + "/Aspect_Textures/" + name.ToLower () + ".png";

					if (File.Exists(file_name))     {
						fileData = File.ReadAllBytes(file_name);
						asp.texture = new Texture2D(32, 32);
						asp.texture.LoadImage(fileData); //..this will auto-resize the texture dimensions.
						asp.texture.filterMode = FilterMode.Point;
					} else {
						Debug.Log("Failed to load texture for: " + name + ", at path: " + file_name);
					}
				} else if (aspect_child.Name == "component1") {
					asp.component1 = aspect_child.InnerText;
				} else if (aspect_child.Name == "component2") {
					asp.component2 = aspect_child.InnerText;
				}
			}
			aspect_dictionary.Add (name, asp);
		}

		aspect_scroll.GetComponent<AspectScroll> ().aspects = aspect_dictionary;

		build_reverse_recipe_table ();

		populate_scrollable_aspect_list ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit;
			if (hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f)) {
				if (hit.collider.tag == "Hex") {

					Hex h = hit.collider.gameObject.transform.GetComponent (typeof(Hex)) as Hex;
					aspect aspc;
					if (aspect_scroll.GetComponent<AspectScroll> ().selected_aspect == "NULL") {
						aspc = new aspect ();
						aspc.name = "NULL";
					} else {
						aspc = aspect_dictionary [aspect_scroll.GetComponent<AspectScroll> ().selected_aspect];
					}
					h.aspect = aspc;
					h.aspect_update (true);
				}
			}
		} if (Input.GetMouseButtonDown (1)) {
			RaycastHit2D hit;
			if (hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f)) {
				if (hit.collider.tag == "Hex") {

					Hex h = hit.collider.gameObject.transform.GetComponent (typeof(Hex)) as Hex;

					aspect aspc;
					aspc = new aspect ();
					aspc.name = "NULL";
					h.aspect = aspc;
					h.aspect_update (true);
				}
			}
		}
	}
}
