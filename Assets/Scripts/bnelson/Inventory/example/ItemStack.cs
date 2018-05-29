using System.Collections.Generic;
using System.Collections.ObjectModel;
using bnelson.Inventory.core;
using UnityEngine;

namespace bnelson.Inventory.example {
    public class ItemStack : IItemStack {
        private List<IItem> _items;
        private string _itemName;

        public ItemStack() {
            _items = new List<IItem>();
        }

        private ItemStack(params IItem[] items) {
            _items = new List<IItem>(items);
            _itemName = items[0].GetName();
        }

        public bool Add(IItem item) {
            if (item == null) return false;
            var itemsCount = _items.Count;
            if (itemsCount > 0 && !item.GetIsStackable()) return false;
            if (itemsCount > 0 && _itemName != item.GetName()) return false;
            if (itemsCount >= item.GetMaxStackCount()) return false;
            _items.Add(item);
            _itemName = item.GetName();
            return true;
        }

        public bool Merge(IItemStack itemStack) {
            if (itemStack == null) return false;
            if (_items.Count > 0
                && _itemName != itemStack.GetItemName()) return false;
            var count = itemStack.GetCount();
            var wasMerged = false;
            for (var i = 0; i < count; i++)
            {
                var removed = itemStack.RemoveOne();
                if (Add(removed))
                {
                    wasMerged = true;
                }
                else
                {
                    //add it back to the other stack
                    itemStack.Add(removed);
                    break;
                }
            }

            return wasMerged;
        }

        public IItem RemoveOne() {
            var temp = _items[_items.Count - 1];
            //TODO investigate race condition here
            _items.Remove(temp);
            if (_items.Count == 0)
            {
                _itemName = null;
            }
            return temp;
        }

        public IList<IItem> RemoveAll() {
            var temp = _items;
            _items = new List<IItem>();
            _itemName = null;
            return temp;
        }

        public ReadOnlyCollection<IItem> GetItems() {
            return _items.AsReadOnly();
        }

        public string GetItemName() {
            return _itemName;
        }

        public int GetCount() {
            return _items.Count;
        }

        public void Pickup() {
            //TODO 
            Debug.Log("picked up itemstack");
        }

        public void Drop() {
            //TODO 
            Debug.Log("dropped itemstack");
        }

        /// <summary>
        /// Factory method for creation of Itemstack
        /// </summary>
        /// <param name="items">item array to initialize with</param>
        /// <returns>new item stack of all the items</returns>
        public static ItemStack Of(params IItem[] items) {
            return new ItemStack(items);
        }
    }
}