using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private GameObject hpBar;
    [SerializeField] private Status status;
    [SerializeField] private Image Gage;

    private Coroutine coroutine;
    private WaitForSeconds wait;
    private float waitTime = 3f;

    private void Awake()
    {
        wait = new WaitForSeconds(waitTime);
    }

    private void Start()
    {
        status.OnHpChanged += UpdateHpBar;
        status.OnDie += HideHpBar;
    }

    private void OnEnable()
    {
        HideHpBar();
    }

    private void OnDestroy()
    {
        status.OnHpChanged -= UpdateHpBar;
        status.OnDie -= HideHpBar;
    }

    private void UpdateHpBar(float currentHp, float maxHp)
    {
        Gage.fillAmount = currentHp / maxHp;

        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(UpdateHpBarCor());
    }

    private IEnumerator UpdateHpBarCor()
    {
        hpBar.SetActive(true);
        yield return wait;
        hpBar.SetActive(false);
    }

    private void HideHpBar()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        hpBar.SetActive(false);
    }
}
