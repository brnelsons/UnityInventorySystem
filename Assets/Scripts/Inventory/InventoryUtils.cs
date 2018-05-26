using JetBrains.Annotations;

namespace Inventory {
    public static class InventoryUtils {
        
        /// <summary>
        /// TODO Documentation
        /// </summary>
        /// <param name="thisContainer"></param>
        /// <param name="item"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        public static bool AddOrSwitchItems([NotNull] IItemContainer thisContainer,
                                            [CanBeNull] IItem item,
                                            [NotNull] InventoryEventDelegates.Extras extras) {
            var index = extras.Index;
            var size = thisContainer.GetSize();
            if (index != -1)
            {
                if (index < thisContainer.GetSize())
                {
                    //switch the items
                    var tempItem = thisContainer.Set(item, index);
                    extras.OtherContainer.Set(tempItem, extras.OtherIndex);
                    return true;
                }
            }

            for (var i = 0; i < size; i++)
            {
                if (!thisContainer.TrySet(item, i)) continue;
                if (extras.OtherContainer != null)
                {
                    extras.OtherContainer.Remove(extras.OtherIndex);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// TODO Documentation
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void Clone(InventorySlotView[] from, InventorySlotView[] to) {
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
    }
}