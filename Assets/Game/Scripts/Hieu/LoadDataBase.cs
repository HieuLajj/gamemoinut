using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public enum TypeCL{
    None,
    Blink,
    Green,
    Blue,
    Violet,
    Yellow
}
public enum TypeCrew{
    None,
    Screw_01
}
[System.Serializable]
public struct Pos
{
    public float x;
    public float y;
    public float z;
}
[System.Serializable]
public struct Scale
{
    public float x;
    public float y;
    public float z;
}
[System.Serializable]
public struct Rot
{
    public float x;
    public float y;
    public float z;
}
[System.Serializable]
public struct ColorHieu
{
    public float r;
    public float g;
    public float b;
    public float a;
}
[System.Serializable]
public struct Ice{
    public bool on;
    public int piece;
}
[System.Serializable]
public struct Crew{
    public TypeCrew TypeScrew;
    public bool isClick;
    public bool isAuto;
    
}
[System.Serializable]
public struct Boxs{
    public TypeCL TypeCL;
    public Crew[] ListCrew;

}
[System.Serializable]
public struct HandTut
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
    public int step;
}
[System.Serializable]
public struct Board
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
    public int layer;
    public string material;
    public int parent;
    public ColorHieu color;
   // public Bomb bomb;
}

[System.Serializable]
public struct Slot
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
    public bool hasNail;
    public bool hasLock;
    public bool hasLockAds;
    public bool hasIce;
    public bool canPutScrew;
    public int iceUnlockNumber;
}
[System.Serializable]
public struct Nail
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
    public int parent;
    public TypeCrew TypeCrew;
    public string material;
    public Ice Ice;
}
[System.Serializable]
public struct Holes{
    public int numbers;
}
[System.Serializable]
public struct Bg
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
}
[System.Serializable]
public struct Txt
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
}
[System.Serializable]
public struct Hint
{
    public Pos pos; 
    public Scale scale;
    public Rot rot;
    public int hintId;
}
[System.Serializable]
public struct Lock
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
    public int layer;
    public ColorHieu color;
}
[System.Serializable]
public struct Key
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
    public int layer;
    //public ColorHieu color;
}
[System.Serializable]
public struct Ad
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
}

[System.Serializable]
public struct Boom
{
    public Pos pos;
    public Scale scale;
    public string type;
    public int Alive;
    public int TimeDeath;
    public int Force;
}


public class RootLevel
{
    public List<Ad_Item> listad;
    public List<Board_Item> listboard;
    public List<Key_Item> listkey;
    public List<HandTut> listHand;
    public List<Boom_Item> listBoom;

    public Bg_Item bgItem;


    // làm theo cơ chế kiểu mới
    public Dictionary<string, Slot_Item> litsslot_mydictionary;
    public Dictionary<string, Lock_Item> litslock_mydictionary;
    public Dictionary<string, Nail_Item> litsnail_mydictionary;
    public RootLevel()
    {      
        litsslot_mydictionary = new Dictionary<string, Slot_Item>();
        litslock_mydictionary = new Dictionary<string, Lock_Item>();
        litsnail_mydictionary = new Dictionary<string, Nail_Item>();

        listad   = new List<Ad_Item>();
        listboard = new List<Board_Item>();
        listkey = new List<Key_Item>();     
        listHand = new List<HandTut>();
        listBoom = new List<Boom_Item> { };
    }
    public void ClearRoot(Action actionafter)
    {    
       
        foreach (Slot_Item slot in litsslot_mydictionary.Values)
        {
            slot.ResetPool();
        }
        foreach (Lock_Item lock_item in litslock_mydictionary.Values)
        {
            lock_item.ResetPool();
        }
        foreach (Nail_Item nail_item in litsnail_mydictionary.Values)
        {
            nail_item.ResetPool();
        }
        for (int i = 0; i < listboard.Count; i++)
        {
            listboard[i].ResetPool();
        }
        for (int i = 0; i < listkey.Count; i++)
        {
            listkey[i].ResetPool();
        }
        for (int i = 0; i < listBoom.Count; i++)
        {
            listBoom[i].ResetPool();
        }
        for (int i = 0; i< listad.Count; i++)
        {
            listad[i].ResetPool();
        }    
        bgItem?.ResetPool();
        bgItem = null;
        listboard?.Clear();    
        litsslot_mydictionary?.Clear();
        litslock_mydictionary?.Clear();
        litsnail_mydictionary?.Clear();
        listad?.Clear();
        listkey?.Clear();      
        listHand?.Clear();
        listBoom?.Clear();
        actionafter?.Invoke();
    }

