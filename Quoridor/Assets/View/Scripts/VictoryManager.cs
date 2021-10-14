using Quoridor.NewModel.PlayerLogic;
using Quoridor.View.Cells;
using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.View
{
    public class VictoryManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CellHighlighter _cellHighlighter;
        [SerializeField] private SoundPlayer _backgroundMusicPlayer;
        [SerializeField] private SoundPlayer _victoryMusicPlayer;

        [Header("Objects")] 
        [SerializeField] private GameObject _background;
        [SerializeField] private GameObject _container;
        [SerializeField] private Text _winnerText;

        [Header("Settings")]
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _finishPosition;
        [SerializeField] private float _speed;
        
        public void ShowVictory(PlayerType winner)
        {
            _cellHighlighter.UnhighlightAllCells();
            
            HandleMusic();
            HandleUiElements();
            
            _winnerText.text = winner + " player won!";
        }

        private void HandleMusic()
        {
            _backgroundMusicPlayer.Stop();
            _victoryMusicPlayer.Play();
        }

        private void HandleUiElements()
        {
            _background.SetActive(true);
            _container.SetActive(true);

            StartCoroutine(ObjectMover.Move(_container.transform, _startPosition, _finishPosition, _speed));
        }
    }
}
