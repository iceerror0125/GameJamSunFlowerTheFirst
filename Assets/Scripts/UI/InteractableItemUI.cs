using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractableItemUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI itemName;
   [SerializeField] private TextMeshProUGUI description;
   [SerializeField] private TextMeshProUGUI detail;
   [SerializeField] private Image image;
   [SerializeField] private GameObject root;
   [SerializeField] private Transform rootParent;
   [Header("Animation")]
   [SerializeField] private float duration = 0.5f; // close - open animation duration
   [SerializeField] private Transform target;
   [SerializeField] private Transform animationParent;
   [SerializeField] private RectTransform animationRect;
   

   private bool isCollectable;
   private ItemData data;
   private bool isChanged;

   private void OnEnable()
   {
      Observer.Instance.Subscribe(MessageType.ChangeToPart2, OnChangeSprite);
   }

   private void OnDisable()
   {
      Observer.Instance.UnSubscribe(MessageType.ChangeToPart2, OnChangeSprite);
   }

   private void OnChangeSprite(Message msg)
   {
      isChanged = true;
      if (data.secondSprite != null)
         image.sprite = data.secondSprite;
      if (string.IsNullOrEmpty(data.description2))
      {
         description.text = data.description2;
      }
   }

   public void Show(ItemData itemData)
   {
      AudioManager.Instance.OpenItem();
      ChangeCanvasBehaviour(1);
      SetData(itemData);
      DoScaleAnimation(1, CheckIsCollectable);
   }

   private void CheckIsCollectable()
   {
      if (isCollectable)
      {
         animationRect.gameObject.SetActive(true);
         image.transform.SetParent(animationParent);
      }
   }

   private void ChangeCanvasBehaviour(float alpha)
   {
      var canvas = root.GetComponent<CanvasGroup>();
      canvas.alpha = 1;
      canvas.interactable = alpha > 0.9;
   }
   
   public void Close()
   {
      DoScaleAnimation(0, () => 
      {
         ChangeCanvasBehaviour(0);
         if (isCollectable)
         {
            AddToInventoryAnimation();
         }
      });

   }

   private void AddToInventoryAnimation()
   {
      StartCoroutine(DoAnim());

      IEnumerator DoAnim()
      {
         yield return new WaitForSeconds(0.2f);
         animationRect.transform.DOMove(target.position, 1);
         animationRect.transform.DOScale(0, 1).OnComplete(() =>
         {
            animationRect.gameObject.SetActive(false);
            animationRect.transform.DOScale(1, 0);
            animationRect.offsetMax = Vector2.zero;
            animationRect.offsetMin = Vector2.zero;
            image.transform.SetParent(rootParent);
            image.transform.localScale = Vector3.one;
         });
      }
     
   }
   
   private void DoScaleAnimation(float scale, Action onComplete = null)
   {
      root.transform.DOScale(scale, duration).OnComplete(() => onComplete?.Invoke());
   }

   private void SetData(ItemData itemData)
   {
      if (data != null && data.id == itemData.id)
         return;

      int part = GameManager.Instance.part;
      data = itemData;
      itemName.text = itemData.name;
      description.text = itemData.description;
      if (part == 2 && !string.IsNullOrEmpty(itemData.description2))
      {
         description.text = itemData.description2;
      }
      isCollectable = itemData.canCollect;
      
      image.sprite = itemData.sprite;
      if (part == 2 && itemData.secondSprite != null)
      {
         image.sprite = itemData.secondSprite;

      }
      
      if (!string.IsNullOrEmpty(itemData.detail))
      {
         detail.gameObject.SetActive(true);
         detail.text = itemData.detail;
      }
      else
      {
         detail.gameObject.SetActive(false);
      }

      // add to inventory
      if (itemData.canCollect)
      {
         Inventory.Instance.AddNewItem(itemData.id);
      }
   }
}
