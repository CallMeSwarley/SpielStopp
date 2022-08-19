using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;


/// <summary>
///    Controller adapted largely from the First Person Controller Exercise of the IMGE Practical Course in Tum.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour, ICharacterSignals
{

    public IObservable<Vector3> Moved => _moved;
    private Subject<Vector3> _moved;  

    public IObservable<Unit> Stepped => _stepped;
    private Subject<Unit> _stepped;

    [Header("References")]
    [SerializeField] private FirstPersonControllerInput firstPersonControllerInput;
    private CharacterController _characterController;
    private Camera _camera;

    [Header("Movement Options")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = 5f;
    [SerializeField] private float strideLength = 2.5f;
    public float StrideLength => strideLength;

    [Header("Head Angels")]
    [Range(-90, 0)] [SerializeField] private float minViewAngle = -60f;
    [Range(0, 90)] [SerializeField] private float maxViewAngle = 60f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>();

        //var stepDistance = 0f;
        //Moved.Subscribe(w => {
        //    stepDistance += w.magnitude;
        //    if (stepDistance > strideLength) {
        //        _stepped.OnNext(Unit.Default);
        //   }
        //    stepDistance %= strideLength;
        //}).AddTo(this);
    }

    private void Start()
    {
        this.HandleLocomotion();
        this.Look();



        _moved = new Subject<Vector3>().AddTo(this);
        _stepped = new Subject<Unit>().AddTo(this);
    }

    private void HandleLocomotion()
    {
        //Movement
        _ = firstPersonControllerInput.Move
            .Subscribe(i =>
            {



                //horizontal movement
                // == if Run runspeed else walkspeed
                var horizontalVelocity = i * moveSpeed;

                //combine horizontal and vertical movement
                var characterSpeed = transform.TransformVector(new Vector3(horizontalVelocity.x, -gravity, horizontalVelocity.y));

                //apply movement
                var distance = characterSpeed * Time.deltaTime;
                _characterController.Move(distance);



                //if (!wasGrounded && _characterController.isGrounded) {
                //    _landed.OnNext(Unit.Default);
                //}
            }).AddTo(this);
    }



    private void Look()
    {
        firstPersonControllerInput.Look.Where(v => v != Vector2.zero).
            Subscribe(inputLook =>
            {
                //2D Vector in Euler Angles

                //Horizontal around vertical axis + being clockwise
                var horizontalLook = inputLook.x * Vector3.up * Time.deltaTime;
                transform.localRotation *= Quaternion.Euler(horizontalLook);

                //Vertical around the horizontal axis + being up
                var verticalLook = inputLook.y * Vector3.left * Time.deltaTime;
                var newQ = _camera.transform.localRotation * Quaternion.Euler(verticalLook);

                _camera.transform.localRotation = RotationTools.ClampRotationAroundXAxis(newQ, -maxViewAngle, -minViewAngle);




            }).AddTo(this);
    }

}
