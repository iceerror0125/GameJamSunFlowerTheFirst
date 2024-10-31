using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Riddle
{
    public class BasicRiddleItem : MonoBehaviour, IDragHandler
    {
        [SerializeField] private Transform parent;
        [SerializeField] private float maxDistance = 10;
        
        private bool isSetToParent = false;
        private bool canDrag = true;

        public void OnDrag(PointerEventData eventData)
        {
            if (!canDrag)
                return;
            
            transform.position = Input.mousePosition;
        }

        private void Update()
        {
            if (isSetToParent)
                return;
            
            float distance = Vector2.Distance(transform.position, parent.position);
            if (distance < maxDistance)
            {
                SetToParent(); 
                AudioManager.Instance.Click();
                Observer.Instance.Announce(new Message(MessageType.PlaceCorrectItem));
            }
        }

        private void SetToParent()
        {
            isSetToParent = true;
            canDrag = false;
            transform.position = parent.position;
        }
    }
}
