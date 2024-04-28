
using UnityEngine;

public class ChallengeHole : MonoBehaviour
{
	public bool hasScrew;

	public int holeTypeId;

	public SpriteRenderer holeSpr;

	public Color currentColor;
    public bool checkholehidescrew;

    public NailChallenge nail_challenge;
    public GameObject newObject;

    public void Setup(int i)
    {
        if (checkholehidescrew)
        {
            nail_challenge = null;
            return;
        }
        if (newObject == null)
        {
            newObject = Instantiate(GamePlayChallenge.Instance.PrefabNailChallenge, transform.position, Quaternion.identity);
        }
        else
        {
            newObject.transform.position = transform.position;
        }
        NailChallenge nailChallenge = newObject.GetComponent<NailChallenge>();
        nailChallenge.holeNail = this;
       // nailChallenge.ResetImage();
        int sothutuid = GamePlayChallenge.Instance.holeTypeIdsRandom[i];
        if (holeTypeId == sothutuid)
        {
            GamePlayChallenge.Instance.NumberNotDestroy--;
        }
        Color fadedColor = Color.Lerp(GamePlayChallenge.Instance.GamePlayMain.colorMain[sothutuid], Color.white, 0.15f);
        nailChallenge.spriteRenderer.color = fadedColor;
        nailChallenge.id = sothutuid;
        nail_challenge = nailChallenge;
    }
    public void ActiveWhenDown()
    {
        if(GamePlayChallenge.Instance.TargetNailChallenge == nail_challenge)
        {
            if(GamePlayChallenge.Instance.TargetNailChallenge != null)
            {
                GamePlayChallenge.Instance.TargetNailChallenge.ResetImageNail();
                GamePlayChallenge.Instance.TargetNailChallenge = null;
            }
            return;
        }

        if (GamePlayChallenge.Instance.TargetNailChallenge != null && nail_challenge == null)
        {
            if(holeTypeId == GamePlayChallenge.Instance.TargetNailChallenge.id)
            {
                if(GamePlayChallenge.Instance.TargetNailChallenge.holeNail.holeTypeId!= holeTypeId)
                {
                    GamePlayChallenge.Instance.NumberNotDestroy--;
                }
            }
            else
            {
                if (GamePlayChallenge.Instance.TargetNailChallenge.holeNail.holeTypeId == GamePlayChallenge.Instance.TargetNailChallenge.id)
                {
                    GamePlayChallenge.Instance.NumberNotDestroy++;
                }
            }
            GamePlayChallenge.Instance.TargetNailChallenge.transform.position = transform.position;
            GamePlayChallenge.Instance.TargetNailChallenge.ResetImageNail();
            GamePlayChallenge.Instance.TargetNailChallenge.holeNail.nail_challenge = null;
            GamePlayChallenge.Instance.TargetNailChallenge.holeNail = this; //cai nay phai sau dong tren khong loi
            nail_challenge = GamePlayChallenge.Instance.TargetNailChallenge;
            GamePlayChallenge.Instance.TargetNailChallenge = null;
            GamePlayChallenge.Instance.CheckWin();
        }
        else
        {

            if (nail_challenge != null)
            {
                if (GamePlayChallenge.Instance.TargetNailChallenge != null)
                {
                    GamePlayChallenge.Instance.TargetNailChallenge.ResetImageNail();
                }
                GamePlayChallenge.Instance.TargetNailChallenge = nail_challenge;
                nail_challenge.ActiveImageNail();
            }
        }

    }
}
