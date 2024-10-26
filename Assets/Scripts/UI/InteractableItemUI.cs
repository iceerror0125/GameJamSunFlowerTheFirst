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
   public void Show(ItemData itemData)
   {
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
      itemName.text = itemData.name;
      description.text = itemData.description;
      image.sprite = itemData.sprite;
      isCollectable = itemData.canCollect;
      
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
