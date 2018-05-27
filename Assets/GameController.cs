using bnelson.Inventory.core;
using bnelson.Inventory.example;
using UnityEngine;

public class GameController : MonoBehaviour {
    private static readonly InventoryEventDelegates.Extras Extras = new InventoryEventDelegates.Extras();

    public Item TestItemPrefab;
    public Item TestItemPrefab2;

    private bool _other;

    // Update is called once per frame
    private void OnGUI() {
        if (TestItemPrefab != null && GUI.Button(new Rect(200, 200, 200, 200), "Click to add"))
        {
            if (_other)
            {
                InventoryEventManager.AddItem(ItemStack.Of(Instantiate(TestItemPrefab, transform, false)), Extras);
            }
            else
            {
                InventoryEventManager.AddItem(ItemStack.Of(Instantiate(TestItemPrefab2, transform, false)), Extras);
            }

            _other = !_other;
        }
    }
}