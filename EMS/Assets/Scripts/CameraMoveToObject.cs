using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̵� �Ϸ�� �߻� �̺�Ʈ
/// </summary>
public class MoveCompleteEventArgs
{
    public GameObject targetObject;
    public Vector3 position;
    public Quaternion quaternion;
}

public class CameraMoveToObject : MonoBehaviour
{
    public static event System.EventHandler<MoveCompleteEventArgs> EventHandler_CameraMoveTargtet;

    /// <summary>
    /// ī�޶�
    /// </summary>
    public GameObject mainCamera;

    /// <summary>
    /// ���� ��� ������Ʈ
    /// </summary>
    private Transform targetObject;

    /// <summary>
    /// ���� ���� ��ġ
    /// </summary>
    public Transform subTarget;

    /// <summary>
    /// �ε巴�� �̵��� ����
    /// </summary>
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    /// <summary>
    /// ī�޶� Ÿ�� ���� ���� �÷���
    /// </summary>
    public static bool IsActive = false;

    /// <summary>
    /// ���� ���� -�� Ŭ���� �ܾƿ�
    /// </summary>
    public float Zoomin = -5;

    /// <summary>
    /// ������Ʈ ũ�⿡ ���� �ܱ�� ���� ���� ������
    /// </summary>
    private Bounds boundsData;
    private bool isBounds = true;

    /// <summary>
    /// �������� ���� �� ���� ī��Ʈ
    /// </summary>
    private int PassCount = 0;
    // Update is called once per frame

    private int childIndex = 0;
    GameObject target;

    private float speed_move = 3.0f;
    private float speed_rota = 2.0f;

    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            if (childIndex == 2)
            {
                childIndex = 0;
            }
            target = GameObject.Find("Targets").transform.GetChild(childIndex++).gameObject;
            SetTarget(target);

        }
        moveObjectFunc();
        if (IsActive)
        {
            Vector3 targetPosition;

            //���������� �ִٸ� ��ǥ������ �����켱���� �����Ѵ�
            if (subTarget != null && PassCount == 0)
            {
                targetPosition = subTarget.transform.position;
                smoothTime = 0.1f;
            }
            else
            {
                //���������� ���ٸ� bounds üũ �� ��ǥ������ �������� �����Ѵ�
                if (!isBounds)
                    targetPosition = targetObject.TransformPoint(new Vector3(0, 10, Zoomin));
                else
                    targetPosition = new Vector3(boundsData.center.x, boundsData.center.y + boundsData.size.y, boundsData.center.z - boundsData.size.z + Zoomin);
            }

            //������ ������ ��ġ�� �ε巴�� �̵�
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition, ref velocity, smoothTime);
            mainCamera.transform.LookAt(targetObject);

            //��ǥ���� �̳��� ����
            if (Vector3.Distance(targetPosition, mainCamera.transform.position) < 0.1f)
            {
                //���������� ���� ���
                if (subTarget != null)
                {
                    //if(Vector3.Distance(targetPosition,subTarget.transform.position) < 0.1f)
                    if (targetPosition == subTarget.transform.position)
                    {
                        PassCount++;

                        //�������� ���� �� targetPosition�� ���� ��ǥ��ġ�� ����
                        if (!isBounds)
                            targetPosition = targetObject.TransformPoint(new Vector3(0, 10, Zoomin));
                        else
                            targetPosition = new Vector3(boundsData.center.x, boundsData.center.y + boundsData.size.y, boundsData.center.z - boundsData.size.z + Zoomin);
                    }
                    else
                    {
                        //�����ϰ� ���� ������ ���������� �̺�Ʈ ó��

                        MoveCompleteEventArgs args = new MoveCompleteEventArgs();
                        args.targetObject = targetObject.gameObject;
                        args.position = mainCamera.transform.position;
                        args.quaternion = mainCamera.transform.rotation;
                        //EventHandler_CameraMoveTargtet(this, args);

                        Clear();
                    }
                }
                else
                {
                    //���� ���� ���� ���� ������ ���������� �̺�Ʈ ó��
                    MoveCompleteEventArgs args = new MoveCompleteEventArgs();
                    args.targetObject = targetObject.gameObject;
                    args.position = mainCamera.transform.position;
                    args.quaternion = mainCamera.transform.rotation;
                    //EventHandler_CameraMoveTargtet(this, args);

                    Clear();
                }
            }
        }

        //if (Input.GetMouseButton(0) && IsActive || Input.GetMouseButton(1) && IsActive || Input.GetAxis("Mouse ScrollWheel") != 0 && IsActive)
        //{
        //    Clear();
        //}

    }

    /// <summary>
    /// target ������Ʈ�� ������ �̵��ϴ� �Լ�
    /// </summary>
    /// <param name="target">��ǥ ������Ʈ</param>
    /// <param name="bounds">������Ʈ ũ�⿡ ���� ���� ����</param>
    public void SetTarget(GameObject target, bool bounds = true)
    {
        if (target == null)
            return;
        IsActive = true;
        targetObject = target.transform;

        //bounds�� true�ϰ�� target�� bounds �����͸� ����
        if (bounds)
        {
            Bounds combinedBounds = new Bounds();
            var renderers = target.GetComponentsInChildren<Renderer>();
            foreach (var render in renderers)
            {
                combinedBounds.Encapsulate(render.bounds);
            }

            boundsData = combinedBounds;
            isBounds = true;
        }
        else
        {
            boundsData = new Bounds();
            isBounds = false;
        }

    }

    public void Clear()
    {
        smoothTime = 0.3f;
        IsActive = false;
        targetObject = null;
        PassCount = 0;
    }

    void moveObjectFunc()
    {
        /*float keyH = Input.GetAxis("Horizontal");
        float keyV = Input.GetAxis("Vertical");
        keyH = keyH * speed_move * Time.deltaTime;
        keyV = keyV * speed_move * Time.deltaTime;
        transform.Translate(Vector3.right * keyH);
        transform.Translate(Vector3.forward * keyV);*/

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.up * speed_rota * mouseX);
        transform.Rotate(Vector3.left * speed_rota * mouseY);
    }

}
