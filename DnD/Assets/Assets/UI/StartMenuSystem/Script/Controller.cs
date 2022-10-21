using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI.StartMenu
{
    public class Controller : MonoBehaviour
    {
        private StartMenuUI _startmenu;
        private StoryMenuUI _story;
        private Scroll _scroll;
        CreateStoryMenuUI _createStoryMenuAnimation;
        private FadeUI _fade;

        private void Start()
        {
            _startmenu = GetComponentInChildren<StartMenuUI>();
            _story = GetComponentInChildren<StoryMenuUI>();
            _scroll = GetComponentInChildren<Scroll>();
            _createStoryMenuAnimation = GetComponentInChildren<CreateStoryMenuUI>();
            _fade = GetComponentInChildren<FadeUI>();
        }
        public void ClickGame()
        {
            StartCoroutine(CR_ClickGame());
        }

        public void OpenSceneCreateStory()
        {
            StartCoroutine(CR_OpenSceneCreateStory());
        }

        private IEnumerator CR_ClickGame()
        {
            _startmenu.CloseMenu();
            yield return new WaitForSeconds(1.5f);
            _story.Open();
            _scroll.Create();
        }

        private IEnumerator CR_OpenSceneCreateStory()
        {
            _createStoryMenuAnimation.ClosePanel();
            yield return new WaitForSeconds(0.5f);
            _fade.Open();
            yield return new WaitForSeconds(1.1f);
            StartCoroutine(SceneControl.CR_NextScene("CreateStory", _fade.Slider));
        }
    }
}

public class SceneControl
{
    public static IEnumerator CR_NextScene(string name, Slider slider)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(name);
        while (!oper.isDone)
        {
            float progress = oper.progress / 0.9f;
            slider.value = progress;
            yield return null;
        }
    }
}
