using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractItemUIManager : SingletonMono<InteractItemUIManager>
{
    [SerializeField] private InteractableItemUI itemUI;
    [Header("Riddle")] 
    [SerializeField] private RiddleItemUI basicRiddle;
    [SerializeField] private RiddleItemUI passwordRiddle;
    
    private bool isShowItem = false;
    private void OnEnable()
    {
        Observer.Instance.Subscribe(MessageType.ShowItemDetail,OnShowDetail);
        Observer.Instance.Subscribe(MessageType.ShowRiddleItem, OnShowRiddle);
    }

    private void OnDisable()
    {
        Observer.Instance.UnSubscribe(MessageType.ShowItemDetail,OnShowDetail);
        Observer.Instance.UnSubscribe(MessageType.ShowRiddleItem, OnShowRiddle);

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

    private void OnShowRiddle(Message msg)
    {
        if (msg.param == null)
            return;
        try
        {
            int id = (int) msg.param;
            
            ItemData item = ItemDataService.Instance.FindItemById(id);
            if (item != null)
            {
                ShowRiddle(item.riddleType);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void ShowRiddle(RiddeType type)
    {
        Debug.Log("ShowRiddle");
        if (passwordRiddle.IsSolved || isShowItem)
            return;
        
        isShowItem = true;
        switch (type)
        {
            case RiddeType.CheckPassword: passwordRiddle.Show(); break;
            case RiddeType.FillHole: basicRiddle.Show(); break;
        }
    }

    private void ShowDetail(ItemData item)
    {
        itemUI.Show(item);
        isShowItem = true;
        CameraManager.Instance.EnableBlurEffect();
        GameManager.Instance.Freeze();
    }

    public void CloseDetail()
    {
        itemUI.Close();
        basicRiddle.Hide();
        passwordRiddle.Hide();
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
