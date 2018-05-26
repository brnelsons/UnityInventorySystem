using JetBrains.Annotations;
using UnityEngine;

namespace Inventory {
    public class InventoryView : MonoBehaviour, IItemContainer {
        public int Size = 28;
        public InventorySlotView InventorySlotViewPrefab;

        private InventorySlotView[] _slots;

        private void Start() {
            SetSize(Size);
        }

        private void OnEnable() {
            InventoryEventManager.OnAddItem += InventoryEventManagerOnOnAddItem;
        }

        private void OnDisable() {
            InventoryEventManager.OnAddItem -= InventoryEventManagerOnOnAddItem;
        }

        private void InventoryEventManagerOnOnAddItem([CanBeNull] IItem item,
                                                      [NotNull] InventoryEventDelegates.Extras extras) {
            InventoryUtils.AddOrSwitchItems(this, item, extras);
        }

        public int GetSize() {
            return Size;
        }

        public void SetSize(int size) {
            var slotContainer = GameObject.Find("SlotContainer");
            var oldSlots = _slots;
            _slots = new InventorySlotView[size];
            for (var i = 0; i < size; i++)
            {
                _slots[i] = Instantiate(InventorySlotViewPrefab, slotContainer.transform, false);
                _slots[i].GetInventoryItemHolder().Index = i;
            }

            Size = size;
            InventoryUtils.Clone(oldSlots, _slots);
        }

        public bool Add(IItem item) {
            for (var i = 0; i < Size; i++)
            {
                if (TrySet(item, i)) return true;
            }

            return false;
        }

        public IItem Set(IItem item, int index) {
            var oldItem = _slots[index].GetItem();
            _slots[index].SetItem(item);
            return oldItem;
        }

        public bool TrySet(IItem item, int index) {
            if (_slots[index].GetItem() != null) return false;
            _slots[index].SetItem(item);
            return true;
        }

        public IItem Remove(int index) {
            var remove = _slots[index].GetItem();
            _slots[index].SetItem(null);
            return remove;
        }
    }
}