    public bool Findslotfornail(Nail_Item nail)
    {
        try
        {
            if (!litsslot_mydictionary.ContainsKey(nail.transform.position.ToString())) return false;
            Slot_Item slotItem = litsslot_mydictionary[nail.transform.position.ToString()];
            if (slotItem != null && slotItem.hasNail == true)
            {
                slotItem.nail_item = nail;
                nail.slot_item = slotItem;
                nail.preslot_item = slotItem;
                nail.transform.parent = slotItem.transform;
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return false;
        }
    }

    public bool Findslotforlock(Lock_Item lockitem)
    {
        try
        {
            Slot_Item slotItem = litsslot_mydictionary[lockitem.transform.position.ToString()];
            if (slotItem != null && slotItem.hasLock == true)
            {
                lockitem.transform.parent = slotItem.transform;
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return false;
        }
    }

    //public bool Findsadforlock(Ad_Item aditem)
    //{
    //    Bounds bounds1 = aditem.GetComponent<Collider2D>().bounds;
    //    foreach (Slot_Item slot_item in litsslot_mydictionary.Values)
    //    {
    //        Bounds bounds2 = slot_item.GetComponent<Collider2D>().bounds;
    //        if (bounds1.Intersects(bounds2))
    //        {
    //            aditem.transform.parent = slot_item.transform;
    //            return true;
    //        }
    //    }
    //    return false;
    //}


    public void Findsnailforslot(Slot_Item slot)
    {
        try
        {
            Nail_Item nail_Item = litsnail_mydictionary[slot.transform.position.ToString()];
            if (nail_Item != null)
            {
                nail_Item.transform.parent = slot.transform;
                slot.nail_item = nail_Item;
                Debug.Log("3");
                nail_Item.slot_item = slot;
                nail_Item.preslot_item = slot;
            }
        }
        catch (Exception ex)
        {
          //  Debug.LogError(ex);
        }
    }

    public void Findslockforslot(Slot_Item slot)
    {
        try
        {
            Lock_Item lockItem = litslock_mydictionary[slot.transform.position.ToString()];
            if (lockItem != null)
            {
                lockItem.transform.parent = slot.transform;
            }
        }
        catch (Exception ex)
        {
           // Debug.LogError(ex);
        }
    }
    
    //public void Findsadforslot(Slot_Item slot)
    //{
    //    if(slot == null)
    //    {
    //        return;
    //    }
    //    List<Ad_Item> fakelistad = listad;
    //    Bounds bounds1 = slot.GetComponent<Collider2D>().bounds;
    //    for (int i = 0; i < fakelistad.Count; i++)
    //    {
    //        Bounds bounds2 = fakelistad[i].GetComponent<Collider2D>().bounds;

    //        if (bounds1.Intersects(bounds2))
    //        {
    //            fakelistad[i].transform.parent = slot.transform;
    //            slot.Aditem = fakelistad[i];
    //        }
    //    }
    //}
}



public class LoadDataBase : MonoBehaviour
{
    public static int checkloadedItem = 0;
    public int MaxLayerBoard;
    private Dictionary<int, Color> dictionaryColor = new Dictionary<int, Color>();
    private int itemCount;
    private int boardCount;
    public List<Board_Item> listsBoardAwaitDeath = new List<Board_Item>();

    private LevelController levelController;
    private LevelController LevelControllerPlay
    {
        set
        {
            levelController = value;
        }
        get
        {
            if(levelController == null) { 
                levelController = (LevelController)FindObjectOfType(typeof(LevelController));
            }
            return levelController;
        }
    }
    private static LoadDataBase instance;
    public static LoadDataBase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LoadDataBase>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private IEnumerator CoroutineCycleGame;
  

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            //DontDestroyOnLoad(this);
        }
      
    }

    public void LoadLevelGame(int level)
    {
        if (LevelController.Instance.LevelIDInt > 298)
        {
            Debug.Log("hay doi update nhe");
        }
        else
        {
            //SuAnalytics.LogEventLevelStart(LevelController.Instance.LevelIDInt,"");
            GameMonitor.Instance.TimeTotalSpendBeforePause = 0;
            if (LevelController.Instance.LevelDifficule == false)
            {
                GameMonitor.Instance.start_match_utc = DateTime.Now;
        
            }
          
            PrepareBeforeLoadLevel();
            LoadFileJsonLevel(level);
        }
    }

