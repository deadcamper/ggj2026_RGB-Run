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

    [SerializeField] private float colorChangeDuration = .5f;

    private enum ColorIndex
    {
        Red = 0,
        Green,
        Blue,
        None = int.MaxValue
    }
    
    [SerializeField] private ColorIndex _activeColorIndex = ColorIndex.None;

    [SerializeField] private bool canToggleOff = true;

    private void Start()
    {
        _image = GetComponent<UnityEngine.UI.Image>();
        SetFilter(_activeColorIndex);
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
        //Debug.Log($"OnColorToggle: {toggleEvent}");
        var (colorIndex, _) = toggleEvent;
        if (canToggleOff && colorIndex == _activeColorIndex)
        {
            // This is a toggle-off event, turn off overlay image
            _activeColorIndex = ColorIndex.None;
        }
        else
        {
            _activeColorIndex = colorIndex;
        }
        SetFilter(_activeColorIndex);
    }

    void SetFilter(ColorIndex colorIndex)
    {
        if(colorIndex == ColorIndex.None)
        {
            _image.enabled = false;
            return;
        }
        // Switch to new active color
        //var color = _colors[(int)colorIndex];
        //_image.color = color;
        _image.enabled = true;
    }

    private void Update()
    {
        if (_activeColorIndex != ColorIndex.None)
        {
            Color.RGBToHSV(_image.color, out float currentAngle, out _, out _);
            Color.RGBToHSV(_colors[(int)_activeColorIndex], out float goalAngle, out _, out _);
            var h = Mathf.MoveTowardsAngle(currentAngle * 360f, goalAngle * 360f, Time.deltaTime * 360 / colorChangeDuration) / 360f;
            _image.color = Color.HSVToRGB(h, 1, 1);
        }
    }

    private void OnValidate()
    {
        Debug.Assert(_colors.Length == 3, $"{nameof(ColorOverlay)}: expected {nameof(_colors)} to have exactly 3 elements but there were {_colors.Length}", this);
    }
}
