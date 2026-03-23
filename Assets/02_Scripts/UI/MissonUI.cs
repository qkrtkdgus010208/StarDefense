using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissonUI : MonoBehaviour
{
    [Header("MissonEnemyDatas")]
    [SerializeField] private EnemyData misson01EnemyData;
    [SerializeField] private EnemyData misson02EnemyData;
    [SerializeField] private EnemyData misson03EnemyData;

    [Header("Buttons")]
    [SerializeField] private Button misson01Btn;
    [SerializeField] private Button misson02Btn;
    [SerializeField] private Button misson03Btn;
    [SerializeField] private Button closeBtn;

    [Header("Title Texts")]
    [SerializeField] private TextMeshProUGUI misson01TitleText;
    [SerializeField] private TextMeshProUGUI misson02TitleText;
    [SerializeField] private TextMeshProUGUI misson03TitleText;

    [Header("HP Texts")]
    [SerializeField] private TextMeshProUGUI misson01HpText;
    [SerializeField] private TextMeshProUGUI misson02HpText;
    [SerializeField] private TextMeshProUGUI misson03HpText;

    [Header("Reward Texts")]
    [SerializeField] private TextMeshProUGUI misson01RewardText;
    [SerializeField] private TextMeshProUGUI misson02RewardText;
    [SerializeField] private TextMeshProUGUI misson03RewardText;

    [Header("etc")]
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private Image cooldownImg;
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private float cooldownTime = 60f;

    private Coroutine cooldownCor;
    private float cooldownEndTime;
    private bool isCooldownActive = false;

    private void Start()
    {
        misson01Btn.onClick.AddListener(OnMisson01BtnClicked);
        misson02Btn.onClick.AddListener(OnMisson02BtnClicked);
        misson03Btn.onClick.AddListener(OnMisson03BtnClicked);
        closeBtn.onClick.AddListener(CloseBtnClicked);

        cooldownImg.gameObject.SetActive(false);

        UpdateView();
    }

    private void OnDestroy()
    {
        misson01Btn.onClick.RemoveListener(OnMisson01BtnClicked);
        misson02Btn.onClick.RemoveListener(OnMisson02BtnClicked);
        misson03Btn.onClick.RemoveListener(OnMisson03BtnClicked);
        closeBtn.onClick.RemoveListener(CloseBtnClicked);
    }

    private void OnEnable()
    {
        if (isCooldownActive)
        {
            if (Time.time >= cooldownEndTime)
            {
                EndCooldown();
            }
            else
            {
                if (cooldownCor != null) StopCoroutine(cooldownCor);
                cooldownCor = StartCoroutine(CooldownRoutine());
            }
        }
    }

    private void OnMissonClicked()
    {
        cooldownEndTime = Time.time + cooldownTime;
        isCooldownActive = true;

        if (cooldownCor != null) StopCoroutine(cooldownCor);
        cooldownCor = StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        cooldownImg.gameObject.SetActive(true);
        SetButtonsInteractable(false);

        float timer = cooldownTime;

        while (Time.time < cooldownEndTime)
        {
            float remainingTime = cooldownEndTime - Time.time;

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            cooldownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return null;
        }

        EndCooldown();
    }

    private void EndCooldown()
    {
        isCooldownActive = false;
        cooldownImg.gameObject.SetActive(false);
        SetButtonsInteractable(true);
        cooldownCor = null;
    }

    private void OnMisson01BtnClicked()
    {
        enemyManager.SpawnEnemy(misson01EnemyData);
        OnMissonClicked();
    }

    private void OnMisson02BtnClicked()
    {
        enemyManager.SpawnEnemy(misson02EnemyData);
        OnMissonClicked();
    }

    private void OnMisson03BtnClicked()
    {
        enemyManager.SpawnEnemy(misson03EnemyData);
        OnMissonClicked();
    }

    private void UpdateView()
    {
        misson01TitleText.text = misson01EnemyData.enemyName;
        misson02TitleText.text = misson02EnemyData.enemyName;
        misson03TitleText.text = misson03EnemyData.enemyName;
        misson01HpText.text = $"HP {misson01EnemyData.hp}";
        misson02HpText.text = $"HP {misson02EnemyData.hp}";
        misson03HpText.text = $"HP {misson03EnemyData.hp}";
        misson01RewardText.text = $"+{misson01EnemyData.rewardGold}G";
        misson02RewardText.text = $"+{misson02EnemyData.rewardGold}G";
        misson03RewardText.text = $"+{misson03EnemyData.rewardGold}G";
    }

    private void CloseBtnClicked()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void SetButtonsInteractable(bool interactable)
    {
        misson01Btn.interactable = interactable;
        misson02Btn.interactable = interactable;
        misson03Btn.interactable = interactable;
    }
}
