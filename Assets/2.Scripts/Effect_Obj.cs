using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Obj : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 1f);
    }
}
