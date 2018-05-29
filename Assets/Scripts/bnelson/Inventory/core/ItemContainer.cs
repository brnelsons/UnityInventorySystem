using JetBrains.Annotations;

namespace bnelson.Inventory.core {
    /// <inheritdoc />
    /// <summary>
    /// This is to be used as a delegating container. This should never extend MonoBehavior or NetworkBehavior
    /// </summary>
    /// <typeparam name="T">The Object that holds an IItem</typeparam>
    public class ItemContainer<T> : IItemContainer where T : IHasItemStack {
        public delegate T HasItemInitializer(int index);
    
        private int _size;

        private T[] _stacks;
        private readonly HasItemInitializer _hasItemInitializer;

        public ItemContainer(int size,
                             [NotNull] HasItemInitializer hasItemInitializer) {
            _hasItemInitializer = hasItemInitializer;
            SetSize(size);
        }

        public string Name { get; set; }

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

        public bool Add(IItemStack itemStackToAdd) {
            IHasItemStack firstOpenStack = null;
            foreach (var stack in _stacks)
            {
                var itemStack = stack.GetItemStack();
                if (firstOpenStack == null
                    && (itemStack == null
                        || itemStack.GetCount() == 0))
                {
                    firstOpenStack = stack;
                    continue;
                }

                if (itemStack != null
                    && itemStack.GetItemName() == itemStackToAdd.GetItemName()
                    && itemStack.Merge(itemStackToAdd))
                {
                    return true;
                }
            }

            if (firstOpenStack != null)
            {
                firstOpenStack.SetItemStack(itemStackToAdd);
                return true;
            }

            return false;
        }

        public void Update() {
            foreach (var stack in _stacks)
            {
                stack.Update();
            }
        }

        public IHasItemStack GetHasItemStack(int index) {
            //TODO check for out of bounds here
            return _stacks[index];
        }
    }
}