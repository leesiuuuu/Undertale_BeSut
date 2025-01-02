using UnityEngine;

public class AlphabetPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject ab;

    private Alphabet[] alphabets = new Alphabet[26];

    private bool SeleteMod = false;

    private void Awake()
    {
        for(int i = 0; i < 26; i++)
        {
            GameObject Clone = Instantiate(ab);
            Alphabet a = Clone.AddComponent<Alphabet>();
            a.AlphabetInit(i + 65, ab);
            a.CreateAlphabet(gameObject, Clone);
            alphabets[i] = a;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !SeleteMod)
        {
            SeleteMod = true;
        }
        
    }
    private Alphabet FindAlphabetWithKey(int key)
    {
        foreach(Alphabet a in alphabets)
        {
            if(a.GetID() == key)
            {
                return a;
            }
        }
        Debug.LogWarning($"{key}의 값을 가진 키를 찾을 수 없습니다!");
        return null;
    }
}
