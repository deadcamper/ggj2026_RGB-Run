using System;
using UnityEngine;
using UnityEngine.UI;
using R3;
using UnityEngine.Serialization;

[RequireComponent(typeof(Button))]
public class SoundEventTriggerOnClick : MonoBehaviour
{
    [SerializeField] private AudioManager.SoundEventType _soundEventType;

    private void Start()
    {
        var button = GetComponent<Button>();
        button.OnClickAsObservable()
            .TakeWhile(_ => this.isActiveAndEnabled)
            .Subscribe(_ =>
            {
                EventBus.Post(new EventBus.SoundEvent(_soundEventType));
            })
            .AddTo(this);
    }
}