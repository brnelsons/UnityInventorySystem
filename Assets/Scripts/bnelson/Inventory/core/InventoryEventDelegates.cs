using JetBrains.Annotations;

namespace bnelson.Inventory.core {
    public static class InventoryEventDelegates {
        public delegate void AddItem([NotNull] IItemStack itemStack, [CanBeNull] Extras extras);

        public class Extras {
            public int ToIndex { get; set; }
            public IItemContainer ToContainer { get; set; }
            public int FromIndex { get; set; }
            public IItemContainer FromContainer { get; set; }

            public Extras() {
                ToIndex = -1;
            }
        }
    }
}