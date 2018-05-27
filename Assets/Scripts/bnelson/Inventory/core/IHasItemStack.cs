using JetBrains.Annotations;

namespace bnelson.Inventory.core {
    public interface IHasItemStack {
        [NotNull]
        IItemStack GetItemStack();

        void SetItemStack([NotNull] IItemStack itemStack);
    }
}