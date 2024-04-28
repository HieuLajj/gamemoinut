using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketUIIngame : MonoBehaviour
{
   public void ActiveUITicket()
    {
        MagicTicket.DisableAction += ResumeTime;
        MagicTicket.checkoption = 1;
        UIEvents.Instance.ShowMagicTicket();
    }
    public void ActiveUISliverTicket()
    {
        MagicTicket.DisableAction += ResumeTime;
        MagicTicket.checkoption = 2;
        UIEvents.Instance.ShowMagicTicket();
    }


    private void ResumeTime()
    {
        Timer.instance.Resume();
    }
}
