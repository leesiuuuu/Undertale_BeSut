using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Quitting : MonoBehaviour
{
    public Text text;
    public IEnumerator Quit()
    {
        while (gameObject.activeSelf)
        {
            text.text = "QUITTING.";
            yield return new WaitForSeconds(0.2f);
            text.text = "QUITTING..";
            yield return new WaitForSeconds(0.2f);
            text.text = "QUITTING...";
            yield return new WaitForSeconds(0.2f);
        }
    }
}
