using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }

    public event EventHandler OnTurnChanged;

    private int turnNumber = 1;
    private bool isPlayerTurn = true;

    private void Awake()
    {
        //checks if Instance already exists on a gameObject, logs error and destroys game object if already exists
        if (Instance != null)
        {
            Debug.LogError("There's more than one TurnSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn = !isPlayerTurn;

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber() 
    { 
        return turnNumber; 
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
