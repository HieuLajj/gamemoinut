using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NailLayerController : MonoBehaviour
{
    private Dictionary<List<int>, Nail_Item> dictionaryNaillError = new Dictionary<List<int>, Nail_Item>();
    public Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
    private  List<int> ListDefaultFullLayer;
    //public int mm = 0;
    public string allxstring;
    public float maxynail;
    public int checkerror;
    public bool FindnewTypeNail;
    public Action EventNailRegistedNewType;
    //public void SetupkeyValue()
    //{
    //    Debug.Log("==============================================");
    //    keyValuePairs = new Dictionary<string, int>();
    //    dictionaryNaillError.Clear();
    //    ListDefaultFullLayer.Clear();
    //    checkerror = 0;
    //    allxstring = string.Empty;
    //    maxynail = 0;
       
    //}

    public void SetupLayer()
    {
        checkerror = 0;
        ListDefaultFullLayer = Enumerable.Range(LoadDataBase.Instance.MaxLayerBoard+1, 30).ToList();
        allxstring = string.Concat(Enumerable.Repeat("X", LoadDataBase.Instance.MaxLayerBoard-7+1));    
    }

    public int InputNumber(List<int> inputs, Nail_Item nailItem, int priority){
        //tim neuu o tren cung nay    
           // Slot_Item slot_Item = nailItem.transform.parent.GetComponent<Slot_Item>();
        if (nailItem.transform.position.y >= maxynail-0.2f && priority == 1)
        {         
             CheckRootNail(nailItem, allxstring);
             return 6;
        }
        char[] numberArray = null;
        try
        {
            numberArray = allxstring.ToCharArray();
        }
        catch
        {
            Debug.Log("loi trong nailayer controller");
            return 6;
        }
        
        foreach(int input in inputs){
            numberArray[input] = 'y';
        }     
        string numberString = string.Join("", numberArray); 
        if(numberString == allxstring)
        {
            CheckRootNail(nailItem, allxstring);

            return 31;
        }
        if(keyValuePairs.ContainsKey(numberString)){
       
            CheckRootNail(nailItem, numberString);
            return keyValuePairs[numberString];
        }else{
            int m = IntIndexPosition();
            if (m <= 30)
            {
                keyValuePairs[numberString] = m;
                ChangeLayer(inputs, m);          
                CheckRootNail(nailItem,numberString);   
                return m;
            }
            else
            {          
                return FindTheClosetString(numberArray, nailItem, inputs);
            }
        }
    }

    public void Sapxeplaithutu()
    {
        if (checkerror <= 3)
        {
            return;
        }     
        ControllerHieu.Instance.nailLayerController.FindnewTypeNail = true;
        ClearLayer();
        List<Nail_Item> listnail = ControllerHieu.Instance.rootlevel.litsnail_mydictionary.Values.ToList();
        listnail.Sort((s1, s2) => s1.listSlotBoardItem.Count().CompareTo(s2.listSlotBoardItem.Count()));
        foreach(Nail_Item item in listnail)
        {
            if (item == null) return;
            item.gameObject.layer = 6;
        }
    }

    public int InputNumber2choError(List<int> inputs, Nail_Item nailItem)
    {
     
        char[] numberArray = allxstring.ToCharArray();
        foreach (int input in inputs)
        {
            numberArray[input] = 'y';
        }
        string numberString = string.Join("", numberArray);
   
       
            int m = IntIndexPosition();
            if (m <= 30)
            {
                keyValuePairs[numberString] = m;
                ChangeLayer(inputs, m);
               // mm++;
                CheckRootNail2choError(nailItem, numberString);
                return m;
            }
        return 6;
       
    }


    
    private int IntIndexPosition()
    {
        foreach (int intIndex in ListDefaultFullLayer)
        {
            if (!keyValuePairs.Values.Contains(intIndex))
            {            
                return intIndex;
            }
        }
        return 2000;
    }

    public int FindTheClosetString(char[] numberArray, Nail_Item nailItem, List<int> inputs)
    {
        int indexY = 0;
        int minindexY = 100000;
        bool checknotalike = false;
        string flagkey = "";
        foreach (string key in keyValuePairs.Keys)
        {
            indexY = 0;
            checknotalike = false;
            char[] numberFind = key.ToCharArray();
            for (int i = 0; i < numberFind.Length; i++)
            {
                if (numberArray[i] == 'y')
                {
                    if (numberFind[i] != 'y')
                    {
                        checknotalike = true;
                        break;
                    }
                }
                if (numberFind[i] == 'y')
                {
                    indexY++;
                }
            }
            if (!checknotalike && indexY <= minindexY)
            {
                minindexY = indexY;
                flagkey = key;
            }
        }
        if (flagkey == "")
        {
            CheckRootNail(nailItem, allxstring);
            dictionaryNaillError.Add(inputs, nailItem);
            checkerror++;
            return 6;
        }
        else
        {
            checkerror++;
            CheckRootNail(nailItem, flagkey);
            return keyValuePairs[flagkey];
        }

    }

    
    private void FetchDictionaryErrorNail(Nail_Item nailItem)
    {
        // List<List<int>> listsave = new List<List<int>>();
        if (dictionaryNaillError.Count == 0 || FindnewTypeNail) return;
        Dictionary<List<int>, float> distances = new Dictionary<List<int>, float>();

        
        foreach (var pair in dictionaryNaillError)
        {
            float distance = Vector3.Distance(pair.Value.transform.position, nailItem.preslot_item.transform.position);
            distances.Add(pair.Key, distance);
        }

        var sortedDistances = distances.OrderBy(x => x.Value);

        
        Dictionary<List<int>, Nail_Item> sortedObjectPositions = new Dictionary<List<int>, Nail_Item>();

       
        foreach (var pair in sortedDistances)
        {
            sortedObjectPositions.Add(pair.Key, dictionaryNaillError[pair.Key]);
        }


        List<int> key = sortedObjectPositions.First().Key;
        Nail_Item value = sortedObjectPositions.First().Value;
        int m = InputNumber2choError(key, value);
        if (m != 6)
        {

            value.gameObject.layer = m;
            dictionaryNaillError.Remove(key);
        }
    }


    private int Compare(Vector3 a, Vector3 b, Vector3 c)
    {
        float distanceA = Vector3.Distance(a, c);
        float distanceB = Vector3.Distance(b, c);

        return distanceA.CompareTo(distanceB);
    }


    public void CheckRootNail(Nail_Item nailItem, string flagkey)
    {
        nailItem.keyLayer = flagkey;
        
            bool checkLayerNailexits = false;
            if (nailItem.keyLayerPre != "" && nailItem.keyLayerPre != allxstring)
            {

                foreach (Nail_Item naiItemcheck in ControllerHieu.Instance.rootlevel.litsnail_mydictionary.Values)
                {
                    if (naiItemcheck.keyLayer == nailItem.keyLayerPre)
                    {
                        checkLayerNailexits = true;
                  
                        break;
                    }
                }
                if (!checkLayerNailexits)
                {
                    keyValuePairs.Remove(nailItem.keyLayerPre);               
                    FetchDictionaryErrorNail(nailItem);

                }

            }
        nailItem.keyLayerPre = flagkey;  
    }
    public void CheckRootNail(Nail_Item nailItem)
    {
        nailItem.keyLayer = allxstring;

        bool checkLayerNailexits = false;
        if (nailItem.keyLayerPre != "" && nailItem.keyLayerPre != allxstring)
        {

            foreach (Nail_Item naiItemcheck in ControllerHieu.Instance.rootlevel.litsnail_mydictionary.Values)
            {
                if (naiItemcheck.keyLayer == nailItem.keyLayerPre)
                {
                    checkLayerNailexits = true;

                    break;
                }
            }
            if (!checkLayerNailexits)
            {
                keyValuePairs.Remove(nailItem.keyLayerPre);
            }
        }
        nailItem.keyLayerPre = allxstring;

    }
    public void CheckRootNail2choError(Nail_Item nailItem, string flagkey)
    {
        nailItem.keyLayer = flagkey;

        bool checkLayerNailexits = false;
        if (nailItem.keyLayerPre != "" && flagkey != allxstring)
        {

            foreach (Nail_Item naiItemcheck in ControllerHieu.Instance.rootlevel.litsnail_mydictionary.Values)
            {
                if (naiItemcheck.keyLayer == nailItem.keyLayerPre)
                {
                    checkLayerNailexits = true;
                    break;
                }
            }
            if (!checkLayerNailexits)
            {
                keyValuePairs.Remove(nailItem.keyLayerPre);

            }

        }
        nailItem.keyLayerPre = flagkey;
   
    }
    private void ChangeLayer(List<int> inputs, int layer){
        ResetLayer(layer);
        foreach (int input in inputs){
            Physics2D.IgnoreLayerCollision(layer, 7 + input, true);   
        }
    }



    public void ResetLayer(int layer)
    {
        int m = 7;
        while(m <= LoadDataBase.Instance.MaxLayerBoard)
        {
            try
            {
                Physics2D.IgnoreLayerCollision(layer, m, false);
            }
            catch
            {
                Debug.LogError("goa tri loi gap phai la" + m);
            }
           
            m++;
        }
    }



    public void ClearLayer()
    {

        keyValuePairs.Clear();
        //keyValuePairs = new Dictionary<string, int>();
        CleanLayerBoard();    
        dictionaryNaillError.Clear();
        ListDefaultFullLayer.Clear();
        checkerror = 0;
        
        maxynail = 0;
    }

    public void CleanLayerBoard()
    {    
        for(int i= 7; i<= 13; i++)
        {
            int a = i;
            for(int j=7; j<= 13; j++)
            {
                if(a == j)
                {
                    Physics2D.IgnoreLayerCollision(a, j, false);
                }
                else
                {
                    Physics2D.IgnoreLayerCollision(a, j, true);
                }
            }
        }
    }
    public void SetupLayerBoard()
    {
        for (int i = 7; i <=  LoadDataBase.Instance.MaxLayerBoard; i++)
        {
            int a = i;
            for (int j = 7; j <= LoadDataBase.Instance.MaxLayerBoard; j++)
            {
                if (a == j)
                {
                    Physics2D.IgnoreLayerCollision(a, j, false);
                }
                else
                {
                    Physics2D.IgnoreLayerCollision(a, j, true);
                }
            }
        }
    }
}
