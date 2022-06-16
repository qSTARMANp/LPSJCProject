using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStart : MonoBehaviour
{
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
        Debug.Log("���콺Ŭ��");
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
            Debug.Log("���̵� �ƿ�");
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
    }
}
