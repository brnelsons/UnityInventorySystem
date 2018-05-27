using JetBrains.Annotations;

namespace bnelson.Inventory.core {
    public static class InventoryEventManager {
        public static event InventoryEventDelegates.AddItem OnAddItem;

        public static void AddItem([NotNull] IItemStack itemStack,
                                   [NotNull] InventoryEventDelegates.Extras extras) {
            if (OnAddItem != null)
            {
                OnAddItem(itemStack, extras);
            }
        }
    }
}