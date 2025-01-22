using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset = Vector3.zero;
    private bool isHeld = false;

    // ����������� ���������� ��� ������������ ������������� ��������
    private static InteractableObject currentHeldObject = null;

    public void ToggleHold(Transform newParent)
    {
        if (isHeld)
        {
            // ���� ��� ������������, ������ �������� ���������
            ReleaseObject();
        }
        else
        {
            // ���� ������� ��� ������������, �� ���� ����������� ����� �����
            if (currentHeldObject != null)
            {
                return;
            }

            // ������������� ������� ������������ �������
            currentHeldObject = this;

            isHeld = true;
            // ��������� ������
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(newParent); // ������������� �������� �� ������
            transform.localPosition = cameraOffset; // ������������� ����� �������
        }
    }

    private void ReleaseObject()
    {
        // ����������� �������
        isHeld = false;
        GetComponent<Rigidbody>().isKinematic = false; // �������� ������
        transform.SetParent(null); // ������� ������������ ������

        // ���������� ������� ������������ �������
        currentHeldObject = null;
    }
}
