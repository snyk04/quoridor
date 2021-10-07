﻿using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.Controller.Buttons
{
    [RequireComponent(typeof(Button))]
    public class CellButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ControllerCommunication _controller;
        
        [Header("Settings")]
        [SerializeField] private Vector2Int _cellCoordinates;

        public void NotifyController()
        {
            _controller.ChooseCell(_cellCoordinates);
        }
    }
}
