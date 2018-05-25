using System;
using UnityEngine;

namespace Inventory {
    public class InventoryView : MonoBehaviour {
        public int Size = 28;
        public InventorySlotView InventorySlotViewPrefab;

        private InventorySlotView[] _slots;

        private void Start() {
            _slots = InitWithSize(Size);
        }

        private void OnEnable() {
            InventoryEventManager.OnAddItem += InventoryEventManagerOnOnAddItem;
        }

        private void OnDisable() {
            InventoryEventManager.OnAddItem -= InventoryEventManagerOnOnAddItem;
        }

        private void InventoryEventManagerOnOnAddItem(IItem item) {
            for (int i = 0; i < Size; i++)
            {
                var inventoryItemHolder = _slots[i].gameObject.GetComponentInChildren<InventoryItemHolder>();
                if (inventoryItemHolder != null)
                {
                    if (inventoryItemHolder.GetItem() == null)
                    {
                        Debug.Log("Added Item");
                        inventoryItemHolder.SetItem(item);
                        return;
                    }
                }
            }
        }

        private InventorySlotView[] InitWithSize(int size) {
            var slots = new InventorySlotView[size];
            var slotContainer = GameObject.Find("SlotContainer");
            for (var i = 0; i < size; i++)
            {
                slots[i] = Instantiate(InventorySlotViewPrefab, slotContainer.transform, false);
            }

            return slots;
        }
    }
}