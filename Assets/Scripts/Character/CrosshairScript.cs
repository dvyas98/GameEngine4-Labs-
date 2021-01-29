using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    // Start is called before the first frame update
     
    public Vector2 CurrentMousePosition { get; private set; }

    public bool Invereted = false;

    public Vector2 MouseSenitivity = Vector2.zero;

    [SerializeField, Range(0.0f, 1.0f)]
    private float HorizontalPercentageConstrain;

    [SerializeField, Range(0.0f, 1.0f)]
    private float VerticalPercentageConstrain;


    
    private float HorizontalConstrain;
    private float VerticalConstrain;

    private Vector2 CrosshairStartingPointPosition;


    private Vector2 CurrentLookDelta = Vector2.zero;

    private float MinHorizontalConstrainValue;
    private float MaxHorizontalConstrainValue;
    private float MinVerticalConstrainValue;
    private float MaxVerticalConstrainValue;

    private GameInputActions InputActions;
    private void Awake()
    {
        InputActions = new GameInputActions();
    }
    private void Start()
    {
        if (GameManager.Instance.CursorActive)
        { 
            AppEvents.Invoke_MouseCursorEnable(false);
        }
        CrosshairStartingPointPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);

        HorizontalConstrain = (Screen.width * HorizontalPercentageConstrain) / 2f;
        MinHorizontalConstrainValue=  -(Screen.width / 2f) + HorizontalConstrain;
        MaxHorizontalConstrainValue = (Screen.width / 2f) - HorizontalConstrain;

        VerticalConstrain = (Screen.height * VerticalPercentageConstrain) * 2f;
        MinVerticalConstrainValue = -(Screen.height / 2f) + VerticalConstrain;
        MaxVerticalConstrainValue = (Screen.height / 2f) - VerticalConstrain;

    }

    // Update is called once per frame
    private void Update()
    {
        float crosshairXPosition = CrosshairStartingPointPosition.x + CurrentLookDelta.x;

        float crosshairYPosition = Invereted ?
            CrosshairStartingPointPosition.y - CurrentLookDelta.y
            : CrosshairStartingPointPosition.y + CurrentLookDelta.y;

        CurrentMousePosition = new Vector2(crosshairXPosition, crosshairYPosition);
        transform.position = CurrentMousePosition;
    }

    private void OnLook(UnityEngine.InputSystem.InputAction.CallbackContext delta)
    {
        Vector2 mouseDelta = delta.ReadValue<Vector2>();

        CurrentLookDelta.x += mouseDelta.x * MouseSenitivity.x;

        if(CurrentLookDelta.x >= MaxHorizontalConstrainValue || CurrentLookDelta.x <= MinHorizontalConstrainValue)
        {
            CurrentLookDelta.x -= mouseDelta.x * MouseSenitivity.x;
        }

        CurrentLookDelta.y += mouseDelta.y * MouseSenitivity.y;
        if (CurrentLookDelta.y >= MaxVerticalConstrainValue || CurrentLookDelta.y <= MinVerticalConstrainValue)
        {
            CurrentLookDelta.y -= mouseDelta.y * MouseSenitivity.y; 
        }
        {

        }
    }

    private void OnEnable()
    {
        InputActions.Enable();
        InputActions.ThirdPerson.Look.performed += OnLook;
    }

   
    private void OnDisable()
    {
        InputActions.Disable();
        InputActions.ThirdPerson.Look.performed -= OnLook;
    }

   
}
