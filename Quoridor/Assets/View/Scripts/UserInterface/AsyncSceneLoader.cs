using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View.Scripts.UserInterface
{
    public class AsyncSceneLoader
    {
        private AsyncOperation _asyncLoad;
        
        public IEnumerator LoadSceneRoutine(int sceneID)
        {
            _asyncLoad = SceneManager.LoadSceneAsync(sceneID);
            SetLoadPermission(false);

            while (!_asyncLoad.isDone)
            {
                yield return null;
            }
        }
        public void SetLoadPermission(bool isAllowed)
        {
            _asyncLoad.allowSceneActivation = isAllowed;
        }
    }
}