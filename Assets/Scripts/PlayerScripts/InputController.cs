using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private HoldButton attackButton;
    [SerializeField] private Button reloadButton;
    [SerializeField] private PlayerCombatSystem combatSystem;
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private CinemachineVirtualCamera followCamera;
    private void Start()
    {
        reloadButton?.onClick.AddListener(TryReload);
    }

    private void Update()
    {
        if (attackButton.IsPressed)
        {
            if (combatSystem)
                combatSystem?.Attack();
        }
        if (movementController)
            movementController.Move(joystick.Direction);
    }
    private void TryReload()
    {
        if (combatSystem != null)
            combatSystem.Reload();
    }
    public void Initialize(PlayerCombatSystem playerCombatSystem, PlayerMovementController playerMovementController)
    {
        combatSystem = playerCombatSystem;
        movementController = playerMovementController;
        followCamera.Follow = playerCombatSystem.transform;
    }
}
