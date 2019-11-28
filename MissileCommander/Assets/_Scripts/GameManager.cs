﻿/* ===================================================================================
 *  GameManager will work as composition root.
 *  Assigned Jobs :
 *     1. Create instance of a BulletLauncher from the prefab.
 *     2. Create instance of any GameController Class and assign it to BulletLauncher.
 * ===================================================================================*/

using System;
using UnityEngine;

namespace MissileCommander
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BulletLauncher launcherPrefab;
        private BulletLauncher _launcher;

        private void Awake()
        {
            _launcher = Instantiate(launcherPrefab);
            _launcher.SetGameController(new MouseGameController());
        }
    }
}