    public void PrepareBeforeLoadLevel()
    {
      //  Timer.instance.ResetTextTime();
        LevelController.Instance.NumberBoardDestroy = 0;
     
        ControllerHieu.Instance.nailLayerController.maxynail = int.MinValue;
        ControllerHieu.Instance.nailLayerController.FindnewTypeNail = false;
        BackFeature.Instance.EventBackFeature = null;
    }
    private void LoadFileJsonLevel(int level)
    {
        string titlelevel;

        string swapLevel = JsonReadLevelConfig.Instance.SwapLevel(level, LevelController.Instance.LevelDifficule);
        if (swapLevel == "")
        {

            if (LevelController.Instance.LevelDifficule == true)
            {

                titlelevel = $"Hieu\\Level\\Difficult\\{level}_data_super";
            }
            else
            {
                titlelevel = $"Hieu\\Level\\Easy\\data_{level}";
            }
        }
        else
        {
            titlelevel = swapLevel;        
        }      
        TextAsset textAsset = Resources.Load<TextAsset>(titlelevel);
        if (textAsset != null)
        {
            string jsonLevel = textAsset.text;
            string[] strings = jsonLevel.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            itemCount = strings.Length;
            foreach (string str in strings)
            {
                TextProcess(str);
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy file text có tên myTextFile trong thư mục Resources.");
        }
    }
   private void TextProcess(string str)
   {    
        if (str.StartsWith("tut:"))
        {
            HandTutEditString(str);
        }
        else if (str.StartsWith("board:"))
        {
            HandBoaEditString(str);
        }
        else if (str.StartsWith("slot:"))
        {
            HandSlotEditString(str);
        }
        else if (str.StartsWith("savable:"))
        {
            HandSavableEditString(str);
        }     
        else if (str.StartsWith("level"))
        {
            HandLevelEditString(str);
        }
        else if (str.StartsWith("spr:"))
        {
            HandSprEditString(str);
        }
   }

    public void CheckTimeSetUpMap()
    {
        itemCount--;
        if (itemCount == 0)
        {         
            CoroutineCycleGame = CycleLoad();
            StartCoroutine(CoroutineCycleGame);
        }
    }
    IEnumerator CycleLoad()
    {      
       
        yield return new WaitForSeconds(0.25f);

        //try
        //{
            ClearItem();
            CreatePhysic2dforboard();
            CheckBoardSetupMap();
            ControllerHieu.Instance.nailLayerController.SetupLayerBoard();
            


            //moi
            BoxsSystem.Instance.SetCurrentBox();

//            Timer.instance.Reset();
            // if (LevelController.Instance.LevelDifficule)
            // {
            //     HardSupport.Instance.Setup();
            //     LevelController.Instance.totalWood += ControllerHieu.Instance.rootlevel.listboard.Count;

            // }
            // else
            // {
            //     LevelController.Instance.totalWood = ControllerHieu.Instance.rootlevel.listboard.Count;  
            // }

        //}
        //catch (Exception e)
        //{
        //    ClearItem();
        //    ControllerHieu.Instance.nailLayerController.ClearLayer();
        //    ControllerHieu.Instance.rootlevel?.ClearRoot(() =>
        //    {
        //        for (int i = 0; i < ControllerHieu.Instance.transform.childCount; i++)
        //        {
        //            Destroy(ControllerHieu.Instance.transform.GetChild(i).gameObject);
        //        }
        //        SceneManager.LoadScene("scn_home");
        //    });
        //}
    }

    private void ClearItem()
    {
        if (NailPullFeature.Instance.check)
        {
            NailPullFeature.Instance.NotSetupFeature();
        }
       
    }
    private void CheckBoardSetupMap()
    {
        ActivePhysic2dforboard();    
    }

    private void CreatePhysic2dforboard()
    {
        ControllerHieu.Instance.nailLayerController.SetupLayer();
        foreach (Nail_Item nailItem in ControllerHieu.Instance.rootlevel.litsnail_mydictionary.Values)
        {      
            if (nailItem == null) return;
            CreatePhysic2dforboard_layersub(nailItem);
        }
        ControllerHieu.Instance.nailLayerController.Sapxeplaithutu();


    }

    public void CreatePhysic2dforboard_layersub(Nail_Item nailItem)
    {
        Bounds boundnail = nailItem.ColiderNail.bounds;
        Vector2 size = boundnail.size;
        List<int> layerboard = new List<int>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(nailItem.transform.position, size, 0);
        foreach (Collider2D collider in colliders)
        {
            Bounds bounds1 = collider.bounds;
            if (bounds1.Intersects(boundnail) && collider.CompareTag("Board"))
            {
                Board_Item board_item = collider.transform.GetComponent<Board_Item>();
                board_item.listnail.Add(nailItem);
                if (board_item.Define_intersection(nailItem.transform.position))
                {
                    // bool fullyOverlap = bounds1.Contains(boundnail.min) && bounds1.Contains(boundnail.max);
                    // if (fullyOverlap == true)
                    {

                        LoadSlotBoardAddressAble(board_item, nailItem);
                        layerboard.Add(collider.transform.gameObject.layer - 7);
                    }
                }
            }
        }
        nailItem.gameObject.layer = ControllerHieu.Instance.nailLayerController.InputNumber(layerboard, nailItem, 0);
    }

    public void CreatePhysic2dforboard_layersub_2(Nail_Item nailItem)
    {
        if (nailItem.gameObject.layer != 6) return;
      
        Bounds boundnail = nailItem.ColiderNail.bounds;
        Vector2 size = boundnail.size;
        List<int> layerboard = new List<int>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(nailItem.transform.position, size, 0);
        foreach (Collider2D collider in colliders)
        {
            Bounds bounds1 = collider.bounds;
            if (bounds1.Intersects(boundnail) && collider.CompareTag("Board"))
            {
                Board_Item board_item = collider.transform.GetComponent<Board_Item>();
                if (board_item.Define_intersection(nailItem.transform.position))
                {
                   layerboard.Add(collider.transform.gameObject.layer - 7);   
                }
            }
        }
        nailItem.gameObject.layer = ControllerHieu.Instance.nailLayerController.InputNumber(layerboard, nailItem,1);
    }
    public void CreatePhysic2dforboard_layersub_3(Nail_Item nailItem)
    {
        Bounds boundnail = nailItem.ColiderNail.bounds;
        Vector2 size = boundnail.size;
        List<int> layerboard = new List<int>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(nailItem.transform.position, size, 0);
        foreach (Collider2D collider in colliders)
        {
            Bounds bounds1 = collider.bounds;
            if (bounds1.Intersects(boundnail) && collider.CompareTag("Board"))
            {
                Board_Item board_item = collider.transform.GetComponent<Board_Item>();
                if (board_item.Define_intersection(nailItem.transform.position))
                {
                    layerboard.Add(collider.transform.gameObject.layer - 7);
                }
            }
        }
        nailItem.gameObject.layer = ControllerHieu.Instance.nailLayerController.InputNumber(layerboard, nailItem, 1);
    }


    public void ActivePhysic2dforboard()
    {
        for (int i=0; i< ControllerHieu.Instance.rootlevel.listboard.Count; i++)
        {
            ControllerHieu.Instance.rootlevel.listboard[i].SetupRb();
        }
    }


    public void LoadSlotBoardAddressAble(Board_Item board, Nail_Item nail_item)
    {
        Slot_board_Item slotboarditem = slotboard_Spawn.Instance._pool.Get();
        if (slotboarditem == null) return;
        slotboarditem.spriteBorder.sortingOrder = board.boardinfomation.layer+6;
        slotboarditem.transform.position = nail_item.transform.position;
        slotboarditem.transform.SetParent(board.transform);
        slotboarditem.transform.localScale = new Vector3(nail_item.nail.scale.x/ board.boardinfomation.scale.x, (nail_item.nail.scale.x * (float)(board.boardinfomation.scale.x / board.boardinfomation.scale.y))/ board.boardinfomation.scale.x, nail_item.nail.scale.z)*1.7f;
        slotboarditem.transform.localRotation = Quaternion.identity;
        board.AddSlotforBoard(slotboarditem);
        HingeJoint2D hingeJoint = board.gameObject.AddComponent<HingeJoint2D>();
        
        Vector3 positioninparent =board.transform.InverseTransformPoint(nail_item.transform.position);
        
        
        hingeJoint.anchor = board.transform.InverseTransformPoint(nail_item.transform.position);
        
        


//moi
nail_item.hingeJoint2D = hingeJoint;



        hingeJoint.enableCollision = true;
        nail_item.listSlotBoardItem.Add(slotboarditem);     
        boardCount--;
        slotboarditem.hingeJointInSlot = hingeJoint;
        slotboarditem.SetMaskInSlotBool();
       
    }





    private void HandBoaEditString(string str)
    {
        string[] strings = DoubleStringEditNameandValue(str);
        Board board = JsonUtility.FromJson<Board>(strings[1]);
    
        LoadBoardAddressAble(strings[0],board);
    }

    private void HandTutEditString(string str)
    {
        string[] strings = DoubleStringEditNameandValue(str);       
        HandTut handTut = JsonUtility.FromJson<HandTut>(strings[1]);
        ControllerHieu.Instance.rootlevel.listHand.Add(handTut);       
        CheckTimeSetUpMap();
    }

    private void HandSlotEditString(string str)
    {
        string[] strings = DoubleStringEditNameandValue(str);     
        Slot slot = JsonUtility.FromJson<Slot>(strings[1]);
        LoadSlotAddressAble(strings[0], slot);
    }

    public void HandLevelEditString(string str)
    {
        string[] parts = str.Split(new string[] { "~~" }, StringSplitOptions.None);
        CheckTimeSetUpMap();
    }
    private void HandSprEditString(string str)
    {
        string[] strings = DoubleStringEditNameandValue(str);
        switch (strings[0])
        {
            case "key":
                HandSprEditString_Key(strings[1]);
                break;
            case "boom":        
                HandSprEditString_Boom(strings[1]);
                break;
        }
    }

    private void HandSprEditString_Key(string str)
    {   
        Key key = JsonUtility.FromJson<Key>(str);
        LoadKeyAddressAble(key);
    }
    private void HandSprEditString_Boom(string str)
    {  
        Boom boom = JsonUtility.FromJson<Boom>(str);
        LoadBoomAddressAble(boom);
    }

    public void HandSprEditString_Lock(string str)
    {
        Lock lock1 = JsonUtility.FromJson<Lock>(str);
        LoadLockAddressAble("lock", lock1);
    }
    public void LoadKeyAddressAble(Key key)
    {
        Key_Item keyItem = KeySpawner.Instance._pool.Get();
        if (keyItem == null) return;
        keyItem.transform.position = new Vector3(key.pos.x, key.pos.y, key.pos.z);
        keyItem.transform.rotation = Quaternion.Euler(new Vector3(key.rot.x, key.rot.y, key.rot.z));
        keyItem.transform.SetParent(LevelControllerPlay.MainLevelSetupCreateMap);
        keyItem.transform.localScale = new Vector3(key.scale.x, key.scale.y, key.scale.z);
        SpriteRenderer spriteRenderer = keyItem.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 100;
        ControllerHieu.Instance.rootlevel.listkey.Add(keyItem);
        CheckTimeSetUpMap();
    }

    private void LoadBoomAddressAble(Boom boom)
    {
        Boom_Item boomItem = BoomSpawner.Instance._pool.Get();
        if (boomItem == null) return;
        boomItem.transform.position = new Vector3(boom.pos.x, boom.pos.y, boom.pos.z);
        boomItem.transform.localScale = new Vector3(boom.scale.x, boom.scale.y, boom.scale.z);
        boomItem.TypeBoom = (TypeBoom)Enum.Parse(typeof(TypeBoom), boom.type);
        //boomItem.TypeBoom = TypeBoom.Time;
        if(boomItem.TypeBoom == TypeBoom.Move)
        {
            boomItem.IntTime = boom.Alive;
        }
        else
        {
            boomItem.IntTime = boom.TimeDeath+1;
        }
     
        boomItem.transform.SetParent(LevelControllerPlay.MainLevelSetupCreateMap);
        ControllerHieu.Instance.rootlevel.listBoom.Add(boomItem);
        boomItem.Setup();
        CheckTimeSetUpMap();
    }

    public void LoadLockAddressAble(string str, Lock lock1)
    {
        Lock_Item lockItem = LockSpawner.Instance._pool.Get();
        if (lockItem == null) return;
        lockItem.transform.position = new Vector3(lock1.pos.x, lock1.pos.y, lock1.pos.z);
        lockItem.spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(lock1.rot.x, lock1.rot.y, lock1.rot.z));
        lockItem.spriteRenderer.transform.localScale = new Vector3(lock1.scale.x, lock1.scale.y, lock1.scale.z);      
        lockItem.spriteRenderer.sortingOrder = lock1.layer;
        ControllerHieu.Instance.rootlevel.Findslotforlock(lockItem);

        if (ControllerHieu.Instance.rootlevel.litslock_mydictionary.ContainsKey(lockItem.transform.position.ToString()))
        {
            ControllerHieu.Instance.rootlevel.litslock_mydictionary[lockItem.transform.position.ToString()].ResetPool();
            ControllerHieu.Instance.rootlevel.litslock_mydictionary.Remove(lockItem.transform.position.ToString());
        }
        ControllerHieu.Instance.rootlevel.litslock_mydictionary.Add(lockItem.transform.position.ToString(), lockItem);
      
        CheckTimeSetUpMap();
    }

