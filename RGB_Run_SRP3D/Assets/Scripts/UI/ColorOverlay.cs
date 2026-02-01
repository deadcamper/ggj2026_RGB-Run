using System;
using UnityEngine;
using UnityEngine.InputSystem;
using R3;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class ColorOverlay : MonoBehaviour
{
    [Tooltip("Colors to use for red, green, and blue overlay effects.")]
    [SerializeField] private Color[] _colors = {};

    private UnityEngine.UI.Image _image;

    private enum ColorIndex
    {
        Red = 0,
        Green,
        Blue,
        None = int.MaxValue
    }
    
    private ColorIndex _activeColorIndex = ColorIndex.None;

    private void Start()
    {
        _image = GetComponent<UnityEngine.UI.Image>();
        _image.enabled = false;
    }

    void OnEnable()
    {
        var toggleRedAction = InputSystem.actions.FindAction("ToggleRedFilter");
        var toggleGreenAction = InputSystem.actions.FindAction("ToggleGreenFilter");
        var toggleBlueAction = InputSystem.actions.FindAction("ToggleBlueFilter");


        var toggleRedObservable = MakeActionStartObservable(ColorIndex.Red, toggleRedAction);
        var toggleGreenObservable = MakeActionStartObservable(ColorIndex.Green, toggleGreenAction);
        var toggleBlueObservable = MakeActionStartObservable(ColorIndex.Blue, toggleBlueAction);

        var allColorTogglesObservable =
            Observable.Merge(toggleRedObservable, toggleGreenObservable, toggleBlueObservable);

        allColorTogglesObservable
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(OnColorToggle)
            .AddTo(this);

        Observable<(ColorIndex, InputAction.CallbackContext)> MakeActionStartObservable(ColorIndex colorIndex, InputAction inputAction)
        {
            return ObservableUtils.InputActionStartedAsObservable(inputAction)
                .Select(context => (colorIndex, context));
        }
    }

    void OnColorToggle((ColorIndex, InputAction.CallbackContext) toggleEvent)
    {
        Debug.Log($"OnColorToggle: {toggleEvent}");
        var (colorIndex, _) = toggleEvent;
        if (colorIndex == ColorIndex.None || colorIndex == _activeColorIndex)
        {
            // This is a toggle-off event, turn off overlay image
            _image.enabled = false;
            _activeColorIndex = ColorIndex.None;
            return;
        }

        // Switch to new active color
        var color = _colors[(int)colorIndex];
        _image.color = color;
        _image.enabled = true;
        _activeColorIndex = colorIndex;
    }

    private void OnValidate()
    {
        Debug.Assert(_colors.Length == 3, $"{nameof(ColorOverlay)}: expected {nameof(_colors)} to have exactly 3 elements but there were {_colors.Length}", this);
    }
}
