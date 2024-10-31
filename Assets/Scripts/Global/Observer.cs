using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.VersionControl;
#endif
using UnityEngine;


public class Message
{
    public MessageType msgType;
    public object param;
    public object returnValue;

    public Message(MessageType msgType, object param = null)
    {
        this.msgType = msgType;
        this.param = param;
    }
    public Message(object param)
    {
        this.param = param;
    }
}

public class Observer : Singleton<Observer>
{
    private Dictionary<MessageType, List<Action<Message>>> observer = new();

    public void Subscribe(MessageType type, Action<Message> callBack)
    {
        if (observer.ContainsKey(type))
        {
            observer[type].Add(callBack);
        }
        else
        {
            observer.Add(type, new List<Action<Message>> { callBack });
        }
    }
    public void UnSubscribe(MessageType type, Action<Message> callBack)
    {
        if (observer.ContainsKey(type))
        {
            if (observer[type].Count == 1)
            {
                observer.Remove(type);
            }
            else
            {
                observer[type].Remove(callBack);
            }
        }
        else
        {
            Debug.LogError("Unsubscribe fail: Event type doesn't exist");
        }
    }
    public void Announce(Message message)
    {
        MessageType type = message.msgType;
        if (observer.ContainsKey(type))
        {
            foreach (Action<Message> action in observer[type])
            {
                action?.Invoke(message);
            }
        }
        else
        {
            Debug.LogError("Announce fail: Event type doesn't exist");
        }
    }
}
