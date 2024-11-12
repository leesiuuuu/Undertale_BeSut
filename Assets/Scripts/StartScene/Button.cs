using UnityEngine;

public class Button : MonoBehaviour
{
    public Sprite SeleteBtn;
    public Sprite Btn;
    private bool isPressed = false;
    
    public void SeleteOn()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = SeleteBtn;
    }
    public void SeleteOff()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Btn;
    }
    public void SetPressed(bool value)
    {
        isPressed = value;
    }
    public bool GetPressed()
    {
        return isPressed;
    }
}
