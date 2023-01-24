using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private bool isOpen;

    private GridPosition gridPosition;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

        if (isOpen)
        {
            OpenWall();
        }
        else
        {
            CloseWall();
        }
    }
    public void OpenWall()
    {
        isOpen = true;
        animator.SetBool("IsOpen", true);
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, true);
    }

    public void CloseWall()
    {
        isOpen = false;
        animator.SetBool("IsOpen", false);
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, false);
    }
}
