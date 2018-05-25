namespace Inventory {
    public class InventoryDatabase {
        private IItem[] items;

        public InventoryDatabase(int size) {
            items = new IItem[size];
        }

        public IItem GetItem(int i) {
            return items[i];
        }

        public void SetItem(IItem item, int i) {
            items[i] = item;
        }
    }
}