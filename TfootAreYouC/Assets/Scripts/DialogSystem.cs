using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public TMP_Text txtName;
    public TMP_Text txtSentence;

    Queue<string> sentences = new Queue<string>();

    public Animator anim;


    public void Begin(Dialogue Info)
    {
        anim.SetBool("IsOpen", true);

        sentences.Clear();

        txtName.text = Info.name;

        foreach(var sentence in Info.sentences)
        {
            sentences.Enqueue(sentence);
        }

        Next();
    }

    public void Next()
    {
        if(sentences.Count == 0)
        {
            End();
            return;
        }
        //txtSentence.text = sentences.Dequeue();

        txtSentence.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach(var letter in sentence)
        {
            txtSentence.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void End()
    {
        anim.SetBool("IsOpen", false);
        txtSentence.text = string.Empty;
    }
}
