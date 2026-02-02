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

    [SerializeField] private float scollCoolDown = 0.2f;

    private enum ColorIndex
    {
        Red = 0,
        Green,
        Blue,
        None = int.MaxValue
    }
    
    [SerializeField] private ColorIndex _activeColorIndex = ColorIndex.None;

    private ColorIndex _previousColorIndex = ColorIndex.None;

    [SerializeField] private bool canToggleOff = true;

    private void Start()
    {
        _image = GetComponent<UnityEngine.UI.Image>();
        SetFilter(_activeColorIndex);
    }

    void OnEnable()
    {
        // EnableLegacyBindings();

        EnableNewBindings();
    }

    private void EnableNewBindings()
    {
        var onOffAction = InputSystem.actions.FindAction("ToggleFilter");
        var scrollUpAction = InputSystem.actions.FindAction("ScrollUpFilter");
        var scrollDownAction = InputSystem.actions.FindAction("ScrollDownFilter");

        ObservableUtils.InputActionStartedAsObservable(onOffAction)
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Debounce(TimeSpan.FromSeconds(scollCoolDown))
            .Subscribe(OnColorToggle)
            .AddTo(this);

        ObservableUtils.InputActionStartedAsObservable(scrollUpAction)
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Debounce(TimeSpan.FromSeconds(scollCoolDown))
            .Subscribe(OnColorScrollUp)
            .AddTo(this);

        ObservableUtils.InputActionStartedAsObservable(scrollDownAction)
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Debounce(TimeSpan.FromSeconds(scollCoolDown))
            .Subscribe(OnColorScrollDown)
            .AddTo(this);
    }

    private void EnableLegacyBindings()
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
            .Subscribe(OnColorRGBSelect)
            .AddTo(this);
    }

    Observable<(ColorIndex, InputAction.CallbackContext)> MakeActionStartObservable(ColorIndex colorIndex, InputAction inputAction)
    {
        return ObservableUtils.InputActionStartedAsObservable(inputAction)
            .Select(context => (colorIndex, context));
    }

    void OnColorRGBSelect((ColorIndex, InputAction.CallbackContext) toggleEvent)
    {
        var (colorIndex, _) = toggleEvent;
        if (canToggleOff && colorIndex == _activeColorIndex)
        {
            // This is a toggle-off event, turn off overlay image
            SetColor(ColorIndex.None);
        }
        else
        {
            SetColor(colorIndex);
        }
    }

    void OnColorScrollUp(InputAction.CallbackContext scrollEvent)
    {
        ColorIndex newColor;
        switch (_activeColorIndex)
        {
            case ColorIndex.Red:
                newColor = ColorIndex.Green;
                break;
            case ColorIndex.Green:
                newColor = ColorIndex.Blue;
                break;
            case ColorIndex.Blue:
            case ColorIndex.None:
            default:
                newColor = ColorIndex.Red;
                break;
        }
        SetColor(newColor);
    }

    void OnColorScrollDown(InputAction.CallbackContext scrollEvent)
    {
        ColorIndex newColor;
        switch (_activeColorIndex)
        {
            case ColorIndex.Red:
            case ColorIndex.None:
                newColor = ColorIndex.Blue;
                break;
            case ColorIndex.Green:
                newColor = ColorIndex.Red;
                break;
            case ColorIndex.Blue:
            default:
                newColor = ColorIndex.Green;
                break;
        }
        SetColor(newColor);
    }

    void OnColorToggle(InputAction.CallbackContext toggleEvent)
    {
        if (_activeColorIndex != ColorIndex.None)
            SetColor(ColorIndex.None);
        else if (_previousColorIndex != ColorIndex.None)
            SetColor(_previousColorIndex);
        else
            SetColor(ColorIndex.Red);
    }

    void SetColor(ColorIndex newColor)
    {
        _previousColorIndex = _activeColorIndex;
        _activeColorIndex = newColor;

        SetFilter(newColor);
        Services.instance.Get<AudioManager>()?.PlaySound(AudioManager.SoundEventType.LensSwitch);
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
