﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public string PlayerId;
    public List<Cannon> Cannons;
    private const int MaxCannonBalls = 4;
    private int _cannonBalls = MaxCannonBalls;
    // Update is called once per frame
    void Update()
    {
        if (CanSwitchCannon() && Input.GetButtonDown(PlayerId + "_player_switch"))
        {
            var activeCannon = GetActiveCannon();
            var unactiveCannon = GetUnactiveCannon();

            activeCannon.currentCannon = false;
            unactiveCannon.currentCannon = true;
        }

        if (Input.GetButtonDown(PlayerId + "_player_toggleTurning"))
        {
            GetActiveCannon().ToggleTurning();
        }

        if (Input.GetButtonDown(PlayerId + "_player_fire"))
        {
            if (_cannonBalls > 0)
            {
                GetActiveCannon().Fire();

                _cannonBalls -= 1;
            }
        }
    }

    private bool CanSwitchCannon()
    {
        return Cannons.Count > 1;
    }

    private Cannon GetActiveCannon()
    {
        return Cannons.First(c => c.currentCannon);
    }

    private Cannon GetUnactiveCannon()
    {
        return Cannons.First(c => !c.currentCannon);
    }

    public bool CanAddCannonBall()
    {
        return _cannonBalls < MaxCannonBalls;
    }
    public void AddCannonBall()
    {
        _cannonBalls += 1;
    }

    public void RemoveOneCannon()
    {
        var activeCannon = GetActiveCannon();
        Cannons.Remove(activeCannon);
        activeCannon.gameObject.SetActive(false);

        GetUnactiveCannon().currentCannon = true;

    }
}
