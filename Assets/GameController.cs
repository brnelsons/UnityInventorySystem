using Inventory;
using UnityEngine;

public class GameController : MonoBehaviour {
	private static InventoryEventDelegates.Extras Extras = new InventoryEventDelegates.Extras();
	
	public InventoryItem TestItemPrefab;
	public InventoryItem TestItemPrefab2;
	
	private bool _other;
	
	// Update is called once per frame
	private void OnGUI() {
		if (TestItemPrefab != null && GUI.Button(new Rect(200, 200, 200, 200), "Click to add"))
		{
			if (_other)
			{
				InventoryEventManager.AddItem(Instantiate(TestItemPrefab, transform, false), Extras);
			}
			else
			{
				InventoryEventManager.AddItem(Instantiate(TestItemPrefab2, transform, false), Extras);
			}

			_other = !_other;
		}
	}
}
