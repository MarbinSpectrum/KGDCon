using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExtensions
{
    public static string WithComma(this int integer)
        => integer.ToString("#,##0");
}
