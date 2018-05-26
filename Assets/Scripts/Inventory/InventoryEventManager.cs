using JetBrains.Annotations;

namespace Inventory {
    public class InventoryEventManager {
        public static event InventoryEventDelegates.AddItem OnAddItem;

        public static void AddItem([NotNull] IItem item, 
                                   [NotNull] InventoryEventDelegates.Extras extras) {
            if (OnAddItem != null)
            {
                OnAddItem(item, extras);
            }
        }
    }
}