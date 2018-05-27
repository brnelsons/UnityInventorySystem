namespace bnelson.Inventory.core {
    /// <summary>
    /// This is a basic item container.
    /// Holds items and allows for basic management of items
    /// </summary>
    public interface IItemContainer {
        /// <summary>
        /// Get the size of the container
        /// </summary>
        /// <returns>container size</returns>
        int GetSize();

        /// <summary>
        /// Calls for a resize of the container to the new size
        /// Should drop any items that are outsize the bounds of the new container size
        /// </summary>
        /// <param name="size">the new container size</param>
        void SetSize(int size);

        /// <summary>
        /// Attempts to add the item
        /// </summary>
        /// <param name="item">the item to add</param>
        /// <returns>whether the item was added</returns>
        bool Add(IItem item);

        /// <summary>
        /// Attempts to add the item
        /// </summary>
        /// <param name="item">the item to add</param>
        /// <returns>whether the item was added</returns>
        bool Add(IItemStack item);

        IHasItemStack Get(int index);
        
        void MergeOrSwap(IItemContainer itemContainer, int index);
    }
}