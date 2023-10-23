using JohnStairs.RCC.Inputs;
using UnityEngine;

namespace JohnStairs.RCC.Character.Motor {
    public class RPGController : MonoBehaviour, IRPGController {
        /// <summary>
        /// If set to false, all character controls are disabled
        /// </summary>
        [Tooltip("If set to false, all character controls are disabled.")]
        public bool ActivateControl = true;
        /// <summary>
        /// If true, Unity's legacy input system is used
        /// </summary>
        [Tooltip("If true, Unity's legacy input system is used.")]
        public bool UseLegacyInputSystem = false;
        /// <summary>
        /// Logs warnings for legacy inputs which are not set up in the project but used in this script
        /// </summary>
        [Tooltip("Logs warnings for legacy inputs which are not set up in the project but used in this script.")]
        public bool LogInputWarnings = true;
        /// <summary>
        /// If true, input values are normalized, i.e. they are rounded to 1 or -1 whenever there is input, otherwise 0. No real numbers in between
        /// </summary>
        [Tooltip("If true, input values are normalized, i.e. they are rounded to 1 or -1 whenever there is input, otherwise 0. No real numbers in between.")]
        public bool NormalizeInputValues = false;

        /// <summary>
        /// Reference to the used RPGMotor script
        /// </summary>
        protected RPGMotor _rpgMotor;
        /// <summary>
        /// Interface for getting character information, e.g. movement impairing effects
        /// </summary>
        protected RPGInputActions _inputActions;
        /// <summary>
        /// Interface for getting pointer information, e.g. if the pointer is over the GUI
        /// </summary>
        protected IPointerInfo _pointerInfo;
        /// <summary>
        /// True if the pointer is over a GUI element
        /// </summary>
        protected bool _pointerOverGUI = false;
        #region Input values
        /// <summary>
        /// 2D axis input for the movement direction
        /// </summary>
        protected Vector2 _inputMovement;
        /// <summary>
        /// If true, character movement starts this frame
        /// </summary>
        protected bool _inputMovementStart = false;
        /// <summary>
        /// Horizontal rotation input
        /// </summary>
        protected float _inputRotate = 0;
        /// <summary>
        /// If true, rotation started this frame
        /// </summary>
        protected bool _inputRotateStart = false;
        /// <summary>
        /// Strafe input axis, i.e. sideward movement
        /// </summary>
        protected float _inputStrafe = 0;
        /// <summary>
        /// If true, strafing started this frame
        /// </summary>
        protected bool _inputStrafeStart = false;
        /// <summary>
        /// First input for forward movement via two combined inputs
        /// </summary>
        protected bool _inputMoveForwardHalf1 = false;
        protected bool _inputMoveForwardHalf1Start = false;
        /// <summary>
        /// Second input for forward movement via two combined inputs
        /// </summary>
        protected bool _inputMoveForwardHalf2 = false;
        protected bool _inputMoveForwardHalf2Start = false;
        /// <summary>
        /// If true, rotation should be modified
        /// </summary>
        protected bool _inputRotationModifier = false;
        /// <summary>
        /// If true, rotation modification started this frame
        /// </summary>
        protected bool _inputRotationModifierStart = false;
        /// <summary>
        /// Jump input
        /// </summary>
        protected bool _inputJump = false;
        /// <summary>
        /// Sprint input (run speed modifier)
        /// </summary>
        protected bool _inputSprint = false;
        /// <summary>
        /// If true, the character should dive while swimming
        /// </summary>
        protected bool _inputDive;
        /// <summary>
        /// If true, the character should surface while swimming
        /// </summary>
        protected bool _inputSurface = false;
        /// <summary>
        /// If true, autorunning is toggled
        /// </summary>
        protected bool _inputToggleAutorunning = false;
        /// <summary>
        /// If true, walking is toggled
        /// </summary>
        protected bool _inputToggleWalking = false;
        /// <summary>
        /// If true, crouching is toggled
        /// </summary>
        protected bool _inputToggleCrouching = false;
        /// <summary>
        /// If true, the character should with the camera view direction
        /// </summary>
        protected bool _inputAlignWithCamera = false;
        /// <summary>
        /// If true, camera rotation is prevented in cases where the camera implicitely rotates with the character, e.g. because the character rotates
        /// </summary>
        protected bool _inputPauseCameraRotation = false;
        /// <summary>
        /// If true, all climbing activities are canceled
        /// </summary>
        protected bool _inputCancelClimbing = false;
        #endregion Input values