    private void HandSavableEditString(string str)
    {
        string[] strings = DoubleStringEditNameandValue(str);
        switch (strings[0])
        {
            case "nail":
                HandSavableEditString_Nail(strings[1]);
                break;
            case "bg":           
                HandSavableEditString_Bg(strings[1]);
                break;
            case "txt":
                HandSavableEditString_Txt(strings[1]);
                break; 
            case "holes":    
                HandSavableEditString_Holes(strings[1]);
                break;
            case "boxs":
                HandSavableEditString_Boxs(strings[1]);
                break;
        }
    }
    //private void HandSavableEditString_Ad(string str)
    //{
    //    Ad ad = JsonUtility.FromJson<Ad>(str);   
    //  //  LoadAdAddressAble("ad", ad);
    //}
    private void HandSavableEditString_Nail(string str)
    {
        Debug.Log(str);
       // Nail nail = JsonUtility.FromJson<Nail>(str);    
        Nail nail =  JsonConvert.DeserializeObject<Nail>(str);
        LoadNailAddressAble("nail",nail);
    }

    private void HandSavableEditString_Bg(string str)
    {
        Bg bg = JsonUtility.FromJson<Bg>(str);
        LoadBgAddressAble(bg);
    }

    private void HandSavableEditString_Txt(string str)
    {
        Txt txt = JsonUtility.FromJson<Txt>(str);
      
        CheckTimeSetUpMap();
    }
    private void HandSavableEditString_Holes(string str)
    {
        Holes holes = JsonUtility.FromJson<Holes>(str);
        HolesFreeSystem.Instance.SetupNumbers(holes.numbers);
        CheckTimeSetUpMap();
    }
     private void HandSavableEditString_Boxs(string str)
    {
        Debug.Log(str);
        Boxs boxs = JsonConvert.DeserializeObject<Boxs>(str);
        Debug.Log(boxs.ListCrew.Length);
        BoxsSystem.Instance.InitBox(boxs.TypeCL+""+boxs.ListCrew.Length,boxs.ListCrew.Length, boxs);
        CheckTimeSetUpMap();
    }


