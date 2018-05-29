using bnelson.Inventory.core;
using UnityEngine.Networking;

namespace bnelson.Inventory.UnityComponents {
    public class Container : NetworkBehaviour, IItemContainer {
        public int Size = 8;
        public ItemSlotView ItemSlotViewPrefab;
        private IItemContainer _primaryInventory;

        void Start() {
            _primaryInventory = new ItemContainer<ItemSlotView>(Size, SlotInitializer);
        }

        private ItemSlotView SlotInitializer(int index) {
            var slot = Instantiate(ItemSlotViewPrefab, transform, false);
            slot.Index = index;
            return slot;
        }

        public string Name { get; set; }

        public bool Add(IItemStack itemStackToAdd) {
            return _primaryInventory.Add(itemStackToAdd);
        }

        public void Update() {
            _primaryInventory.Update();
        }

        public int GetSize() {
            return Size;
        }

        public void SetSize(int size) {
            Size = size;
            _primaryInventory.SetSize(size);
        }

        public IHasItemStack GetHasItemStack(int index) {
            return _primaryInventory.GetHasItemStack(index);
        }
    }
}