using bnelson.Inventory.core;
using UnityEngine;

namespace bnelson.Inventory.example {
    public class Item : MonoBehaviour, IItem {
        public Sprite Sprite;
        public string Name;
        public bool IsStackable = true;
        public int MaxStackCount = 100;

        private void Start() {
            if (Name == "")Debug.Log("Name cannot be left empty");
            if (Sprite == null)Debug.Log("Sprite cannot be null");
        }

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