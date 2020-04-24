using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeLoading : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private Text _text;

    [SerializeField]
    private Animator _logoAnimator;

    [SerializeField]
    private List<Text> _texts = new List<Text>();
    [SerializeField]
    private List<Animator> _textAnimators = new List<Animator>();

    [SerializeField]
    private Button _continueButton;
    //[SerializeField]
    //private Animator _buttonAnimator;

    #region MonoBehaviour

    private void Start()
    {
        StartCoroutine(Loading());
    }

    #endregion

    private IEnumerator Loading()
    {

        for (int i = 1; i <= 100; i++)
        {
            _slider.value = i;
            _text.text = $"{i} %";
            yield return new WaitForSeconds(0.05f);
        }

        OnLoadingEnded();

    }

    private void OnLoadingEnded()
    {
        _slider.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);

        _logoAnimator.SetTrigger("StartLogoAnimation");

        foreach (Text text in _texts)
        {
            text.gameObject.SetActive(true);
        }

        foreach (Animator anim in _textAnimators)
        {
            anim.SetTrigger("Text");
        }

        _continueButton.gameObject.SetActive(true);
    }


}