    private string[] DoubleStringEditNameandValue(string str)
    {
        
        int colonIndex = str.IndexOf(':');
        int tildeIndex = str.IndexOf("~~");

        // Trích xuất chuỗi
        string firstString = str.Substring(colonIndex + 1, tildeIndex - colonIndex - 1);
        string secondString = str.Substring(tildeIndex + 2);
        return new string[] {firstString,secondString};
    }

    public void LoadBoardAddressAble(string str, Board board)
    {

        Board_Item boardItem = null;
      
        switch (str)
        {
            case "tile17":
                boardItem = tile17Spawner.Instance._pool.Get();
                break;
              case "tile1":
                boardItem = tile1Spawner.Instance._pool.Get();
                break;
              case "tile2":
                boardItem = tile2Spawner.Instance._pool.Get();
                break;
            case "wavy":
               boardItem = WavySpawner.Instance._pool.Get();
                break;
            case "solidcicle":
                boardItem = SolidCicleSpawner.Instance._pool.Get();
                break;
            case "eshaped":
                boardItem = EShapeSpawner.Instance._pool.Get();
                break;
            case "square":
                boardItem = SquareSpawner.Instance._pool.Get();
                break;
            case "plusshaped":
                boardItem = PlusShapeSpawner.Instance._pool.Get();
                break;
            case "cshaped":
                boardItem = CshapeSpawner.Instance._pool.Get();
                break;
            case "moonshaped":
                boardItem = MoonShapeSpawner.Instance._pool.Get();
                break;
            case "lshaped":
                boardItem = LShaperSpawner.Instance._pool.Get();
                break;
            case "shackle8":
                boardItem = Shackle8Spawner.Instance._pool.Get();
                break;
            case "cicle8":
                boardItem = Cicle8Spawner.Instance._pool.Get();
                break;
            case "halfcicle":
                boardItem = HalfCicleSpawner.Instance._pool.Get();
                break;
            case "triangle":
                boardItem = boardtamgiacSpawner.Instance._pool.Get();
                break;
            case "ushaped":
                boardItem = UshapedSpawner.Instance._pool.Get();
                break;
          
            case "vboard":
                boardItem = VboardSpawner.Instance._pool.Get();
                break;
            case "l3board":
                boardItem = L3Spawner.Instance._pool.Get();
                break;
            case "l5board":
                boardItem = L5Spawner.Instance._pool.Get();
                break;
            case "bshape":
                boardItem = BShapeSpawner.Instance._pool.Get();
                break;
            case "moonshape2":
                boardItem = Moon2ShapeSpawner.Instance._pool.Get();
                break;
            case "ashape":
                boardItem = AShapeSpawner.Instance._pool.Get();
                break;
            case "tshape":
                boardItem = TShaperSpawner.Instance._pool.Get();
                break;
            case "l1shape":
                boardItem = L1ShapeSpawner.Instance._pool.Get();
                break;
            case "l2shape":
                boardItem = L2ShapeSpawner.Instance._pool.Get();
                break;
        }
        if (boardItem == null)
        {
            return;
        }
        boardItem.boardinfomation = board;
        boardItem.transform.position = new Vector3(board.pos.x, board.pos.y, board.pos.z);
        boardItem.transform.rotation = Quaternion.Euler(new Vector3(board.rot.x, board.rot.y, board.rot.z));
        boardItem.transform.parent = LevelControllerPlay.MainLevelSetupCreateMap;
        boardItem.transform.localScale = new Vector3(board.scale.x, board.scale.y, board.scale.z);
       
        boardItem.spritemain.sortingOrder = board.layer + 5;
        int layerboard = 7 + board.parent;
        boardItem.gameObject.layer = layerboard;
        if (layerboard >= MaxLayerBoard)
        {
            MaxLayerBoard = layerboard;
        }
        boardItem.spritemain.color = new Color(board.color.r, board.color.g, board.color.b, board.color.a);
        
        ControllerHieu.Instance.rootlevel.listboard.Add(boardItem);
        CheckTimeSetUpMap();
    }


