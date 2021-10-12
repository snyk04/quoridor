using Quoridor.Model.Players;
using Quoridor.View.Cells;
using UnityEngine;

namespace Quoridor.View
{
    public class VictoryManager : MonoBehaviour
    {
        [SerializeField] private CellHighlighter _cellHighlighter;
        [SerializeField] private SoundPlayer _backgroundMusicPlayer;
        [SerializeField] private SoundPlayer _victoryMusicPlayer;

        public void ShowVictory(PlayerType winner)
        {
            Debug.Log(winner + " won!");
            _cellHighlighter.UnhighlightAllCells();
            _backgroundMusicPlayer.Stop();
            _victoryMusicPlayer.Play();
        }
    }
}
