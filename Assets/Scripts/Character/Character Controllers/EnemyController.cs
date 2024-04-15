using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterManager
{
    private void FixedUpdate()
    {
        ApplyGravity();
    }
}
