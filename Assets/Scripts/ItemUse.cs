using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ItemUse : MonoBehaviour
{
    /// <summary>
    /// ������ ��� �� ���� �α׸� ������ְ� ������ ȿ���� ��������ִ� Ŭ����
    /// </summary>
    public void ItemUsed(TMP_Text Ttext, GameObject ItemAndAct, List<string> ItemList, string ItemName, GameObject Heart, int index)
    {
        Heart.SetActive(false);
        ItemAndAct.SetActive(false);
        Ttext.text = "";
        int HPPercetage = 0;
        switch (ItemName)
        {
            case "�ڿ��� Ŀ��":
                HPPercetage = 30;
                break;
            case "���� ������ũ":
                HPPercetage = 50;
                break;
            case "���� �Ҵ�":
                HPPercetage = 40;
                break;
            case "���� �꽺":
                HPPercetage = 30;
                break;
            case "Ư�� ��-��ũ":
                HPPercetage = 80;
                break;
        }
        string ItemLog = "* ����� " + ItemName + "��(��) ����ߴ�!\n  HP�� " + HPPercetage +"��ŭ �����ߴ�!";
        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, ItemLog);
        PlayerManager.instance.HP += HPPercetage;
        PlayerManager.instance.HPChanged();
        ItemList.RemoveAt(index);
    }
}
