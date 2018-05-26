using JetBrains.Annotations;
using UnityEngine;

namespace Inventory {
    public class InventorySlotView : MonoBehaviour, IHasItem {
        private IItem _item;

        public IItem GetItem() {
            return _item;
        }

        public void SetItem(IItem item) {
            _item = item;
            GetInventoryItemHolder().SetItem(item);
        }

        [NotNull]
        public InventoryItemHolder GetInventoryItemHolder() {
            return GetComponentInChildren<InventoryItemHolder>();
        }
    }
}