        protected virtual void Awake() {
            _rpgMotor = GetComponent<RPGMotor>();
            if (!_rpgMotor) {
                Debug.LogWarning("Game object " + this.name + " has the RPGController script attached but no RPGMotor! Please add an RPGMotor component to use this controller");
                return;
            }
            _inputActions = RPGInputManager.GetInputActions();
            _pointerInfo = GetComponent<IPointerInfo>();
        }

        protected virtual void Start() {
            GetInputs(LogInputWarnings);
            SetupInputActionCallbacks();
        }

        protected virtual void Update() {
            if (!_rpgMotor) {
                return;
            }

            if (!ActivateControl) {
                // Early return if the controls are disabled
                _rpgMotor.SetInputDirection(Vector3.zero);
                _rpgMotor.ToggleAutorunning(false);
                _rpgMotor.Surface(false);
                _rpgMotor.Dive(false);
                _rpgMotor.SetAlignWithCamera(false);
                _rpgMotor.StartMotor();
                ConsumeEventInputs();
                return;
            }

            _pointerOverGUI = _pointerInfo?.IsPointerOverGUI() ?? false;

            GetInputs();

            #region Process movement inputs
            // Get the vertical movement direction/input
            float vertical = _inputMovement.y;
            // Check if both select buttons are pressed
            if (_inputMoveForwardHalf1 && _inputMoveForwardHalf2
                && !_pointerOverGUI) {
                // Let the character move forward
                vertical = 1.0f;
            }

            // Check the autorun input
            if (_inputToggleAutorunning) {
                _rpgMotor.ToggleAutorunning();
            }

            bool forwardMovementStarted = _inputMovementStart && _inputMovement.y != 0
                                            || CombinedMoveForwardStart();

            if (forwardMovementStarted) {
                // Disable autorunning
                _rpgMotor.ToggleAutorunning(false);
            }


            // Get the horizontal movement input
            float horizontal = _inputMovement.x;
            // Get the rotation input
            float rotation = _inputRotate;
            // Get the strafe input				
            float strafe = _inputStrafe;

            if (NormalizeInputValues) {
                vertical = Utils.Normalize(vertical);
                horizontal = Utils.Normalize(horizontal);
                rotation = Utils.Normalize(rotation);
                strafe = Utils.Normalize(strafe);
            }

            // Create and set the player's input direction inside the motor
            Vector3 inputDirection = new Vector3(horizontal, 0, vertical);

            _rpgMotor.SetInputDirection(inputDirection);
            _rpgMotor.SetStrafe(strafe);
            _rpgMotor.SetRotation(rotation);
            _rpgMotor.SetRotationInputModification(_inputRotationModifier);

            // Set midair movement input
            if (forwardMovementStarted 
                || _inputStrafeStart 
                || RotationModificationStart()) {
                _rpgMotor.TryMidairMovement();
            }

            // Set sprinting inside the motor
            _rpgMotor.Sprint(_inputSprint);
            // Toggle walking inside the motor
            _rpgMotor.ToggleWalking(_inputToggleWalking);
            // Toggle crouching inside the motor
            _rpgMotor.ToggleCrouching(_inputToggleCrouching);
            
            if (_inputCancelClimbing) {
                _rpgMotor.CancelClimbing();
            }            

            // Check if the jump button is pressed down
            if (_inputJump) {
                // Signal the motor to jump
                _rpgMotor.Jump();
            }

            // Signal the motor to surface
            _rpgMotor.Surface(_inputSurface);
            // Signal the motor to dive
            _rpgMotor.Dive(_inputDive);
            #endregion Process movement inputs            

            // Camera-related inputs
            _rpgMotor.SetAlignWithCamera(_inputAlignWithCamera);
            _rpgMotor.PauseCameraRotation(_inputPauseCameraRotation);

            // Start the motor
            _rpgMotor.StartMotor();

            ConsumeEventInputs();
        }

        /// <summary>
        /// Enables the controller (and referenced motor)
        /// </summary>
        public virtual void Enable() {
            enabled = true;
            if (_rpgMotor != null) {
                _rpgMotor.enabled = true;
            }
        }

