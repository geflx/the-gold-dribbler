using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelFactory : MonoBehaviour
{
    public Jewel gold;
    public Jewel diamond;
    public Jewel rubi;
    public Jewel emerald;

    public void GetNewGold ()
    {
        Instantiate (gold, transform.position, Quaternion.identity);
    }

    public void GetNewDiamond ()
    {
        Instantiate (diamond, transform.position, Quaternion.identity);
    }

    public void GetNewRubi ()
    {
        Instantiate (rubi, transform.position, Quaternion.identity);
    }

    public void GetNewEmerald ()
    {
        Instantiate (emerald, transform.position, Quaternion.identity);
    }

    public void GetNewJewel (int index)
    {
        switch (index) {
            case 1:
                GetNewGold();
                break;
            case 2:
                GetNewDiamond();
                break;
            case 3:
                GetNewEmerald();
                break;    
            case 4:
                GetNewRubi();
                break;            
        }
    }
}
