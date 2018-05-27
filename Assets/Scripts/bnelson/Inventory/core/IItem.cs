using UnityEngine;

namespace bnelson.Inventory.core {
    public interface IItem {
        Sprite GetSprite();
        string GetName();
        bool GetIsStackable();
        int GetMaxStackCount();
        void Pickup();
        void Drop();
    }
}