using UnityEngine;

namespace Inventory {
    public class InventoryItem : MonoBehaviour, IItem {
        public Sprite Sprite;

        public Sprite GetSprite() {
            return Sprite;
        }
    }
}