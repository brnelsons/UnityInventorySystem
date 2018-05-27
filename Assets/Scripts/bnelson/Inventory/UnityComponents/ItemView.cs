using bnelson.Inventory.core;
using bnelson.Inventory.example;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace bnelson.Inventory.UnityComponents {
    public class ItemView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {
        public IItemStack ItemStack;

        private Image _image;
        private Text _countText;
        private Vector3 _originalPosition;

        public int Index { get; set; }

        private GameObject _dragwrapper;

        private void Start() {
            ItemStack = new ItemStack();
            _image = GetComponent<Image>();
            _countText = GetComponent<Text>();
        }

        public void SetItemStack(IItemStack itemStack) {
            var alpha = 1f;
            if (itemStack.GetCount() == 0)
            {
                alpha = 0f;
                _image.sprite = null;
            }
            else
            {
                _image.sprite = itemStack.GetItems()[0].GetSprite();
            }

            ItemStack = itemStack;
            var tmpColor = _image.color;
            tmpColor.a = alpha;
            _image.color = tmpColor;
        }

        public IItemStack GetItemStack() {
            return ItemStack;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (ItemStack.GetCount() == 0) return;
            _dragwrapper = new GameObject("Dragging");
            _dragwrapper.transform.parent = GameObject.Find("Canvas").transform;
            var image = _dragwrapper.AddComponent<Image>();
            image.sprite = _image.sprite;
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData) {
            if (_dragwrapper == null) return;
            _dragwrapper.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (_dragwrapper == null) return;
            Destroy(_dragwrapper);
        }

        public void OnDrop(PointerEventData eventData) {
            var otherItemHolder = eventData.pointerDrag.GetComponent<ItemView>();
            if (otherItemHolder == null) return;
            var extras = new InventoryEventDelegates.Extras {
                ToIndex = Index,
                ToContainer = gameObject.GetComponentInParent<IItemContainer>(),
                FromContainer = otherItemHolder.GetComponentInParent<IItemContainer>(),
                FromIndex = otherItemHolder.Index
            };
            InventoryEventManager.AddItem(otherItemHolder.GetItemStack(), extras);
        }
    }
}