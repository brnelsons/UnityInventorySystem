using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;

namespace bnelson.Inventory.core {
    public interface IItemStack {
        bool Add(IItem item);
        bool Merge([CanBeNull] IItemStack itemStack);
        IItem RemoveOne();
        IList<IItem> RemoveAll();
        ReadOnlyCollection<IItem> GetItems();
        string GetItemName();
        int GetCount();
        void Pickup();
        void Drop();
    }
}