    public void LoadSlotAddressAble(string str, Slot slot)
    {
        Slot_Item slot_Item = slot_Spawner.Instance._pool.Get();
        if (slot_Item == null) return;
        slot_Item.transform.position = new Vector3(slot.pos.x, slot.pos.y, slot.pos.z);
        slot_Item.transform.rotation = Quaternion.Euler(new Vector3(slot.rot.x, slot.rot.y, slot.rot.z));
        slot_Item.transform.parent = LevelControllerPlay.MainLevelSetupCreateMap;
        slot_Item.transform.localScale = new Vector3(slot.scale.x, slot.scale.y, slot.scale.z);
        slot_Item.hasNail = slot.hasNail;
        slot_Item.hasLock = slot.hasLock;
        slot_Item.hasLockAds = slot.hasLockAds;
        
        if (ControllerHieu.Instance.rootlevel.litsslot_mydictionary.ContainsKey(slot_Item.transform.position.ToString())){
            ControllerHieu.Instance.rootlevel.litsslot_mydictionary[slot_Item.transform.position.ToString()].ResetPool();
            ControllerHieu.Instance.rootlevel.litsslot_mydictionary.Remove(slot_Item.transform.position.ToString());
        }

        ControllerHieu.Instance.rootlevel.litsslot_mydictionary.Add(slot_Item.transform.position.ToString(), slot_Item);     
        if (slot_Item.hasNail == true)
        {
            ControllerHieu.Instance.rootlevel.Findsnailforslot(slot_Item);
        }
        if (slot_Item.hasLock == true)
        {
            //Controller.Instance.rootlevel.Findslockforslot(slot_Item);
            Lock_Item lockItem = LockSpawner.Instance._pool.Get();
            if (lockItem == null) return;
            lockItem.transform.position = new Vector3(slot.pos.x, slot.pos.y, slot.pos.z);
            lockItem.spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(slot.rot.x, slot.rot.y, slot.rot.z));
            lockItem.spriteRenderer.transform.localScale = new Vector3(slot.scale.x, slot.scale.y, slot.scale.z);
            lockItem.transform.parent = slot_Item.transform;
            if (ControllerHieu.Instance.rootlevel.litslock_mydictionary.ContainsKey(lockItem.transform.position.ToString()))
            {
                ControllerHieu.Instance.rootlevel.litslock_mydictionary[lockItem.transform.position.ToString()].ResetPool();
                ControllerHieu.Instance.rootlevel.litslock_mydictionary.Remove(lockItem.transform.position.ToString());
            }
            ControllerHieu.Instance.rootlevel.litslock_mydictionary.Add(lockItem.transform.position.ToString(), lockItem);
        }    
        if(slot_Item.hasLockAds == true)
        {
            Ad_Item aditem = Ad_Spawner.Instance._pool.Get();
            if (aditem == null) return;
            aditem.transform.position = new Vector3(slot.pos.x+0.1f, slot.pos.y+0.1f, slot.pos.z);
            aditem.transform.rotation = Quaternion.Euler(new Vector3(slot.rot.x, slot.rot.y, slot.rot.z));
            aditem.spriteRenderer.transform.localScale = new Vector3(slot.scale.x/2, slot.scale.y/2, slot.scale.z/2);
            aditem.transform.parent = slot_Item.transform;
            slot_Item.Aditem = aditem;
            ControllerHieu.Instance.rootlevel.listad.Add(aditem);
        }

