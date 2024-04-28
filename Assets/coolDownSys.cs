using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coolDownSys : MonoBehaviour
{
    [SerializeField] private float coolDownTime;

    private float nextFireTime;

    public bool isCoolingDown => Time.time < nextFireTime;

    public void StartCooldown() => nextFireTime = Time.time + coolDownTime;
}
