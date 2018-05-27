using bnelson.Inventory.core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace bnelson.Inventory.UnityComponents {
    public class InventoryWindow : MonoBehaviour, IItemContainer {
        public int Size = 8;
        public ItemSlotView ItemSlotViewPrefab;
        public bool IsPlayerPrimaryInventory;
        private IItemContainer _delegateContainer;

        void Start() {
            _delegateContainer = new ItemContainer<ItemSlotView>(Size, HasItemInitializer);
        }

        private ItemSlotView HasItemInitializer(int index) {
            var slot = Instantiate(ItemSlotViewPrefab, transform, false);
            slot.GetInventoryItemHolder().Index = index;
            return slot;
        }

        private void OnEnable() {
            InventoryEventManager.OnAddItem += InventoryEventManagerOnOnAddItem;
        }

        private void OnDisable() {
            InventoryEventManager.OnAddItem -= InventoryEventManagerOnOnAddItem;
        }

        private void InventoryEventManagerOnOnAddItem([CanBeNull] IItemStack itemStack,
                                                      [NotNull] InventoryEventDelegates.Extras extras) {
            //if this is an Add directly to this container 
            //or its not coming from another container and this is the primary inventory
            if ((InventoryWindow) extras.ToContainer != this &&
                (extras.ToContainer != null || !IsPlayerPrimaryInventory)) return;
            if (extras.ToIndex == -1)
            {
                _delegateContainer.Add(itemStack);
            }
            else
            {
                _delegateContainer.MergeOrSwap(extras.FromContainer, extras.ToIndex);
            }
        }

        public int GetSize() {
            return Size;
        }

        public void SetSize(int size) {
            Size = size;
            _delegateContainer.SetSize(size);
        }

        public bool Add(IItem item) {
            return _delegateContainer.Add(item);
        }

        public IHasItemStack Get(int index) {
            return _delegateContainer.Get(index);
        }

        public bool Add(IItemStack item) {
            return _delegateContainer.Add(item);
        }

        public void MergeOrSwap(IItemContainer itemContainer, int index) {
            _delegateContainer.MergeOrSwap(itemContainer, index);
        }
    }
}