using DG.Tweening;
using Quoridor.Controller;
using Quoridor.Model.PlayerLogic;
using Quoridor.View.Audio;
using UnityEngine;
using View.Scripts.UserInterface;

namespace Quoridor.View.UserInterface {
    public sealed class MainMenu : MonoBehaviour
    {
        private const int MainSceneId = 1;
        
        [Header("Button containers")]
        [SerializeField] private ButtonContainer _firstButtonContainer;
        [SerializeField] private ButtonContainer _secondButtonContainer;

        [Header("Sound players")]
        [SerializeField] private CertainSoundPlayer _menuMusicPlayer;
        [SerializeField] private CertainSoundPlayer _roundaboutPlayer;

        [Header("Objects")] 
        [SerializeField] private Transform _toBeContinuedImage;
        [SerializeField] private GameObject _greenFiler;
        [SerializeField] private Vector3 _finishPosition;
        [SerializeField] private float _moveDuration;
        
        private void Start()
        {
            _menuMusicPlayer.Play();
        }

        public void PlayButton()
        {
            _firstButtonContainer.Disable();
            _secondButtonContainer.Enable();
        }
        public void ExitButton()
        {
            Application.Quit();
        }
        public void ChooseGameMode(int gameMode)
        {
            GameModeTransmitter.GameMode = (GameMode) gameMode;

            _toBeContinuedImage.DOLocalMove(_finishPosition, _moveDuration);
            LoadSceneAsync();
            
            _menuMusicPlayer.Stop();
            
            _greenFiler.SetActive(true);
        }
        public void BackButton()
        {
            _firstButtonContainer.Enable();
            _secondButtonContainer.Disable();
        }

        private void LoadSceneAsync()
        {
            AsyncSceneLoader asyncSceneLoader = new AsyncSceneLoader();
            StartCoroutine(asyncSceneLoader.LoadSceneRoutine(MainSceneId));

            _roundaboutPlayer.ExecuteAfterSound(() => asyncSceneLoader.SetLoadPermission(true));
        }
    }
}
