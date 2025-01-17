using TMPro;
using UnityEngine;

public class NameInput : MonoBehaviour
{
    private int MAX_VALUE = 6;
    private TMP_Text t;
    private void Awake()
    {
        t = GetComponent<TMP_Text>();
        t.text = "";
    }
    public void InputAlphabet(char id)
    {
        if (t.text.Length >= MAX_VALUE) return;
        t.text += id.ToString();
    }
    public void DeleteAlphabet()
    {
        if (t.text.Length <= 0) return;
        t.text = t.text.Remove(t.text.Length-1);
    }
    public string GetNameField()
    {
        return t.text;
    }
}
