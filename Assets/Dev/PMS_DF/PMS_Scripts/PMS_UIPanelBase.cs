using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIPanel�� �����ϰ�
public class PMS_UIPanelBase : MonoBehaviour
{
    [SerializeField] private GameObject UIpanel; //Ư�� UIPanel�� ���� Ű�� ���ְ� �ϱ�

    //UI��Ҹ� �����ְ� �ϴ� �Լ�
    public void Show()
    {
        UIUtilities.SetUIActive(UIpanel,true);
    }

    //UI��Ҹ� �Ⱥ����ְ� �ϴ� ���
    public void Hide()
    {
        UIUtilities.SetUIActive(UIpanel, false);
    }

    public void OnButtonClicked()
    {
        
    }

}

public static class UIUtilities
{
    // ���� ������Ʈ�� Ȱ��ȭ ���¸� �����ϴ� ��ƿ��Ƽ �Լ�
    public static void SetUIActive(GameObject uiObject, bool isActive)
    {
        if (uiObject != null)
        {
            uiObject.SetActive(isActive);
        }
    }
}
