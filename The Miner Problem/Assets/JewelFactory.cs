using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelFactory : MonoBehaviour
{
    public Jewel diamond;
    public Jewel rubi;

    public void GetNewDiamond ()
    {
        Instantiate (diamond, transform.position, Quaternion.identity);
    }

    public void GetNewRubi ()
    {
        Instantiate (rubi, transform.position, Quaternion.identity);
    }
}
