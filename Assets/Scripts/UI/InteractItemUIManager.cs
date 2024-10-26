using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractItemUIManager : MonoBehaviour
{
    [SerializeField] private InteractableItemUI itemUI;
    private bool isShowItem = false;
    private void OnEnable()
    {
        Observer.Instance.Subscribe(MessageType.ShowItemDetail,OnShowDetail);
    }

    private void OnDisable()
    {
        Observer.Instance.UnSubscribe(MessageType.ShowItemDetail,OnShowDetail);
    }

    private void OnShowDetail(Message msg)
    {
        if (msg.param == null)
            return;
        try
        {
            int id = (int) msg.param;
            
            ItemData item = ItemDataService.Instance.FindItemById(id);
            if (item != null)
            {
                ShowDetail(item);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void ShowDetail(ItemData item)
    {
        itemUI.Show(item);
        isShowItem = true;
        CameraManager.Instance.EnableBlurEffect();
        GameManager.Instance.Freeze();
    }

    private void CloseDetail()
    {
        itemUI.Close();
        isShowItem = false;
        CameraManager.Instance.DisableBlurEffect();
        GameManager.Instance.UnFreeze();
    }

    private void Update()
    {
        if (Input.GetKeyDown(InputConstant.Escape) && isShowItem)
        {
            CloseDetail();
        }
    }
}
