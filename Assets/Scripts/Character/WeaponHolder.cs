using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{

    [SerializeField] private GameObject Weapon;

    [SerializeField] private Transform WeaponSocket;


    private Transform GripLocation;


    //Components
    private PlayerController PlayerController;
    private Animator PlayerAnimator;

    //Ref
    private Camera mainCamera;


    //Animator Hashes
    private readonly int AimVerticalHash = Animator.StringToHash("AimVertical");
    private readonly int AimHorizontalHash = Animator.StringToHash("AimHorizontal");


    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerAnimator = GetComponent<Animator>();

        mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(Weapon, WeaponSocket.position, WeaponSocket.rotation);
        if (!spawnedWeapon) return;
        spawnedWeapon.transform.parent = WeaponSocket;
        WeaponComponent weapon = spawnedWeapon.GetComponent<WeaponComponent>();
        GripLocation = weapon.HandPosition;
    }

    public void OnLook(UnityEngine.InputSystem.InputValue delta)
    {
        Vector3 IndependentMousePosition =
            mainCamera.ScreenToViewportPoint(PlayerController.CrosshairComponent.CurrentMousePosition);
        PlayerAnimator.SetFloat(AimVerticalHash, IndependentMousePosition.y);
        PlayerAnimator.SetFloat(AimHorizontalHash, IndependentMousePosition.x);

    }


    private void OnAnimatorIK(int layerIndex)
    {
        PlayerAnimator.SetIKHintPositionWeight((AvatarIKHint)AvatarIKGoal.LeftHand, 1);
        PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, GripLocation.position);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
