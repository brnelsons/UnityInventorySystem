using JetBrains.Annotations;

namespace Inventory {
    public static class InventoryEventDelegates {
        public delegate void AddItem([NotNull] IItem item, [CanBeNull] Extras extras);

        public class Extras {
            public int Index { get; set; }
            public int OtherIndex { get; set; }
            public IItemContainer OtherContainer { get; set; }

            public Extras() {
                Index = -1;
            }
        }
    }
}