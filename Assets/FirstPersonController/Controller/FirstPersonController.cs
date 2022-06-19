using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;


/// <summary>
///     Controller that handles the character controls and camera controls of the first person player.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour, ICharacterSignals
{

    public IObservable<Vector3> Moved => _moved;
    private Subject<Vector3> _moved;

    public ReactiveProperty<bool> IsRunning => _isRunning;
    private ReactiveProperty<bool> _isRunning;

    public IObservable<Unit> Landed => _landed;
    private Subject<Unit> _landed;

    public IObservable<Unit> Jumped => _jumped;
    private Subject<Unit> _jumped;

    public IObservable<Unit> Stepped => _stepped;
    private Subject<Unit> _stepped;

    [Header("References")]
    [SerializeField] private FirstPersonControllerInput firstPersonControllerInput;
    private CharacterController _characterController;
    private Camera _camera;

    [Header("Movement Options")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float gravity = 5f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float strideLength = 2.5f;
    public float StrideLength => strideLength;

    [Header("Head Angels")]
    [Range(-90, 0)] [SerializeField] private float minViewAngle = -60f;
    [Range(0, 90)] [SerializeField] private float maxViewAngle = 60f;

    private void Awake() {
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
        

        _isRunning = new ReactiveProperty<bool>(false);
        _moved = new Subject<Vector3>().AddTo(this);
        _jumped = new Subject<Unit>().AddTo(this);
        _landed = new Subject<Unit>().AddTo(this);
        _stepped = new Subject<Unit>().AddTo(this);
    }

    private void HandleLocomotion() {
        //Start of grounded
        _characterController.Move(-gravity * transform.up);
        

        var jumpLatch = LatchObservables.Latch(this.UpdateAsObservable(), firstPersonControllerInput.Jump, false);


        //Movement
        _ = firstPersonControllerInput.Move
            .Zip(jumpLatch, (m, j) => new MoveInputData(m, j))
            .Where(moveInputData => moveInputData.jump ||
                                    moveInputData.move != Vector2.zero ||
                                   _characterController.isGrounded == false)
            .Subscribe(i =>
            {
                var wasGrounded = _characterController.isGrounded;

                //vertical movement
                var verticalSpeed = 0f;

                //Determine vertical movement
                //on the ground and want to jump
                if (i.jump && wasGrounded)
                {
                    verticalSpeed = jumpSpeed;
                    //_jumped.OnNext(Unit.Default);
                }
                //not grounded -> gravity
                else if (!wasGrounded)
                {
                    verticalSpeed = _characterController.velocity.y + (Physics.gravity.y * Time.deltaTime * 3.0f);
                }
                else
                //on the ground. Restore base state
                {
                    verticalSpeed = -Math.Abs(gravity);
                }

                //horizontal movement
                // == if Run runspeed else walkspeed
                var currentSpeed = firstPersonControllerInput.Run.Value ? runSpeed : moveSpeed;
                var horizontalVelocity = i.move * currentSpeed;

                //combine horizontal and vertical movement
                var characterSpeed = transform.TransformVector(new Vector3(horizontalVelocity.x, verticalSpeed, horizontalVelocity.y));

                //apply movement
                var distance = characterSpeed * Time.deltaTime;
                _characterController.Move(distance);

                var tempIsRunning = false;
                if (wasGrounded && _characterController.isGrounded)
                { 
                    _moved.OnNext(_characterController.velocity * Time.deltaTime);
                    if (_characterController.velocity.magnitude > 0)
                    {
                        tempIsRunning = firstPersonControllerInput.Run.Value;
                    }
                }

                //if (!wasGrounded && _characterController.isGrounded) {
                //    _landed.OnNext(Unit.Default);
                //}
            }).AddTo(this);
    }

   

    private void Look() {
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

    public struct MoveInputData {
        public readonly Vector2 move;
        public readonly bool jump;

        public MoveInputData(Vector2 move, bool jump) {
            this.move = move;
            this.jump = jump;
        }
    }
}
