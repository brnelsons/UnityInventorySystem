﻿namespace bnelson.Inventory.core {
    public static class InventoryUtils {
        /// <summary>
        /// Clones all the Items from the "from" array of IHasItem to the "to" array of IHasItem 
        /// </summary>
        /// <param name="from">the array to clone the items from</param>
        /// <param name="to">the array to clone the items to</param>
        public static void Clone<T>(T[] from, T[] to) where T : IHasItem {
            if (from == null || to == null) return;
            //copy over all the old slots items into the new slots
            for (var i = 0; i < from.Length; i++)
            {
                if (i < to.Length && to[i] != null && from[i] != null)
                {
                    to[i].SetItem(from[i].GetItem());
                }
                else
                {
                    //TODO drop items here 
                    //call to Invenotry event manager and drop items.
                    //they should fall to the ground near the player
                }
            }
        }

        /// <summary>
        /// Clones all the ItemStacks from the "from" array of IHasItemStack to the "to" array of IHasItemStack 
        /// </summary>
        /// <param name="from">the array to clone the items from</param>
        /// <param name="to">the array to clone the items to</param>
        public static void CloneStack<T>(T[] from, T[] to) where T : IHasItemStack {
            if (from == null || to == null) return;
            //copy over all the old slots items into the new slots
            for (var i = 0; i < from.Length; i++)
            {
                if (i < to.Length && to[i] != null && from[i] != null)
                {
                    to[i].SetItemStack(from[i].GetItemStack());
                }
                else
                {
                    //TODO drop stack here 
                    //call to Invenotry event manager and drop items.
                    //they should fall to the ground near the player
                }
            }
        }
    }
}