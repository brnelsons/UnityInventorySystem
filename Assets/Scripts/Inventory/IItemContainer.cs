namespace Inventory {
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
        /// Force sets the item at the index
        /// </summary>
        /// <param name="item">the item to set</param>
        /// <param name="index">the index of the container to set the object to</param>
        /// <returns>the object that was at the index before the set</returns>
        IItem Set(IItem item, int index);

        /// <summary>
        /// Attempts to set the item at the index of the container
        /// </summary>
        /// <param name="item">the item to set</param>
        /// <param name="index">the index of the container to set the object to</param>
        /// <returns>whether the item was set or not</returns>
        bool TrySet(IItem item, int index);

        /// <summary>
        /// Removes item at the index from the container and returns it
        /// and sets the containers element at that index to null
        /// </summary>
        /// <param name="index">the index to remove the item from</param>
        /// <returns>the item that was removed from the container</returns>
        IItem Remove(int index);
    }
}