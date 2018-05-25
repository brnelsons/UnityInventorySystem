namespace Inventory {
    public interface IItemContainer {
        bool Add(IItem item);
        void Remove(IItem item, int index);
    }
}