using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using bnelson.Inventory.core;

namespace bnelson.Inventory.example {
    public class ItemStack : IItemStack {

        private List<IItem> _items;
        private Type _itemType;

        public ItemStack() {
            _items = new List<IItem>();
        }


        private ItemStack(params IItem[] items) {
            _items = new List<IItem>(items);
        }

        public bool Add(IItem item) {
            if (item == null) return false;
            if (_items.Count != 0 && _itemType != item.GetType()) return false;
            if (_items.Count > 0 && !item.GetIsStackable()) return false;
            _itemType = item.GetType();
            _items.Add(item);
            return true;

        }

        public bool Add(IItemStack other) {
            for (var i = 0;i<other.GetCount();i++)
            {
                var item = other.RemoveOne();
                if (!Add(item))
                {
                    other.Add(item);
                    return false;
                }
            }

            return true;
        }

        public IItem RemoveOne() {
            var temp = _items[_items.Count - 1];
            //TODO investigate race condition here
            _items.Remove(temp);
            return temp;
        }

        public IList<IItem> RemoveAll() {
            var temp = _items;
            _items = new List<IItem>();
            return temp;
        }

        public ReadOnlyCollection<IItem> GetItems() {
            return _items.AsReadOnly();
        }

        public int GetCount() {
            return _items.Count;
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