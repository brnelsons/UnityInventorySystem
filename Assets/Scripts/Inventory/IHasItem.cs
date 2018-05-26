using JetBrains.Annotations;

namespace Inventory {
    public interface IHasItem {
        
        [CanBeNull]
        IItem GetItem();
        
        void SetItem([CanBeNull] IItem item);
    }
}