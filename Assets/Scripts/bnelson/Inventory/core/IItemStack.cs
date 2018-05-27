using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace bnelson.Inventory.core {
    public interface IItemStack {
        bool Add(IItem item);
        bool Add(IItemStack item);
        IItem RemoveOne();
        IList<IItem> RemoveAll();
        ReadOnlyCollection<IItem> GetItems();
        int GetCount();
    }
}