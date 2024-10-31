using UnityEngine;

public class ShieldMove : JustMove
{
    public GameObject Shield;
    private Vector2 Locate;
    private float Rotate;
    private void Start()
    {
        Locate = new Vector2(0.2f, 0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Locate = new Vector2(0.2f, 0f);
            Rotate = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Locate = new Vector2(0, 0.2f);
            Rotate = -90f;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            Locate = new Vector2(0, -0.2f);
            Rotate = -90f;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Locate = new Vector2(-0.2f, 0);
            Rotate = 0;
        }
        Shield.transform.localPosition = Locate;
        Shield.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Rotate));
    }
}
