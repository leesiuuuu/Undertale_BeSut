using UnityEngine;

public class Description : MonoBehaviour
{
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite DescriptionImage;
    public string GetDescription()
    {
        return description;
    }
    public void SetDescription(string description)
    {
        this.description = description;
    }
    public Sprite GetDescriptionImage()
    {
        return DescriptionImage;
    }
    public void SetDescritpionImage(Sprite image)
    {
        DescriptionImage = image;
    }
}
