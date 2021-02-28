using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UChatUtils
{
    private static int id;

    public static int GenerateId()
    {
        return id++;
    }
}
