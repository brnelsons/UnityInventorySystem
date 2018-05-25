using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory {
    public class InventoryItemHolder: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        public IItem InventoryItem;

        private Image _image;
        private Vector3 _originalPosition;

        private GameObject _dragwrapper;
        
        private void Start() {
            _image = GetComponent<Image>();
        }

        public void SetItem(IItem item) {
            InventoryItem = item;
            _image.sprite = item.GetSprite();
            var tmpColor = _image.color;
            tmpColor.a = 1f;
            _image.color = tmpColor;
        }

        public IItem GetItem() {
            return InventoryItem;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (InventoryItem == null) return;
            _dragwrapper = new GameObject("Dragging");
            _dragwrapper.transform.parent = GameObject.Find("Canvas").transform;
            _dragwrapper.AddComponent<Image>().sprite = _image.sprite;
        }

        public void OnDrag(PointerEventData eventData) {
            if (InventoryItem == null) return;
            _dragwrapper.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (InventoryItem == null) return;
            Destroy(_dragwrapper);
        }
    }
}