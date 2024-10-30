using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RiddleItemUI : MonoBehaviour
{
   [SerializeField] private float animDuration;
   [SerializeField] private float hideY = -1100f;
   [SerializeField] private RectTransform root;
   
   private bool isShow = false;
   private bool isSolved;
   public bool IsSolved => isSolved;

   public void Solve()
   {
      isSolved = true;  
   }
   private void Start()
   {
      DoAnim(hideY, 0);
   }

   public void Show()
   {
      if (isShow)
         return;
      isShow = true;
      root.gameObject.SetActive(true);
      DoAnim(0, animDuration);
   }

   public void Hide()
   {
      if (!isShow)
         return;
      isShow = false;
      root.gameObject.SetActive(false);
      DoAnim(hideY, animDuration);
   }

   private void DoAnim(float value, float duration)
   {
      DOTween.To(() => root.offsetMax, y => root.offsetMax = y, new Vector2(0,-value), duration);
      DOTween.To(() => root.offsetMin, y => root.offsetMin = y, new Vector2(0,-value), duration);
   }
}
