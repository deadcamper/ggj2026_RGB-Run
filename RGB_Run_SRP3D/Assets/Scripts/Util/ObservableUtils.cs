using System;
using UnityEngine;
using R3;
using UnityEngine.InputSystem;

public class ObservableUtils
{
    public static Observable<InputAction.CallbackContext> InputActionStartedAsObservable(InputAction inputAction)
    {
        return Observable.FromEvent<InputAction.CallbackContext>(
            action => inputAction.started += action,
            action => inputAction.started -= action
        );
    }
}