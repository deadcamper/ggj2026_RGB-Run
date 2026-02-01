using System;
using R3;

public static class EventBus
{
    // It's possible to extend EventBase with new record types that have strongly typed data
    public record EventBase(EventType Kind);
    
    public record SoundEvent(AudioManager.SoundEventType SoundEventType) : EventBase(EventType.SoundEvent);
    
    
    public enum EventType
    {
        SoundEvent, // This can trigger any sound
    }

    private static Subject<EventBase> _eventBus = new();
    public static Observable<EventBase> Events => _eventBus;
    
    // public static void Post(EventType eventType)
    // {
    //     var eventRecord = eventType switch
    //     {
    //         // EventType.UIClick => new EventBase(Kind: eventType),
    //         _ => throw new ArgumentException($"Event type {eventType} requires additional data, use the {nameof(Post)}({nameof(EventBase)}) overload instead.", nameof(eventType))
    //     };
    //     Post(eventRecord);
    // }
    
    public static void Post(EventBase eventRecord) {
        _eventBus.OnNext(eventRecord);
    }
}