        /// <summary>
        /// Disables the controller (and referenced motor). For disabling only user input, use ActivateControl
        /// </summary>
        public virtual void Disable() {
            enabled = false;
            if (_rpgMotor != null) {
                _rpgMotor.enabled = false;
            }
        }

        /// <summary>
        /// Checks if forward movement via two combined inputs starts this frame
        /// </summary>
        /// <returns>True if combined input forward movement starts this frame, otherwise false</returns>
        protected virtual bool CombinedMoveForwardStart() {
            return !_pointerOverGUI &&
                    (_inputMoveForwardHalf1Start && _inputMoveForwardHalf2
                    || _inputMoveForwardHalf1 && _inputMoveForwardHalf2Start);
        }

        /// <summary>
        /// Checks if rotation modification starts this frame
        /// </summary>
        /// <returns>True if rotation modification starts this frame, otherwise false</returns>
        protected virtual bool RotationModificationStart() {
            return !_pointerOverGUI &&
                    (_inputRotationModifierStart && _inputRotate != 0
                    || _inputRotationModifier && _inputRotateStart);
        }

        /// <summary>
        /// Tries to get the inputs used by this script. If logWarnings is true, warnings are logged if inputs of Unity's legacy input system are not set up
        /// </summary>
        /// <param name="logWarnings">If true and Unity's legacy input system is used, warnings are logged whenever an input could not be found</param>
        protected virtual void GetInputs(bool logWarnings = false) {
            if (!UseLegacyInputSystem) {
                // Poll inputs
                // Movement input
                _inputMovement = _inputActions.Character.Movement.ReadValue<Vector2>();

                // Rotation input
                _inputRotate = _inputActions.Character.Rotate.ReadValue<float>();

                // Strafe input
                _inputStrafe = _inputActions.Character.Strafe.ReadValue<float>();

                // Rotation modifier input
                _inputRotationModifier = _inputActions.Character.RotationModifier.ReadValue<float>() > 0 && !_pointerOverGUI;

                // Inputs for forward movement via two combined inputs
                _inputMoveForwardHalf1 = _inputActions.Character.MoveForwardHalf1.ReadValue<float>() > 0;
                _inputMoveForwardHalf2 = _inputActions.Character.MoveForwardHalf2.ReadValue<float>() > 0;

                // Action inputs
                _inputSprint = _inputActions.Character.Sprint.ReadValue<float>() > 0;
                _inputDive = _inputActions.Character.Dive.ReadValue<float>() > 0;
                _inputSurface = _inputActions.Character.Surface.ReadValue<float>() > 0;

                _inputAlignWithCamera = _inputActions.Character.AlignWithCamera.ReadValue<float>() > 0 && !_pointerOverGUI;
                _inputPauseCameraRotation = _inputActions.Character.PauseCameraRotation.ReadValue<float>() > 0 && !_pointerOverGUI;
            } else {
                // Try to get Unity legacy inputs
                // Movement input
                float inputVerticalMovement = Utils.TryGetAxis(Utils.InputPhase.Raw, "Vertical", logWarnings);
                float inputHorizontalMovement = Utils.TryGetAxis(Utils.InputPhase.Raw, "Horizontal", logWarnings);
                _inputMovement = new Vector2(inputHorizontalMovement, inputVerticalMovement);
                _inputMovementStart = Utils.TryGetButton(Utils.InputPhase.Down, "Vertical", logWarnings) || Utils.TryGetButton(Utils.InputPhase.Down, "Horizontal", logWarnings);

                // Rotation input
                _inputRotate = Utils.TryGetAxis(Utils.InputPhase.Raw, "Rotate", logWarnings);
                _inputRotateStart = Utils.TryGetButton(Utils.InputPhase.Down, "Rotate", logWarnings);

                // Strafe input
                _inputStrafe = Utils.TryGetAxis(Utils.InputPhase.Raw, "Strafe", logWarnings);
                _inputStrafeStart = Utils.TryGetButton(Utils.InputPhase.Down, "Strafe", logWarnings);

                // Rotation modifier input
                _inputRotationModifier = Utils.TryGetButton(Utils.InputPhase.Pressed, "Fire2", logWarnings) && !_pointerOverGUI;
                _inputRotationModifierStart = Utils.TryGetButton(Utils.InputPhase.Down, "Fire2", logWarnings) && !_pointerOverGUI;

                // Inputs for forward movement via two combined inputs
                _inputMoveForwardHalf1 = Utils.TryGetButton(Utils.InputPhase.Pressed, "Fire1", logWarnings);
                _inputMoveForwardHalf1Start = Utils.TryGetButton(Utils.InputPhase.Down, "Fire1", logWarnings);
                _inputMoveForwardHalf2 = Utils.TryGetButton(Utils.InputPhase.Pressed, "Fire2", logWarnings);
                _inputMoveForwardHalf2Start = Utils.TryGetButton(Utils.InputPhase.Down, "Fire2", logWarnings);

                // Action inputs
                _inputJump = Utils.TryGetButton(Utils.InputPhase.Down, "Jump", logWarnings);
                _inputSprint = Utils.TryGetButton(Utils.InputPhase.Pressed, "Sprint", logWarnings);
                _inputDive = Utils.TryGetButton(Utils.InputPhase.Pressed, "Dive", logWarnings);
                _inputSurface = Utils.TryGetButton(Utils.InputPhase.Pressed, "Jump", logWarnings);
                _inputToggleAutorunning = Utils.TryGetButton(Utils.InputPhase.Down, "Autorun Toggle", logWarnings);
                _inputToggleWalking = Utils.TryGetButton(Utils.InputPhase.Up, "Walk Toggle", logWarnings);
                _inputToggleCrouching = Utils.TryGetButton(Utils.InputPhase.Down, "Crouch Toggle", logWarnings);
                _inputCancelClimbing = Utils.TryGetButton(Utils.InputPhase.Down, "Cancel", logWarnings);

                _inputAlignWithCamera = Utils.TryGetButton(Utils.InputPhase.Pressed, "Fire2", logWarnings) && !_pointerOverGUI;
                _inputPauseCameraRotation = Utils.TryGetButton(Utils.InputPhase.Pressed, "Fire1", logWarnings) && !_pointerOverGUI;
            }
        }

