using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory {
    public class InventoryView : MonoBehaviour, IItemContainer {
        public int Size = 28;
        public InventorySlotView InventorySlotViewPrefab;

        private InventorySlotView[] _slots;
        private InventoryDatabase _items;
        
        private void Start() {
            _slots = InitWithSize(Size);
        }

        private void OnEnable() {
            InventoryEventManager.OnAddItem += InventoryEventManagerOnOnAddItem;
        }

        private void OnDisable() {
            InventoryEventManager.OnAddItem -= InventoryEventManagerOnOnAddItem;
        }

        public bool Add(IItem item) {
            //TODO i dont need this right now.
            return false;
        }

        public void Remove(IItem item, int index) {
            if (_items.GetItem(index) == item)
            {
                SetItemAt(null, index, true);
            }
        }

        private InventorySlotView[] InitWithSize(int size) {
            var slots = new InventorySlotView[size];
            _items = new InventoryDatabase(size);
            var slotContainer = GameObject.Find("SlotContainer");
            for (var i = 0; i < size; i++)
            {
                slots[i] = Instantiate(InventorySlotViewPrefab, slotContainer.transform, false);
                GetItemHolder(slots, i).Index = i;
            }

            return slots;
        }

        private static InventoryItemHolder GetItemHolder(InventorySlotView[] slots, int i) {
            return slots[i].gameObject.GetComponentInChildren<InventoryItemHolder>();
        }

        private void InventoryEventManagerOnOnAddItem(IItem item, InventoryEventDelegates.Extras extras) {
            if (extras != null && extras.Index != -1)
            {
                if (extras.Index < Size)
                {
                    if (SetItemAt(item, extras.Index, false))
                    {
                        if (extras.OtherContainer != null)
                        {
                            extras.OtherContainer.Remove(item, extras.OtherIndex);
                        }
                        return;
                    }
                    else if(extras.OtherContainer == this)
                    {
                        //switch the items
                        
                    }
                }
            }
            for (var i = 0; i < Size; i++)
            {
                if (SetItemAt(item, i, false))
                {
                    if (extras != null && extras.OtherContainer != null)
                    {
                        extras.OtherContainer.Remove(item, extras.OtherIndex);
                    }
                    return;
                }
            }
            Debug.Log("Did not actually add the item, something is wrong!");
        }

        private bool SetItemAt(IItem item, int i, bool force) {
            var inventoryItemHolder = _slots[i].gameObject.GetComponentInChildren<InventoryItemHolder>();
            if (inventoryItemHolder == null) return false;
            if (inventoryItemHolder.GetItem() != null && !force) return false;
            Debug.Log("Added Item");
            inventoryItemHolder.SetItem(item);
            _items.SetItem(item, i);
            return true;

        }
    }
}