using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneUIController : MonoBehaviour
{
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _commentator;
    [SerializeField] private GameObject _dialog;
    [SerializeField] private Animator _mainCanvasAnim;
    [SerializeField] private Animator _commentatorAnim;
    [SerializeField] private Animator _dialogAnim;

    private static readonly int Disappear = Animator.StringToHash("Disappear");
    private static readonly int Appear = Animator.StringToHash("Appear");

    public void OnGameInfoOpen()
    {
        _mainCanvasAnim.SetTrigger(Disappear);
        _commentator.SetActive(true);
        _dialog.SetActive(true);
    }

    public void OnGameInfoClose()
    {
        _mainCanvasAnim.SetTrigger(Appear);
        _commentatorAnim.SetTrigger(Disappear);
        _dialogAnim.SetTrigger(Disappear);
        StartCoroutine(CloseDefaultUI(_commentator, 0.8f));
        StartCoroutine(CloseDefaultUI(_dialog, 0.8f));
    }

    IEnumerator CloseDefaultUI(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
