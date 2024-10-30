using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : BaseItem
{
   private void OnEnable()
   {
      Observer.Instance.Subscribe(MessageType.ClaimReward, OnClaimReward);
      Observer.Instance.Subscribe(MessageType.ChangeToPart2, OnChangeSprite);
   }

   private void OnDisable()
   {
      Observer.Instance.UnSubscribe(MessageType.ClaimReward, OnClaimReward);
      Observer.Instance.UnSubscribe(MessageType.ChangeToPart2, OnChangeSprite);
   }

   private void OnClaimReward(Message msg)
   {
      if (msg == null) 
         return;

      int param = (int)msg.param;
      Observer.Instance.Announce(new Message(MessageType.ShowItemDetail, param));
   }

   private void OnChangeSprite(Message msg)
   {
      ItemData item = ItemDataService.Instance.FindItemById(id);
      if (item.secondSprite != null)
         spriteRenderer.sprite = item.secondSprite;
      
   }
   public override void Interact()
   {
      base.Interact();
      Observer.Instance.Announce(new Message(MessageType.ShowItemDetail, id));
      spriteRenderer.material = NormalMaterial;
      ItemData item = ItemDataService.Instance.FindItemById(id);
      if (item.canCollect)
         Destroy(this.gameObject);
   }
}
