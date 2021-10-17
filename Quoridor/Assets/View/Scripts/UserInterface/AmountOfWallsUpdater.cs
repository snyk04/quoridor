using System;
using Quoridor.Model.PlayerLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.View.UserInterface
{
    public sealed class AmountOfWallsUpdater : MonoBehaviour
    {
        [SerializeField] private Text _firstPlayerAmountOfWallsCounter;
        [SerializeField] private Text _secondPlayerAmountOfWallsCounter;

        public void UpdateCounter(Player player)
        {
            Text counter = player.Type switch
            {
                PlayerType.First => _firstPlayerAmountOfWallsCounter,
                PlayerType.Second => _secondPlayerAmountOfWallsCounter,
                _ => throw new ArgumentOutOfRangeException(nameof(player.Type), player.Type, null)
            };

            counter.text = player.AmountOfWalls.ToString();
        }
    }
}
