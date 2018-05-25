using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory {
    public class InventoryItemHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {
        public IItem InventoryItem;

        private Image _image;
        private Vector3 _originalPosition;

        public int Index { get; set; }

        private GameObject _dragwrapper;

        private void Start() {
            _image = GetComponent<Image>();
        }

        public void SetItem(IItem item) {
            var alpha = 1f;
            if (item == null)
            {
                alpha = 0f;
            }
            else
            {
                _image.sprite = item.GetSprite();
            }

            InventoryItem = item;
            var tmpColor = _image.color;
            tmpColor.a = alpha;
            _image.color = tmpColor;
        }

        public IItem GetItem() {
            return InventoryItem;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (InventoryItem == null) return;
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
            var otherItemHolder = eventData.pointerDrag.GetComponent<InventoryItemHolder>();
            if (otherItemHolder != null)
            {
                var extras = new InventoryEventDelegates.Extras {
                    Index = Index,
                    OtherContainer = gameObject.GetComponentInParent<IItemContainer>(),
                    OtherIndex = otherItemHolder.Index
                };
                InventoryEventManager.AddItem(otherItemHolder.GetItem(), extras);
            }
        }
    }
}