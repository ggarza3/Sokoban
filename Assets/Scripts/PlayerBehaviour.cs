using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements.Experimental;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public Tilemap walls;
    public LayerMask boxesLayer;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void OnMoveUp(InputValue value)
    {
        if (value.isPressed && canMove(Vector3.up))
            transform.position += Vector3.up;
    }
    public void OnMoveDown(InputValue value)
    {
        if (value.isPressed && canMove(Vector3.down))
            transform.position += Vector3.down;
    }
    public void OnMoveLeft(InputValue value)
    {
        if (value.isPressed && canMove(Vector3.left))
            transform.position += Vector3.left;
    }
    public void OnMoveRight(InputValue value)
    {
        if (value.isPressed && canMove(Vector3.right))
            transform.position += Vector3.right;
    }

    private bool canMove(Vector3 dir)
    {
        Vector3 newPos = transform.position + dir;
        // if wall, no
        if(walls.HasTile(walls.WorldToCell(newPos))) {
            anim.SetBool("isMoving", false);
            return false;
        }
        Collider2D boxColl = Physics2D.OverlapPoint(newPos,boxesLayer);
        // if box, check one more space
        if (boxColl)
        {
            //     if wall or box, no
            if (walls.HasTile(walls.WorldToCell(newPos+dir)) ||
                Physics2D.OverlapPoint(newPos+dir,boxesLayer))
            {
                anim.SetBool("isMoving", false);
                return false;
            }
            boxColl.gameObject.transform.position += dir;
            anim.SetBool("isMoving", true);
            return true;
        }
        anim.SetBool("isMoving", true);
        return true;
    }

    public void OnReset(InputValue value)
    {
        if (value.isPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
