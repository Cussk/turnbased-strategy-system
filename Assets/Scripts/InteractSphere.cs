using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractSphere : MonoBehaviour, IInteractable
{
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MovingWall movingWallGameObject;

    private GridPosition gridPosition;
    private Action onInteractionComplete;
    private bool isGreen;
    private bool isActive;
    private float timer;

    private void Awake()
    {
        movingWallGameObject = movingWallGameObject.GetComponent<MovingWall>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(gridPosition, this);


        SetColorRed();
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isActive = false;
            onInteractionComplete();
        }
    }


    private void SetColorGreen()
    {
        isGreen = true;
        meshRenderer.material = greenMaterial;
    }

    private void SetColorRed()
    {
        isGreen = false;
        meshRenderer.material = redMaterial;
    }

    public void Interact(Action onInteractionComplete)
    {
        this.onInteractionComplete = onInteractionComplete;
        isActive = true;
        timer = .5f;

        if (isGreen)
        {
            SetColorRed();
            movingWallGameObject.CloseWall();
        }
        else
        {
            SetColorGreen();
            movingWallGameObject.OpenWall();
        }
    }
}
