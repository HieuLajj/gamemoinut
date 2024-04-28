using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine.Rendering.Universal;
using Sirenix.OdinInspector;

public enum TypeBoom
{
    Move, Timer
}
public class Boom_Item : MonoBehaviour, TInterface<Boom_Item>
{
    public SpriteRenderer spriteBoom;
    public Canvas canvas;
    public ParticleSystem particleBoom;
    private int intTime;
    public int IntTime
    {
        set { 
            intTime = value;
            textTime.text = intTime.ToString();
        }
        get { return intTime;}
    }

    public Sprite[] spritesBoom;

    public TextMeshProUGUI textTime;
    //pool
    private ObjectPool<Boom_Item> _pool;
    public TypeBoom TypeBoom;
    public RectTransform rectTransfomBoomText;
    public void Setup()
    {
        if(TypeBoom == TypeBoom.Move)
        {
            Slot_Item.EventDisActiveNailOther += RegisterEventMove;
            spriteBoom.sprite = spritesBoom[0];
            spriteBoom.transform.localScale = new Vector2(0.56f, 0.56f);
            rectTransfomBoomText.anchoredPosition = new Vector2(0,-0.112f);
        }
        else
        {
            spriteBoom.transform.localScale = new Vector2(0.5f, 0.5f);
            rectTransfomBoomText.anchoredPosition = new Vector2(0, -0.221f);
            spriteBoom.sprite = spritesBoom[1];
            Run();
        }
    }

    private void RegisterEventMove()
    {
        IntTime--;
        if(IntTime <= 0)
        {
            LevelController.Instance.CheckFailureDetail(() =>
            {

                //spriteBoom.gameObject.SetActive(false);
                PlayBoom();
                Timer.instance.Stop();
                Invoke("DelayThatbai", 1);
            });
        }
    }
    [Button()]
    public void PlayBoom()
    {
        canvas.gameObject.SetActive(false);
        float scalefloat = transform.localScale.x;
        transform.DOShakePosition(0.4f, 0.05f, 15, 90, false);

        transform.DOScale(scalefloat+0.25f, 0.5f).OnComplete(EffectBoom);
    }
    private void EffectBoom(){
            spriteBoom.gameObject.SetActive(false);
            particleBoom.gameObject.SetActive(true);
            particleBoom.Play();
    }

    private void DelayThatbai()
    {
        DefaultUI.typeDefault = 1;
        UIEvents.Instance.ShowDefaultUI();
    }

    private void OnEnable()
    {
        Timer.instance.PauseEvent += PauseTime;
        Timer.instance.ResumeEvent += ResumeTime;
    }

    private void OnDisable()
    {
        Timer.instance.PauseEvent -= PauseTime;
        Timer.instance.ResumeEvent -= ResumeTime;
    }

    private void PauseTime()
    {
        Pause();
    }
    public void ResumeTime()
    {
        Resume();
    }

    public void SetPool(ObjectPool<Boom_Item> pool)
    {

        _pool = pool;
    }

    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Boom_Item IGetComponentHieu()
    {
        return this;
    }

    public void ResetAfterRelease()
    {
        if (TypeBoom == TypeBoom.Move)
        {
            Slot_Item.EventDisActiveNailOther -= RegisterEventMove;
        }
        else
        {
            Stop();
            tween.Kill();
            tween = null;
        }
    }

    public void StartCreate()
    {
        canvas.gameObject.SetActive(true);
        particleBoom.gameObject.SetActive(false);
        spriteBoom.gameObject.SetActive(true);
    }

   // public int timeInSeconds;
    private bool isPaused;
    private float timeCounter;
    private int totalTime;
    private float sleepTime;
    public bool runOnStart;
    private Tween tween;
    //time
    public void Run()
    {
     
        this.isPaused = false;
        this.timeCounter = 0f;
        this.sleepTime = 1f;
        this.totalTime = intTime;
        //this.timeInSeconds = this.totalTime;
     
        base.InvokeRepeating("Wait", 0f, this.sleepTime);
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
    }

    public void Resume()
    {
        this.isPaused = false;
    }
    private void Wait()
    {   
        if (!this.isPaused)
        {
            this.timeCounter += this.sleepTime;
            IntTime = this.totalTime - (int)this.timeCounter;       
        }
        if (IntTime <= 0)
        {
            Stop();
            LevelController.Instance.CheckFailureDetail(() =>
            {
                PlayBoom();
                Timer.instance.Stop();
                Invoke("DelayThatbai", 1);
            });
            //checkthua
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        Stop();
        if (TypeBoom == TypeBoom.Timer)
        {
            gameObject.SetActive(false);
        }        
    }
}
