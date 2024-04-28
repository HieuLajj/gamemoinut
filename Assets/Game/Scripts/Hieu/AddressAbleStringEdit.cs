using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public static class AddressAbleStringEdit
{
   public static string URLAddress(string str)
   {
        string url = null;
        switch (str)
        {
            case "locked":
                url = "Assets/Prefabs/Locked.prefab";
                break;
            case "nail_particle":
                url = "Assets/Particle/Flash_magic_ellow_blue.prefab";
                break;    
            case "slot_board":
                url = "Assets/Prefabs/ItemInMap/slotinboard.prefab";
                break;    
            case "ad":
                url = "Assets/Prefabs/ItemInMap/ad.prefab";
                break;         
            case "lock":
                url = "Assets/Prefabs/ItemInMap/lock.prefab";
                break;
            case "key":
                url = "Assets/Prefabs/ItemInMap/key.prefab";
                break;       
            case "slot":
                url = "Assets/Prefabs/ItemInMap/slot.prefab";
                break;
            case "nail":
                url = "nail";
                break;
            case "bg":
                url = "Assets/Prefabs/ItemInMap/bg.prefab";
                break;     
            case "bar":               
                url = "Assets/Prefabs/ItemInMap/New/bar.prefab";
                break;
            case "bracket_1":
                url = "Assets/Material/Board/bracket_1.mat";
                break;
            case "wavy":
                url = "Assets/Prefabs/ItemInMap/New/wavy.prefab";
                break;
            case "solidcicle":
                url = "Assets/Prefabs/ItemInMap/New/solidcicle.prefab";
                break;
            case "eshape":
                url = "Assets/Prefabs/ItemInMap/New/eshape.prefab";
                break;
            case "square":
                url = "Assets/Prefabs/ItemInMap/New/square.prefab";
                break;
            case "plusshape":
                url = "Assets/Prefabs/ItemInMap/New/plusshape.prefab";
                break;
            case "cshaped":
                url = "Assets/Game/Prefabs/Hieu/Board/cshape.prefab";
                break;
            case "moonshaped":
                url = "Assets/Game/Prefabs/Hieu/Board/moonshape.prefab";
                break;
            case "lshaped":
                url = "Assets/Game/Prefabs/Hieu/Board/lshape.prefab";
                break;
            case "shackle8":
                url = "Assets/Game/Prefabs/Hieu/Board/Shackle8.prefab";
                break;
            case "cicle8":
                url = "Assets/Game/Prefabs/Hieu/Board/cicle8.prefab";
                break;
            case "halfcicle":
                url = "Assets/Game/Prefabs/Hieu/Board/haflcicle.prefab";
                break;
            case "triangle":
                url = "Assets/Game/Prefabs/Hieu/Board/trigle.prefab";
                break;
            case "ushaped":
                url = "Assets/Game/Prefabs/Hieu/Board/ushape.prefab";
                break;
            case "variation_bar":
                url = "Assets/Game/Prefabs/Hieu/Board/variation_bar.prefab";
                break;
            case "wavebar":
                url = "Assets/Game/Prefabs/Hieu/Board/wavebar.prefab";
                break;
            case "vboard":
                url = "Assets/Game/Prefabs/Hieu/Board/vshape.prefab";
                break;
            case "l3board":
                url = "Assets/Game/Prefabs/Hieu/Board/l3shape.prefab";
                break;
            case "l5board":
                url = "Assets/Game/Prefabs/Hieu/Board/l5shape.prefab";
                break;
            case "bshape":
                url = "Assets/Game/Prefabs/Hieu/Board/bshape.prefab";
                break;
            case "moonshape2":
                url = "Assets/Game/Prefabs/Hieu/Board/moonshape2.prefab";
                break;
            case "ashape":
                url = "Assets/Game/Prefabs/Hieu/Board/ashape.prefab";
                break;
            case "tshape":
                url = "Assets/Game/Prefabs/Hieu/Board/tshape.prefab";
                break;
            case "l1shape":
                url = "Assets/Game/Prefabs/Hieu/Board/l1shape.prefab";
                break;
            case "l2shape":
                url = "Assets/Game/Prefabs/Hieu/Board/l2shape.prefab";
                break;

        }
        return url;
   }
}
