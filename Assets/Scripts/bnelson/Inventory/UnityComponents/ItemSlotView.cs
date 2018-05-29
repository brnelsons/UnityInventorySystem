using System;
using bnelson.Inventory.core;
using bnelson.Inventory.example;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace bnelson.Inventory.UnityComponents {
    public class ItemSlotView : MonoBehaviour, IHasItemStack, IBeginDragHandler, IDragHandler, IEndDragHandler,
        IDropHandler {
        private IItemStack _itemStack;
        public String CanvasObjectName;
        public Text Text;
        public Image Image;

        private Vector3 _originalPosition;

        public int Index { get; set; }

        private GameObject _dragwrapper;

        private void Start() {
            _itemStack = new ItemStack();
        }

        public void SetItemStack(IItemStack itemStack) {
            _itemStack = itemStack;
            UpdateView();
        }

        public IItemStack GetItemStack() {
            return _itemStack;
        }

        public void SwapItemStack(IHasItemStack hasItemStack) {
            var temp = _itemStack;
            SetItemStack(hasItemStack.GetItemStack());
            hasItemStack.SetItemStack(temp);
        }

        public void Update() {
            UpdateView();
        }

        private void UpdateView() {
            var alpha = 1f;
            if (_itemStack.GetCount() == 0)
            {
                alpha = 0f;
                Image.sprite = null;
            }
            else
            {
                Image.sprite = _itemStack.GetItems()[0].GetSprite();
                Text.text = _itemStack.GetCount().ToString();
            }

            Image.color = InventoryUtils.ChangeAlpha(Image.color, alpha);
            Text.color = InventoryUtils.ChangeAlpha(Text.color, alpha);
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (_itemStack.GetCount() == 0) return;
            _dragwrapper = new GameObject("Dragging");
            _dragwrapper.transform.parent = GameObject.Find(CanvasObjectName).transform;
            var image = _dragwrapper.AddComponent<Image>();
            image.sprite = Image.sprite;
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
            var otherItemHolder = eventData.pointerDrag.GetComponent<ItemSlotView>();
            if (otherItemHolder == null || otherItemHolder.GetItemStack() == null) return;
            //merge
            if (_itemStack.Merge(otherItemHolder.GetItemStack()))
            {
                UpdateView();
                otherItemHolder.UpdateView();
                return;
            }
            //swap
            SwapItemStack(otherItemHolder);

        }
    }
}