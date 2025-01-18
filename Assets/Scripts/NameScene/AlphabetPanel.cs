using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AlphabetPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject ab;
    [SerializeField]
    private NameInput _InputField;
    [SerializeField]
    private TMP_Text _Confirm;
    [SerializeField]
    private TMP_Text _ChangeAlphbet;

    private Alphabet[,] alphabets = new Alphabet[9,3];

    private bool SeleteMod = false;

    private bool Confirm = false;
    private bool ChangeAlphabet = false;

    private bool once = false;
    int _i = 0, _j = 0;

    private KeyCode _key;

    private void Awake()
    {
        int n = 0;
        //객체 생성
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                if (i == 2 && j == 8) break;
                GameObject Clone = Instantiate(ab);
                Alphabet a = Clone.AddComponent<Alphabet>();
                a.AlphabetInit(n + 65, ab);
                a.CreateAlphabet(gameObject, Clone);
                alphabets[j, i] = a;
                if (i == 0 && j == 0) alphabets[i, j].SetSelete(true);
                n++;
            }
        }
    }
    private void Update()
    {
        if (Input.anyKey && !Confirm && !ChangeAlphabet)
        {
            //선택 모드 활성화 후 첫 글자 제외 선택 상태 초기화
            SeleteMod = true;
        }

        if (SeleteMod)
        {
            UpdateSelete();
            AlphabetInput();
        }
        else
        {
            if (ChangeAlphabet)
            {
                ChangeAlpha();
            }
            else if (Confirm)
            {
                Confirm_();
            }
        }
    }
    private void UpdateSelete()
    {
        if(!Confirm && !ChangeAlphabet)
        {
            alphabets[_i, _j].SetSelete(false);
            alphabets[_i, _j].SetSeleteCheck();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _i--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _i++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _j--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(_j == 2)
            {
                SeleteMod = false;
                if (_i >= 0 && _i < 4)
                {
                    ChangeAlphabet = true;
                    _ChangeAlphbet.color = Color.yellow;
                    return;
                }
                else
                {
                    Confirm = true;
                    _Confirm.color = Color.yellow;
                    return;
                }
            }
            else
            {
                _j++;
            }
        }
        if(!Confirm && !ChangeAlphabet)
        {
            if (_i >= 8 && _j == 2)
            {
                _i = Mathf.Clamp(_i, 0, 7);
            }
            else
            {
                _i = Mathf.Clamp(_i, 0, 8);
            }
            _j = Mathf.Clamp(_j, 0, 2);
            alphabets[_i, _j].SetSelete(true);
            alphabets[_i, _j].SetSeleteCheck();
        }
    }
    private void AlphabetInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _InputField.InputAlphabet((char)alphabets[_i, _j].GetID());
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            _InputField.DeleteAlphabet();
        }
    }
    private void ChangeAlpha()
    {
        int i = 0;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            once = !once;
            foreach (Alphabet alphabet in alphabets)
            {
                i++;
                if (i > 25) return;
                else
                {
                    alphabet.AlphabetChange(once);
                    alphabet.UpdateAlphabet();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeAlphabet = false;
            SeleteMod = true;
            _ChangeAlphbet.color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeAlphabet = false;
            _ChangeAlphbet.color = Color.white;
            Confirm = true;
            _Confirm.color = Color.yellow;
        }
    }
    private void Confirm_()
    {
        if (Input.GetKeyDown(KeyCode.Z) && _InputField.GetNameField().Length > 0)
        {
            PlayerPrefs.SetString("name", _InputField.GetNameField());
            PlayerPrefs.Save();
            SceneManager.LoadScene("SureThisNameScene");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Confirm = false;
            SeleteMod = true;
            _Confirm.color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Confirm = false;
            _Confirm.color = Color.white;
            ChangeAlphabet = true;
            _ChangeAlphbet.color = Color.yellow;
        }
    }

}
