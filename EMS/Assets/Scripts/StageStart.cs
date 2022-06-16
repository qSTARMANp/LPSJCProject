using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStart : MonoBehaviour
{
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
    GameObject target;

    private float speed_move = 3.0f;
    private float speed_rota = 2.0f;
    public GameObject mainCamera;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public CanvasGroup BackgroundImageCanvasGroup;
    public float speed = 0.5f;
    float m_Timer;

    private void OnMouseDown()
    {
        Debug.Log("마우스클릭");
        StartCoroutine(Run(1f));
        //StartLevel(BackgroundImageCanvasGroup);
        target = GameObject.Find("Targets").transform.GetChild(0).gameObject;
        SetTarget(target);
        StartCoroutine(FadeOut(BackgroundImageCanvasGroup, fadeDuration));
        Vector3 targetPosition;
        targetPosition = new Vector3(boundsData.center.x, boundsData.center.y + boundsData.size.y, boundsData.center.z - boundsData.size.z + Zoomin);

        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition, ref velocity, smoothTime);
        mainCamera.transform.LookAt(targetObject);
    }

    IEnumerator Delay()
    {
        
        yield return new WaitForSeconds(3);
    }

    IEnumerator Run(float duration)
    {
        var runTime = 0.0f;

        while (runTime < duration)
        {
            runTime += Time.deltaTime;

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, transform.position, runTime / duration);
            yield return null;
        }
    }

    void StartLevel(CanvasGroup imageCanvasGroup)
    {
        m_Timer += Time.deltaTime;

        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {

            SceneManager.LoadScene(1);

        }
    }

    public IEnumerator FadeIn(CanvasGroup imageCanvasGroup, float time)
    {
        while (imageCanvasGroup.alpha > 0f)
        {
            imageCanvasGroup.alpha = Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeOut(CanvasGroup imageCanvasGroup, float time)
    {
        while (imageCanvasGroup.alpha < 1f)
        {
            imageCanvasGroup.alpha = time / Time.deltaTime;
            yield return null;
            Debug.Log("페이드 아웃");
        }
        if (time / Time.deltaTime >= 1f)
        {

            SceneManager.LoadScene(1);

        }
    }

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
    }
}
