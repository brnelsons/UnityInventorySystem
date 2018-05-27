using bnelson.Inventory.core;
using UnityEngine;

namespace bnelson.Inventory.example {
    public class Item : MonoBehaviour, IItem {
        public Sprite Sprite;
        public string Name;
        public bool IsStackable = true;
        public int MaxStackCount = 100;

        public Sprite GetSprite() {
            return Sprite;
        }

        public string GetName() {
            return Name;
        }

        public bool GetIsStackable() {
            return IsStackable;
        }

        public int GetMaxStackCount() {
            return MaxStackCount;
        }

        public void Pickup() {
            Debug.Log("was picked up");
        }

        public void Drop() {
            Debug.Log("was dropped");
        }
    }
}