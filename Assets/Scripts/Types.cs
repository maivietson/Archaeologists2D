using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Types : MonoBehaviour
{
    public static class NPCs
    {
        //public static string Type = "NPCs";
        //public static class ID
        //{
        //    public static string STR_START_COLLIDER_TRAPS = "STR_START_COLLIDER_TRAPS";
        //    public static string STR_END_COLLIDER_TRAPS = "STR_END_COLLIDER_TRAPS";
        //    public static string STR_COLLIDER_CHEST_CONVERT_TITLE = "STR_COLLIDER_CHEST_CONVERT_TITLE";
        //    public static string STR_COLLIDER_CHEST_CONVERT_CONTENT = "STR_COLLIDER_CHEST_CONVERT_CONTENT";
        //    public static string STR_RUN_FAST_AS_CAN = "STR_RUN_FAST_AS_CAN";
        //}

        public static ushort Type = 0;
        public static class ID
        {
            public static ushort STR_START_COLLIDER_TRAPS = 0;
            public static ushort STR_END_COLLIDER_TRAPS = 1;
            public static ushort STR_COLLIDER_CHEST_CONVERT_TITLE = 2;
            public static ushort STR_COLLIDER_CHEST_CONVERT_CONTENT = 3;
            public static ushort STR_COLLIDER_CHEST_NOT_ENOUGH_GOLD = 4;
            public static ushort STR_IMFORMATION_6 = 5;
            public static ushort STR_COLLIDER_CHEST_TAKE_GOLD = 6;
            public static ushort STR_COLLIDER_CHEST_TAKE_LIVE = 7;
        }
    }
}
