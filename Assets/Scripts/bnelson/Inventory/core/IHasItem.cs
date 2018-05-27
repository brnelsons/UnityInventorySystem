using JetBrains.Annotations;

namespace bnelson.Inventory.core {
    /// <summary>
    /// Basic interface for and object that can hold an IItem
    /// </summary>
    public interface IHasItem {
        [CanBeNull]
        IItem GetItem();

        void SetItem([CanBeNull] IItem item);
    }
}