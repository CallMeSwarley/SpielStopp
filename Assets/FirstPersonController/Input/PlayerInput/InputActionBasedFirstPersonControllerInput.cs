using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;


public class InputActionBasedFirstPersonControllerInput : FirstPersonControllerInput
{

    private IObservable<Vector2> _move;
    public override IObservable<Vector2> Move => _move;

    private IObservable<Vector2> _look;
    public override IObservable<Vector2> Look => _look;

    private ReadOnlyReactiveProperty<bool> _run;
    public override ReadOnlyReactiveProperty<bool> Run => _run;

    private Subject<Unit> _jump;
    public override IObservable<Unit> Jump => _jump;

    [Header("Look Properties")]
    [SerializeField] private float lookSmoothingFactor = 14.0f;

    private FirstPersonInputAction _controls;

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    protected void Awake()
    {
        _controls = new FirstPersonInputAction();

        //Hides Mouse and locks it in game window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Movement
        _move = this.UpdateAsObservable().Select(_ => _controls.Game.Move.ReadValue<Vector2>());

        //Look
        var smoothLookValue = new Vector2(0, 0);
        _look = this.UpdateAsObservable()
            .Select(_ =>
            {
                var rawLookValue = _controls.Game.Look.ReadValue<Vector2>();

                smoothLookValue = new Vector2(
                    Mathf.Lerp(smoothLookValue.x, rawLookValue.x, lookSmoothingFactor * Time.deltaTime),
                    Mathf.Lerp(smoothLookValue.y, rawLookValue.y, lookSmoothingFactor * Time.deltaTime)
                    );
                return smoothLookValue;
            });

        _run = this.UpdateAsObservable()
            .Select(_ => _controls.Game.Run.ReadValueAsObject() != null)
            .ToReadOnlyReactiveProperty();

        _jump = new Subject<Unit>().AddTo(this);
        _controls.Game.Jump.performed += context =>
        {
            _jump.OnNext(Unit.Default);
        };
    }
}