        CheckTimeSetUpMap();
    }

    public void LoadNailAddressAble(string str,  Nail nail)
    {
        Nail_Item nail_Item = ControllerHieu.Instance.nailSpawner._pool.Get();
        if(nail_Item != null){
            nail_Item.nail = nail;      
            nail_Item.transform.localScale = new Vector3(nail.scale.x , nail.scale.y, nail.scale.z);
            nail_Item.transform.position = new Vector3(nail.pos.x, nail.pos.y, nail.pos.z);
            nail_Item.transform.rotation = Quaternion.Euler(new Vector3(nail.rot.x, nail.rot.y, nail.rot.z));
            ControllerHieu.Instance.rootlevel.Findslotfornail(nail_Item);     
            if (ControllerHieu.Instance.rootlevel.litsnail_mydictionary.ContainsKey(nail_Item.transform.position.ToString()))
            {
                ControllerHieu.Instance.rootlevel.litsnail_mydictionary.Remove(nail_Item.transform.position.ToString());
            }
            ControllerHieu.Instance.rootlevel.litsnail_mydictionary.Add(nail_Item.transform.position.ToString(), nail_Item);
            Color colornail = Color.white;
            switch(nail.material){
                case "pink":
                    colornail = Color.red;
                    break;
                case "blue":
                    colornail = Color.blue;
                    break;
                case "green":
                    colornail = Color.green;
                    break;
                 case "violet":
                    colornail = new Color((float)238/255, (float)130/255, (float)238/255,1);
                    break;
                 case "yellow":
                  colornail = new Color(1, 1, 0,1);
                    break;
        
            }
            nail_Item.spriteRenderer.color = colornail;

            CheckTimeSetUpMap();
        }
    }
    //public void LoadAdAddressAble(string str, Ad ad)
    //{
    //    Ad_Item aditem = Ad_Spawner.Instance._pool.Get();
    //    if (aditem == null) return;
    //    aditem.transform.position = new Vector3(ad.pos.x, ad.pos.y, ad.pos.z);
    //    aditem.transform.rotation = Quaternion.Euler(new Vector3(ad.rot.x, ad.rot.y, ad.rot.z));
    //    aditem.spriteRenderer.transform.localScale = new Vector3(ad.scale.x, ad.scale.y, ad.scale.z);
    //    Controller.Instance.rootlevel.listad.Add(aditem);
        
    //    CheckTimeSetUpMap();    
    //}

    public void LoadBgAddressAble(Bg bg)
    {
        Bg_Item bg_Item = bg_Spawner.Instance._pool.Get();
        if (bg_Item == null) return;
        bg_Item.transform.position = new Vector3(bg.pos.x, bg.pos.y, bg.pos.z);
        bg_Item.transform.rotation = Quaternion.Euler(new Vector3(bg.rot.x, bg.rot.y, bg.rot.z));
        bg_Item.transform.SetParent(LevelControllerPlay.MainLevelSetupCreateMap);   
        bg_Item.transform.localScale = new Vector3(bg.scale.x, bg.scale.y, bg.scale.z);
        ControllerHieu.Instance.rootlevel.bgItem = bg_Item;        
        CheckTimeSetUpMap();
    }
    Color GetRandomColor()
    {
        Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value); // Sinh màu ngẫu nhiên

        // Kiểm tra và điều chỉnh màu để tránh các màu quá tối
        while (randomColor.r < 0.2f && randomColor.g < 0.2f && randomColor.b < 0.2f)
        {
            randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }

        return randomColor;
    }
    public Color HandleColorForBoard(int index)
    {
        if(dictionaryColor.ContainsKey(index))
        {
            return dictionaryColor[index];
        }
        else
        {
            Color color = GetRandomColor();
            dictionaryColor.Add(index, color);
            return color;
        }
    }

   
}


