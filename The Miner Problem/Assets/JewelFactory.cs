using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelFactory : MonoBehaviour
{
    public Jewel diamond;
    public Jewel rubi;
    public Jewel emerald;

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
                GetNewDiamond();
                break;
            case 2:
                GetNewRubi();
                break;
            case 3:
                GetNewEmerald();
                break;                
        }
    }
}
