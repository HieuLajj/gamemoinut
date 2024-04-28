using UnityEngine;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System.Collections;
using System;
public class Timer : MonoBehaviour
{  
    public TextMeshProUGUI uiText;
    public int timeInSeconds;
    private bool isPaused;
    private float timeCounter;
    private int totalTime;
    private float sleepTime;
    private Color timeInitialColor;
    public bool runOnStart;
    public static Timer instance;
    private Tween tween;

    public Action EndTime;

    public Image timerImage;
    private Vector3 TargetScale = new Vector3(1.35f,1.35f,1.35f);
    public Transform PositionStart;
    public ParticleSystem VFXAddTime;
    public Action PauseEvent;
    public Action ResumeEvent;
    private void Awake()
    {
        if (Timer.instance == null)
        {
            Timer.instance = this;
        }
        this.timeInitialColor = this.uiText.color;
        this.Pause();
    }    
    private void OnEnable()
    {
        ControllerHieu.TimeEvent += TimeRemote;
    }

    private void OnDisable()
    {
        ControllerHieu.TimeEvent -= TimeRemote;
        tween.Kill();
        tween = null;
    }


    public void TimeRemote(int a)
    {
        if(a == 1)
        {
            this.Resume();
        }
        else
        {
            this.Pause();
        }
    }


    public void Run()
    {
        if(uiText.gameObject.activeInHierarchy == false)
        {
            uiText.gameObject.SetActive(true);
        }
        this.isPaused = false;
        this.timeCounter = 0f;
        this.sleepTime = 1f;
        this.totalTime = JsonReadLevelConfig.Instance.GetTimer()+1;   
        this.timeInSeconds = this.totalTime;
        base.InvokeRepeating("Wait", 0f, this.sleepTime);
    }
    public void Run(int seconds)
    {
        if (uiText.gameObject.activeInHierarchy == false)
        {
            uiText.gameObject.SetActive(true);
        }
        this.isPaused = false;
        this.timeCounter = 0f;
        this.sleepTime = 1f;
        this.totalTime = seconds;
        this.timeInSeconds = this.totalTime;
        base.InvokeRepeating("Wait", 0f, this.sleepTime);
    }
    public void ResetTextTime()
    {
        uiText.text = TimeString(JsonReadLevelConfig.Instance.GetTimer());
    }
    public void SetTextTime(int total)
    {
        uiText.text = TimeString(total);
    }
    public void Stop()
    {
       
        base.CancelInvoke();
    }

    public void Reset()
    {
        this.Stop();
        this.Run();
    }

    public void Pause()
    {
        this.isPaused = true;
        PauseEvent?.Invoke();
    }

    public void Resume()
    {
        this.isPaused = false;
        ResumeEvent?.Invoke();
    }

    private void Wait()
    {
        if (!this.isPaused)
        {
            this.timeCounter += this.sleepTime;
            this.timeInSeconds = this.totalTime - (int)this.timeCounter;
            this.ApplyTime();         
        }
        if(this.timeInSeconds == 0){
            Stop();
            EndTime?.Invoke();
        }
    }

    private void ApplyTime()
    {
        if (this.uiText == null)
        {
            return;
        }
        int number = this.timeInSeconds / 60;
        int number2 = this.timeInSeconds % 60;
        if (this.timeInSeconds < 11)
        {
            if(tween == null)
            {
                tween = uiText.transform.DOScale(new UnityEngine.Vector3(1.15f, 1.15f, 1.15f), 1f)
               .SetEase(Ease.InOutQuad)
               .SetLoops(-1, LoopType.Yoyo);
            }
            this.uiText.color = Color.red;        

        }
        else
        {
           
            if (tween != null)
            {
                uiText.transform.localScale = UnityEngine.Vector2.one;
                tween.Kill();
                tween = null;
            }
            this.uiText.color = this.timeInitialColor;
        }
        this.uiText.text = Timer.GetNumberWithZeroFormat(number) + ":" + Timer.GetNumberWithZeroFormat(number2);
      //  this.uiText.text = this.timeInSeconds.ToString("000");
    }
    private string TimeString(int TotalTime)
    {
        int number = TotalTime / 60;
        int number2 = TotalTime % 60;
        return GetNumberWithZeroFormat(number) + ":" + GetNumberWithZeroFormat(number2);
    }
    [Button()]
    public void IncreaseTime()
    {
        StartCoroutine(AnimateTimer());
    }
    private IEnumerator AnimateTimer()
    {

        timerImage.transform.position = PositionStart.position;
        timerImage.gameObject.SetActive(true);
        yield return timerImage.transform.DOScale(TargetScale, 0.25f).SetEase(Ease.InOutSine).WaitForCompletion();
        yield return timerImage.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutSine).WaitForCompletion();
        yield return timerImage.transform.DOScale(TargetScale, 0.25f).SetEase(Ease.InOutSine).WaitForCompletion();
        yield return timerImage.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutSine).WaitForCompletion();
        timerImage.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f);
        yield return timerImage.transform.DOMove(uiText.transform.position, 0.75f).SetEase(Ease.InOutSine).WaitForCompletion();
        VFXAddTime.Play();
        timerImage.gameObject.SetActive(false);
        uiText.color = new Color(177f / 255f, 253f / 255f, 164f / 255f, 1);
        uiText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f).SetEase(Ease.InOutSine);

        int timelast = timeInSeconds + HardSupport.Instance.GetTimeScale();
      
        int timefirst = timeInSeconds;

        yield return DOTween.To(() => timefirst, x => timefirst = x, timelast, 1.5f).OnUpdate(() =>
        {
            //uiText.text = "Time: " + timefirst.ToString("000");
            int number = timefirst / 60;
            int number2 = timefirst % 60;
            this.uiText.text = Timer.GetNumberWithZeroFormat(number) + ":" + Timer.GetNumberWithZeroFormat(number2);
        }).WaitForCompletion();

        IncreaseTime(HardSupport.Instance.GetTimeScale());
        Resume();
        uiText.transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.InOutSine);
        uiText.color = timeInitialColor;
    }
    public void IncreaseTime(float factor)
    {     
        this.timeCounter -= factor;
    }

    public static string GetNumberWithZeroFormat(int number)
    {
        string text = string.Empty;
        if (number < 10)
        {
            text += "0";
        }
        return text + number;
    }
    public bool IsPaused()
    {
        return this.isPaused;
    }

    private void OnDestroy()
    {
        base.CancelInvoke();
    }

   
}
