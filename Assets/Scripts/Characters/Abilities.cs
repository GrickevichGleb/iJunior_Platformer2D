using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [SerializeField] private VampirismAbility _vampirism;

    public void UseVampirism()
    {
        _vampirism.TryUseAbility();
    }
}