        /// <summary>
        /// Sets up all input action callbacks
        /// </summary>
        protected virtual void SetupInputActionCallbacks() {
            if (UseLegacyInputSystem) {
                return;
            }
            // Movement input
            _inputActions.Character.Movement.started += context => _inputMovementStart = true;
            
            // Rotate input
            _inputActions.Character.Rotate.started += context => _inputRotateStart = true;

            // Strafe input
            _inputActions.Character.Strafe.started += context => _inputStrafeStart = true;

            // Rotation modifier input
            _inputActions.Character.RotationModifier.started += context => _inputRotationModifierStart = true;

            // Inputs for forward movement via two combined inputs
            _inputActions.Character.MoveForwardHalf1.started += context => _inputMoveForwardHalf1Start = true;
            _inputActions.Character.MoveForwardHalf2.started += context => _inputMoveForwardHalf2Start = true;

            // Action inputs
            _inputActions.Character.Jump.performed += context => _inputJump = true;
            _inputActions.Character.ToggleAutorunning.performed += context => _inputToggleAutorunning = true;
            _inputActions.Character.ToggleWalking.performed += context => _inputToggleWalking = true;
            _inputActions.Character.ToggleCrouching.performed += context => _inputToggleCrouching = true;            
            _inputActions.Character.CancelClimbing.performed += context => _inputCancelClimbing = true;
        }

        /// <summary>
        /// Resets all variables which are set by input action callbacks
        /// </summary>
        protected virtual void ConsumeEventInputs() {
            if (UseLegacyInputSystem) {
                return;
            }
            _inputRotationModifierStart = false;
            _inputMoveForwardHalf1Start = false;
            _inputMoveForwardHalf2Start = false;
            _inputMovementStart = false;
            _inputRotateStart = false;
            _inputStrafeStart = false;
            _inputJump = false;
            _inputToggleAutorunning = false;
            _inputToggleWalking = false;
            _inputToggleCrouching = false;
            _inputCancelClimbing = false;
        }

        public virtual void ActivateControls() {
            ActivateControl = true;
        }

        public virtual void DeactivateControls() {
            ActivateControl = false;
        }
    }
}
