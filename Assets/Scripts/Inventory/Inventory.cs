using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Inventory : SingletonMono<Inventory>
{
   [SerializeField] private InventoryData inventoryData;
   [SerializeField] private List<Image> slots;
   [SerializeField] private TextMeshProUGUI itemName;
   [SerializeField] private RectTransform indicator;
   [SerializeField] private GameObject root;
   
   private List<ItemData> itemData = new();
   private int currentIndicatorIndex = 0;

   private void OnEnable()
   {
      Observer.Instance.Subscribe(MessageType.ShowInventory, OnShow);
   }

   private void OnDisable()
   {
      Observer.Instance.Subscribe(MessageType.ShowInventory, OnShow);

   }

   private void OnShow(Message msg)
   {
      Show();
   }

   private void Show()
   {
      ClearItemData();
      AddItemData();
      ClearUISlot();
      UpdateInventoryUI();
      SetIndicatorPosition(currentIndicatorIndex);
      EnableRoot(true);
      
      CameraManager.Instance.EnableBlurEffect();
      GameManager.Instance.Freeze();
   }

   private void Close()
   {
      EnableRoot(false);
      
      CameraManager.Instance.DisableBlurEffect();
      GameManager.Instance.UnFreeze();
   }

   public bool IsContainRequiredId(int requiredId)
   {
      return inventoryData.inventoryData.Contains(requiredId);
   }

   public void AddNewItem(int id)
   {
      if (inventoryData.inventoryData.Contains(id))
         return;
      
      inventoryData.inventoryData.Add(id);
   }

   public void RemoveItem(int id)
   {
      if (!inventoryData.inventoryData.Contains(id))
         return;
      
      inventoryData.inventoryData.Remove(id);
   }

   private void Update()
   {
      if (root.activeSelf)
      {
         if(Input.GetKeyDown(InputConstant.MoveLeft))
         {
            MoveIndicator(-1);
         }

         if (Input.GetKeyDown(InputConstant.MoveRight))
         {
            MoveIndicator(1);
         }
      }
      if (Input.GetKeyDown(InputConstant.Escape) && root.activeSelf)
      {
         Close();
      }
   }

   private void MoveIndicator(int direction)
   {
      currentIndicatorIndex += direction;
      if (currentIndicatorIndex < 0)
      {
         currentIndicatorIndex = slots.Count - 1;
      }

      if (currentIndicatorIndex >= slots.Count)
      {
         currentIndicatorIndex = 0;
      }
      SetIndicatorPosition(currentIndicatorIndex);
   }

   private void EnableRoot(bool enable)
   {
      root.SetActive(enable);
   }
   private void UpdateInventoryUI()
   {
      for (int i = 0; i < itemData.Count; i++)
      {
         slots[i].sprite = itemData[i].sprite;  
      }
   }

   private void SetIndicatorPosition(int slotIndex)
   {
      indicator.SetParent(slots[slotIndex].transform);
      indicator.offsetMax = Vector2.zero;
      indicator.offsetMin = Vector2.zero;

      SetItemName(slotIndex);
   }

   private void SetItemName(int slotIndex)
   {
      string name = "";
      if (slotIndex <= itemData.Count - 1)
      {
         name = itemData[slotIndex].name;
      }

      itemName.text = name;
   }

   private void AddItemData()
   {
      ItemDataService service = ItemDataService.Instance;
      foreach (var id in inventoryData.inventoryData)
      {
         ItemData item = service.FindItemById(id);
         itemData.Add(item);
      }
   }

   private void ClearUISlot()
   {
      foreach (var slot in slots)
      {
         slot.sprite = null;
      }
   }
   private void ClearItemData()
   {
      itemData.Clear();
   }
}
