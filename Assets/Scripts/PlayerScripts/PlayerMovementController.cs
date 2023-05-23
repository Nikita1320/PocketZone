using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rotater rotater;
    [SerializeField] private AnimatorController animator;

    public void Move(Vector2 direction)
    {
        animator.MoveAnimation(direction != Vector2.zero);

        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        if (direction != Vector2.zero)
        {
            rotater.Rotate((direction.x < 0), this);
        }
    }
}
