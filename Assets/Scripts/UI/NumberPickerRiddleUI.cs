using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberPickerRiddleUI : MonoBehaviour
{
    [SerializeField] private List<PickNumberItem> listPicker;
    [SerializeField] private GameObject root;
    [SerializeField] private int rewardId;

    private RiddleItemUI riddleUI;
    private string key = "2020";
    private bool isSolved = false;

    private void OnEnable()
    {
        Observer.Instance.Subscribe(MessageType.InteractPressed, Submit);
    }

    private void OnDisable()
    {
        Observer.Instance.UnSubscribe(MessageType.InteractPressed, Submit);
    }

    private void Start()
    {
        riddleUI = GetComponent<RiddleItemUI>();
    }

    private void Submit(Message msg)
    {
        if (isSolved || !root.activeSelf)
        {
            return;
        }
        
        string answer = GetNumber();
        if (answer.Equals(key))
        {
            // todo: riddle resolved
            // Observer.Instance.Announce(new Message(MessageType.ShowPartical));
            riddleUI.Solve();
            Debug.Log("Close");
            StartCoroutine(RewardRoutine());

        }
    }

    private IEnumerator RewardRoutine()
    {
        InteractItemUIManager.Instance.CloseDetail();
        yield return new WaitForSeconds(0.3f);
        Observer.Instance.Announce(new Message(MessageType.ClaimReward,rewardId));
    }

    private string GetNumber()
    {
        string answer = "";
        foreach (var number in listPicker)
        {
            answer += number.Value;
        }

        return answer;
    }
}
