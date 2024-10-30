using System;
using System.Collections;
using UnityEngine;

namespace Riddle
{
    public class BasicRiddle : MonoBehaviour
    {
        [SerializeField] private int itemCount = 3;
        [SerializeField] private int rewardId;
        private int count = 0;
        private void OnEnable()
        {
            Observer.Instance.Subscribe(MessageType.PlaceCorrectItem, OnPlaceItemToHole);
        }

        private void OnDisable()
        {
            Observer.Instance.UnSubscribe(MessageType.PlaceCorrectItem, OnPlaceItemToHole);
        }

        private void OnPlaceItemToHole(Message msg)
        {
            count++;
            if (count >= itemCount)
            {
                Debug.Log("Congrat");
                // Observer.Instance.Announce(new Message(MessageType.ShowPartical));
                // InteractItemUIManager.Instance.CloseDetail();
                // Observer.Instance.Announce(new Message(MessageType.ClaimReward,2));
                StartCoroutine(RewardRoutine());
            }
        }
        private IEnumerator RewardRoutine()
        {
            InteractItemUIManager.Instance.CloseDetail();
            yield return new WaitForSeconds(0.5f);
            Observer.Instance.Announce(new Message(MessageType.ClaimReward,rewardId));
        }
    }
}
