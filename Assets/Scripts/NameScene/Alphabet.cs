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
    }
    public void SetSeleteCheck()
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
    public int GetID()
    {
        return id;
    }
    public bool GetSelete()
    {
        return Selete;
    }
    public void AlphabetChange(bool Big)
    {
        //대문자 -> 소문자
        if (Big)
        {
            id = (char)(id + 32);
        }
        //소문자 -> 대문자
        else
        {
            id = (char)(id - 32);
        }
    }
    public void UpdateAlphabet()
    {
        _this.GetComponent<TMP_Text>().text = id.ToString();
    }
}
