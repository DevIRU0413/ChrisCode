using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMS_UIPopup : MonoBehaviour
{
    [SerializeField] private GameObject popupCanvas; // �˾� â�� ĵ����
    // Start is called before the first frame update

    private void Start()
    {
        Close();
    }
    public void Open()
    {
        if(popupCanvas != null)
        {
            UIUtilities.SetUIActive(popupCanvas, true);
        }
    }

    public void Close()
    {
        if (popupCanvas != null)
        {
            UIUtilities.SetUIActive(popupCanvas, false);
        }
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
