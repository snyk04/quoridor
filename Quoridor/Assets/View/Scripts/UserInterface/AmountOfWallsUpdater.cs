using System;
using Quoridor.Model.Players;
using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.View.UserInterface
{
    public class AmountOfWallsUpdater : MonoBehaviour
    {
        [SerializeField] private Text _firstPlayerAmountOfWallsCounter;
        [SerializeField] private Text _secondPlayerAmountOfWallsCounter;

        public void UpdateCounter(PlayerType playerType, int amountOfWalls)
        {
            Text counter = playerType switch
            {
                PlayerType.First => _firstPlayerAmountOfWallsCounter,
                PlayerType.Second => _secondPlayerAmountOfWallsCounter,
                _ => throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null)
            };

            counter.text = amountOfWalls.ToString();
        }
    }
}
