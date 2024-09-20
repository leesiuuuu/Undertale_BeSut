using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxToFightBox : MonoBehaviour
{
    public Vector3 StartScale; //시작 위치
    public Vector3 EndScale; //도착 위치 
    public float Duration; //기간

    private float TimeElapsed = 0f; //진행 시간

    void Update()
    {
        if (TimeElapsed < Duration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / Duration; // 0에서 1까지의 시간 비율을 계산하는 코드
            t = easeInOutSine(t);
            transform.localScale = Vector3.Lerp(StartScale, EndScale, t); //위치를 선형보간함수로 자연스럽게 이동
        }
    }
    //부드러운 움직임 함수
    float easeInOutSine(float x)
    {   
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }
}
