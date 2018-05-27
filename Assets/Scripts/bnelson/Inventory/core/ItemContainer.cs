using bnelson.Inventory.example;

namespace bnelson.Inventory.core {
    /// <summary>
    /// This is to be used as a delegating container. This should never extend MonoBehavior or NetworkBehavior
    /// </summary>
    /// <typeparam name="T">The Object that holds an IItem</typeparam>
    public class ItemContainer<T> : IItemContainer where T : IHasItemStack {
        public delegate T HasItemInitializer(int index);

        private int _size;

        private T[] _stacks;
        private readonly HasItemInitializer _hasItemInitializer;

        public ItemContainer(int size, HasItemInitializer hasItemInitializer) {
            _hasItemInitializer = hasItemInitializer;
            SetSize(size);
        }

        public int GetSize() {
            return _size;
        }

        public void SetSize(int size) {
            var oldSlots = _stacks;
            _stacks = new T[size];
            for (var i = 0; i < size; i++)
            {
                _stacks[i] = _hasItemInitializer(i);
            }

            _size = size;
            InventoryUtils.CloneStack(oldSlots, _stacks);
        }

        public bool Add(IItem item) {
            return Add(ItemStack.Of(item));
        }

        public bool Add(IItemStack item) {
            foreach (var stack in _stacks)
            {
                if(stack.GetItemStack().Add(stack.GetItemStack())) return true;
            }

            return false;
        }

        public IHasItemStack Get(int index) {
            //TODO check for out of bounds here
            return _stacks[index];
        }

        public void MergeOrSwap(IItemContainer itemContainer, int index) {
            var otherItemStack = itemContainer.Get(index);
            var thisItemStack = _stacks[index];
            if (thisItemStack.GetItemStack().Add(otherItemStack.GetItemStack()))
            {
                return;
            }

            var temp = thisItemStack.GetItemStack();
            thisItemStack.SetItemStack(otherItemStack.GetItemStack());
            otherItemStack.SetItemStack(temp);
        }
    }
}