using System;
using Quoridor.Model.PlayerLogic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Quoridor.View.UserInterface
{
    public sealed class AmountOfWallsUpdater : MonoBehaviour
    {
        [SerializeField] private Text _whitePlayerAmountOfWallsCounter;
        [SerializeField] private Text _blackPlayerAmountOfWallsCounter;

        public void UpdateCounter(Player player)
        {
            Text counter = player.Color switch
            {
                PlayerColor.White => _whitePlayerAmountOfWallsCounter,
                PlayerColor.Black => _blackPlayerAmountOfWallsCounter,
                _ => throw new ArgumentOutOfRangeException(nameof(player.Type), player.Type, null)
            };

            counter.text = player.AmountOfWalls.ToString();
        }
    }
}
