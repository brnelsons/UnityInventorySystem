namespace Inventory {
    public class InventoryEventManager {
        public static event InventoryEventDelegates.AddItem OnAddItem;

        public static void AddItem(IItem item) {
            if (OnAddItem != null)
            {
                OnAddItem(item);
            }
        }
    }
}