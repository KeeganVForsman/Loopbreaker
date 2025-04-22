using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Attacks : MonoBehaviour
{
    private PlayerInput playerControls;

    private void Awake()
    {
        playerControls = new PlayerInput();
    }
    public void OnEnable()
    {
        playerControls.Player.Enabled();
        playerControls.actions.FindAction("Attack").performed += OnAttack();
    }
}
