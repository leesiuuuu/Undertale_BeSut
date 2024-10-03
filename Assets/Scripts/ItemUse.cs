using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ItemUse : MonoBehaviour
{
    /// <summary>
    /// 아이템 사용 시 관련 로그를 출력해주고 아이템 효과를 적용시켜주는 클래스
    /// </summary>
    public void ItemUsed(TMP_Text Ttext, GameObject ItemAndAct, List<string> ItemList, string ItemName, GameObject Heart, int index)
    {
        Heart.SetActive(false);
        ItemAndAct.SetActive(false);
        Ttext.text = "";
        int HPPercetage = 0;
        switch (ItemName)
        {
            case "자연산 커피":
                HPPercetage = 20;
                break;
            case "마법 롤케이크":
                HPPercetage = 40;
                break;
            case "마법 소다":
                HPPercetage = 30;
                break;
            case "뮤즈 룰스":
                HPPercetage = 15;
                break;
            case "특제 케-이크":
                HPPercetage = 60;
                break;
        }
        string ItemLog = "* 당신은 " + ItemName + "을(를) 사용했다!\n  HP가 " + HPPercetage +"만큼 증가했다!";
        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, ItemLog);
        PlayerManager.instance.HP += HPPercetage;
        PlayerManager.instance.HPChanged();
        ItemList.RemoveAt(index);
    }
}
