using UnityEngine;
using TMPro;

public class Alphabet : MonoBehaviour
{
    private char id;
    private GameObject obj;

    private GameObject _this;

    private bool Selete = false;

    public void AlphabetInit(int id, GameObject obj)
    {
        this.id = (char)id;
        this.obj = obj;
    }
    public void CreateAlphabet(GameObject Parent, GameObject _object)
    {
        _object.transform.SetParent(Parent.transform);
        _object.GetComponent<RectTransform>().localScale = Vector3.one;
        _object.name = ((char)id).ToString();
        _this = _object;
        SetAlphabet(id, _object);
    }
    private void SetAlphabet(char alp, GameObject clone)
    {
        TMP_Text t = clone.GetComponent<TMP_Text>();
        t.text = alp.ToString();
    }

    public void SetSelete(bool value)
    {
        Selete = value;
        SetSeleteCheck();
    }
    private void SetSeleteCheck()
    {
        if (Selete)
        {
            _this.GetComponent<TMP_Text>().color = Color.yellow;
        }
        else
        {
            _this.GetComponent<TMP_Text>().color = Color.white;
        }
    }
    public void InputAlphabet(TMP_Text NameText)
    {
        NameText.text += id.ToString();
    }
    public void DeleteAlphabet(TMP_Text NameText)
    {
        NameText.text = NameText.text.Remove(NameText.text.Length);
    }
    public int GetID()
    {
        return id;
    }
}
