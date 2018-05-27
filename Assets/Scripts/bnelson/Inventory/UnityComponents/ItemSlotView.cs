using bnelson.Inventory.core;
using JetBrains.Annotations;
using UnityEngine;

namespace bnelson.Inventory.UnityComponents {
    public class ItemSlotView : MonoBehaviour, IHasItemStack {
        public IItemStack GetItemStack() {
            return GetInventoryItemHolder().GetItemStack();
        }

        public void SetItemStack(IItemStack itemStack) {
            GetInventoryItemHolder().SetItemStack(itemStack);
        }

        [NotNull]
        public ItemView GetInventoryItemHolder() {
            return GetComponentInChildren<ItemView>();
        }
    }
}