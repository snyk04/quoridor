using Quoridor.Model.PlayerLogic;
using UnityEngine;
using View.Scripts.UserInterface;

namespace Quoridor.View.UserInterface {
    public class MainMenu : MonoBehaviour
    {
        private const int MainSceneId = 1;
        
        [Header("Button containers")]
        [SerializeField] private ButtonContainer _firstButtonContainer;
        [SerializeField] private ButtonContainer _secondButtonContainer;

        [Header("Sound players")]
        [SerializeField] private SoundPlayer _menuMusicPlayer;
        [SerializeField] private SoundPlayer _roundaboutPlayer;

        [Header("Objects")] 
        [SerializeField] private Transform _toBeContinuedImage;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _finishPosition;
        [SerializeField] private float _time;
        
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

            StartCoroutine(ObjectMover.Move(_toBeContinuedImage, _startPosition, _finishPosition, _time));
            LoadSceneAsync();
            
            _menuMusicPlayer.Stop();
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
