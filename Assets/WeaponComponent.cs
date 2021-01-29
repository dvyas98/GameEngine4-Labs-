using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    public Transform HandPosition => GripIKLocation;
    [SerializeField] Transform GripIKLocation;

}
