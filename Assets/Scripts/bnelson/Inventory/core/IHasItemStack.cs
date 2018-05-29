using JetBrains.Annotations;

namespace bnelson.Inventory.core {
    public interface IHasItemStack {
        [CanBeNull]
        IItemStack GetItemStack();

        void SetItemStack([CanBeNull] IItemStack itemStack);

        void SwapItemStack([NotNull] IHasItemStack hasItemStack);

        void Update();
    }
}