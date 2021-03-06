using DG.Tweening;
using Quoridor.Model.PlayerLogic;
using Quoridor.View.Audio;
using Quoridor.View.Cells;
using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.View
{
    public sealed class VictoryManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ViewCommunication _view;
        [SerializeField] private CertainSoundPlayer _backgroundMusicPlayer;
        [SerializeField] private CertainSoundPlayer _victoryMusicPlayer;

        [Header("Objects")] 
        [SerializeField] private GameObject _background;
        [SerializeField] private GameObject _container;
        [SerializeField] private Text _winnerText;

        [Header("Settings")]
        [SerializeField] private Vector3 _finishPosition;
        [SerializeField] private float _moveDuration;
        
        public void ShowVictory(PlayerColor winner)
        {
            _view.CellHighlighter.UnhighlightAllCells();
            
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

            _container.transform.DOLocalMove(_finishPosition, _moveDuration);
        }
    }
}
