using bnelson.Inventory.core;
using bnelson.Inventory.example;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Item TestItemPrefab;
    public Item TestItemPrefab2;

    private bool _other;

    // Update is called once per frame
    private void OnGUI() {
        if (TestItemPrefab == null || !GUI.Button(new Rect(200, 200, 50, 100), "Click to add to inventory")) return;
        var instantiate = Instantiate(_other ? TestItemPrefab : TestItemPrefab2, transform, false);

        var itemStack = ItemStack.Of(instantiate);
        InventoryEventManager.PickupItem(itemStack);

        _other = !_other;
    }
}