using Inventory;
using UnityEngine;

public class GameController : MonoBehaviour {

	public InventoryItem TestItemPrefab;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void OnGUI() {
		if (TestItemPrefab != null && GUI.Button(new Rect(200, 200, 200, 200), "Click to add"))
		{
			InventoryEventManager.AddItem(Instantiate(TestItemPrefab, transform, false), null);
		}
	}
}
