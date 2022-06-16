using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이동 완료시 발생 이벤트
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
    /// 카메라
    /// </summary>
    public GameObject mainCamera;

    /// <summary>
    /// 줌인 대상 오브젝트
    /// </summary>
    private Transform targetObject;

    /// <summary>
    /// 경유 지점 위치
    /// </summary>
    public Transform subTarget;

    /// <summary>
    /// 부드럽게 이동될 감도
    /// </summary>
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    /// <summary>
    /// 카메라 타겟 줌인 상태 플래그
    /// </summary>
    public static bool IsActive = false;

    /// <summary>
    /// 줌인 정도 -가 클수록 줌아웃
    /// </summary>
    public float Zoomin = -5;

    /// <summary>
    /// 오브젝트 크기에 맞춰 줌기능 사용시 사용될 데이터
    /// </summary>
    private Bounds boundsData;
    private bool isBounds = true;

    /// <summary>
    /// 경유지점 도착 후 진행 카운트
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

            //경유지점이 있다면 목표지점을 경유우선으로 지정한다
            if (subTarget != null && PassCount == 0)
            {
                targetPosition = subTarget.transform.position;
                smoothTime = 0.1f;
            }
            else
            {
                //경유지점이 없다면 bounds 체크 후 목표지점을 종착지로 설정한다
                if (!isBounds)
                    targetPosition = targetObject.TransformPoint(new Vector3(0, 10, Zoomin));
                else
                    targetPosition = new Vector3(boundsData.center.x, boundsData.center.y + boundsData.size.y, boundsData.center.z - boundsData.size.z + Zoomin);
            }

            //위에서 설정된 위치로 부드럽게 이동
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition, ref velocity, smoothTime);
            mainCamera.transform.LookAt(targetObject);

            //목표지점 이내에 도착
            if (Vector3.Distance(targetPosition, mainCamera.transform.position) < 0.1f)
            {
                //경유지점이 있을 경우
                if (subTarget != null)
                {
                    //if(Vector3.Distance(targetPosition,subTarget.transform.position) < 0.1f)
                    if (targetPosition == subTarget.transform.position)
                    {
                        PassCount++;

                        //경유지점 도착 후 targetPosition을 최종 목표위치로 변경
                        if (!isBounds)
                            targetPosition = targetObject.TransformPoint(new Vector3(0, 10, Zoomin));
                        else
                            targetPosition = new Vector3(boundsData.center.x, boundsData.center.y + boundsData.size.y, boundsData.center.z - boundsData.size.z + Zoomin);
                    }
                    else
                    {
                        //경유하고 최종 목적지 도착했을때 이벤트 처리

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
                    //경유 지점 없이 최종 목적지 도착했을때 이벤트 처리
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
    /// target 오브젝트로 시점을 이동하는 함수
    /// </summary>
    /// <param name="target">목표 오브젝트</param>
    /// <param name="bounds">오브젝트 크기에 따라 줌인 여부</param>
    public void SetTarget(GameObject target, bool bounds = true)
    {
        if (target == null)
            return;
        IsActive = true;
        targetObject = target.transform;

        //bounds가 true일경우 target의 bounds 데이터를 저장
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
