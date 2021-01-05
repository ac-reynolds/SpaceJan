using UnityEngine.Events;

/*
 * Generic event handler for events.  Use AddListener and RemoveListener to modify
 * listeners.  Use Invoke to invoke the event.  Any data passed to invoke will be
 * sent to listeners.  
 * 
 * Access this globally with GetInstance(), e.g.:
 * EventHandler<SetActiveControllerEvent>.GetInstance().AddListener(OnSetActiveControllerEvent);
 */
public class EventHandler<T>
{
    private static EventHandler<T> instance = new EventHandler<T>();
    public static EventHandler<T> GetInstance() {
        return instance;
    }

    private UnityEvent<T> _event;
    private EventHandler() {
        if (_event == null) {
            _event = new UnityEvent<T>();
        }
    }

    //passes args to anyone registered to receive a T
    public void Invoke(T args) {
        _event.Invoke(args);
    }

    public void AddListener(UnityAction<T> action) {
        _event.AddListener(action);
    }

    public void RemoveListener(UnityAction<T> action) {
        _event.RemoveListener(action);
    }
}
