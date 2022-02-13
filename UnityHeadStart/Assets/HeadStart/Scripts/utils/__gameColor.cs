using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.utils
{
    public static class __gameColor
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        private static Dictionary<string, string> _hexes;

        public static Color GetColor(string hex)
        {
            Color newCol;
            if (ColorUtility.TryParseHtmlString(hex, out newCol))
            {
                return newCol;
            }
            return new Color();
        }

        public static Color GetColor(COLOR colorEnum)
        {
            if (_hexes == null)
            {
                SetupHexes();
            }
            return GetColor(_hexes[colorEnum.ToString()]);
        }

        public static Color GetColorByName(string colorName)
        {
            if (_hexes == null)
            {
                SetupHexes();
            }
            return GetColor(_hexes[colorName]);
        }

        private static void SetupHexes()
        {
            _hexes = new Dictionary<string, string>();
            AddHex("White", "ffffff");
            AddHex("Blue_Dark_Ebony_Clay", "232c40");

            // Non Colors
            AddHex("Black_Rangoon_Green", "181810");
            AddHex("Black_Cod_Gray", "1b1b1b");

            AddHex("White_Orange", "FEFBF0");
            AddHex("White_Tana", "ded4c0");
            AddHex("White_Bison_Hide", "C0B59F");
            AddHex("White_Double_Colonial", "F0E3AF");
            AddHex("White_Spring_Wood", "F6F3EC");
            AddHex("White_Black_Haze", "f6f8f7");
            AddHex("White_Coconut_Cream", "f7edd0");

            // Grey
            AddHex("Grey_Juniper", "648285");
            AddHex("Grey_Mantle", "8ba197");
            AddHex("Grey_Norway", "b2c1a9");
            AddHex("Grey_Beryl_Green", "d8e0ba");

            // Yellow
            AddHex("Yellow_Texas", "f4f68f");
            AddHex("Yellow_Putty", "e3cb81");
            AddHex("Yellow_Orange", "fea24a");
            AddHex("Yellow_Portafino", "FFFDAD");
            AddHex("Yellow_Gimblet", "BAB76A");
            AddHex("Yellow_Limed_Oak", "A19C4B");
            AddHex("Yellow_Banana_Mania", "FCE6AD");
            AddHex("Yellow_Gold_Sand", "E4C58C");
            AddHex("Yellow_Winter_Hazel", "cfd08e");
            AddHex("Yellow_Sweet_Corn", "f9e181");
            AddHex("Yellow_Cream", "ffffcc");
            AddHex("Yellow", "ffff00");
            AddHex("Yellow_Thatch_Green", "48441e");

            // Orange
            AddHex("Orange_Rajah", "f9a277");
            AddHex("Orange_Koromiko", "ffc76d");
            AddHex("Brown_Orange_Antique_Brass", "c48d6f");
            AddHex("Brown_Orange_Coral_Reef", "c7b4a4");
            AddHex("Orange_Romantic", "ffccb3");
            AddHex("Orange_Atomic_Tangerine", "ff9966");
            AddHex("Orange_Cherokee", "face90");
            AddHex("Brown_Orange_Indian_Khaki", "c3ab93");
            AddHex("Orange_Burnt_Sienna", "ee6549");

            // Red
            AddHex("Red_Night_Shadz", "ac3548");
            AddHex("Red_Torch", "FF0054");
            AddHex("Red_Tabasco", "a62819");
            AddHex("Orange_Red_Cinnabar", "E84D2D");
            AddHex("Red_Razzmatazz", "d8064b");
            AddHex("Brown_Bazaar", "987b79");
            AddHex("Red_Brown_Eggplant", "68414d");
            AddHex("Red_Radical", "FF3366");
            AddHex("Red_Wild_Watermelon", "ff6680");
            AddHex("Red_Mona_Lisa", "ff9999");
            AddHex("Red_Maroon_Flush", "cb2159");
            AddHex("Red_Maroon_Flush_Darker", "b1234b");
            AddHex("Red_Sunglo", "e37875");
            AddHex("Red_Amaranth", "eb365f");
            AddHex("Pink_Dark_Claret", "6c1631");

            // Pink
            AddHex("Pink_Dark_Night_Shadz", "9a3054");
            AddHex("Pink_Dark_Tawny_Port", "6d1d44");
            AddHex("Pink_Hibiscus", "b7266d");
            AddHex("Brown_Dark_Pink_Aubergine", "380821");
            AddHex("Pink_Hot", "FF51B2");
            AddHex("Pink_Charm", "d9738c");
            AddHex("Purple_Pink_Fuchsia", "b34db3");
            AddHex("Purple_Fuchsia_Blue", "7A5EBA");
            AddHex("Purple_Pink_Wisteria", "a667ab");

            // Purple
            AddHex("Purple_Lavender", "B76BD5");
            AddHex(" Purple_Dark_Bastille", "211924");
            AddHex(" Purple_Dark_Voodoo", "4f3654");
            AddHex(" Purple_Dark_Haiti", "201232");
            AddHex("Purple_Heart", "8c26d9");
            AddHex("Purple_Violet_Electric", "6600ff");
            AddHex("Purple_Studio", "6E53B0");
            AddHex("Purple_Salt_Box", "66546D");

            // Brown
            AddHex("Brown_Finn", "532742");
            AddHex("Brown_Buccaneer", "653543");
            AddHex("Brown_Ferra", "77494c");
            AddHex("Brown_Roman_Coffee", "805753");
            AddHex("Brown_Loulou", "4c1036");
            AddHex("Brown_Flint", "6C645B");
            AddHex("Brown_English_Walnut", "3E2C22");
            AddHex("Brown_Gondola", "170d0e");
            AddHex("Brown_Bistre", "3c2f21");
            AddHex("Brown_Tobacco", "69553f");
            AddHex("Brown_Saddle", "4f2624");
            AddHex("Brown_Mule_Fawn", "955333");
            AddHex("Brown_Kabul", "69413F");
            AddHex("Brown_Muddy_Waters", "BB885D");
            AddHex("Brown_Thunder", "2C2024");
            AddHex("Brown_Sandrift", "af9179");

            // Green
            AddHex("Green_Inch_Worm", "a5ed24");
            AddHex("Green_Genoa", "0f6c64");
            AddHex("Green_Eucalyptus", "27836c");
            AddHex("Green_Dark_Deep_Teal", "003030");
            AddHex("Green_Observatory", "008f7b");
            AddHex("Green_Jungle", "1e9b8c");
            AddHex("Green_Java", "1edda4");
            AddHex("Green_Pastel", "81ed84");
            AddHex("Green_Light_Coriander", "bdccab");
            AddHex("Green_Light_Turquoise", "5FE7B7");
            AddHex("Green_Sulu", "ABF667");
            AddHex("Green_Light_Spring_Rain", "A0C6AF");
            AddHex("Green_Light_Spring_Rain_Darker", "98c798");
            AddHex("Green_Aqua_Forest", "65aa8b");
            AddHex("Green_Light_Magic_Mint", "B0EFD4");
            AddHex("Green_Light_Monte_Carlo", "8bd2c0");
            AddHex("Green_Como", "518a77");
            AddHex("Green_Highland", "829F6F");
            AddHex("Green_Persian", "009999");
            AddHex("Green_Keppel", "40b399");
            AddHex("Green_De_York", "80cc99");
            AddHex("Green_Yellow", "bfe699");
            AddHex("Yellow_Pale_Canary", "ffff99");
            AddHex("Green_Teal", "008d7b");
            AddHex("Green_Jade", "00cc66");
            AddHex("Green_Conifer", "80e633");
            AddHex("Green_Pastel_Darker", "74E46A");
            AddHex("Green_Lochinvar", "329c71");
            AddHex("Green_Shamrock", "35c0a0");
            AddHex("Green_Ocean", "49b386");
            AddHex("Green_William", "386f61");
            AddHex("Green_Puerto_Rico", "50bf9e");
            AddHex("Green_Patina", "5e987f");
            AddHex("Green_Red_Xanadu", "77867A");
            AddHex("Green_Blue_Wedgewood", "4A8EA2");
            AddHex("Green_Wild_Willow", "AAC67C");

            // Blue
            AddHex("Blue_Cornflower", "4e8feb");
            AddHex("Blue_Dark_Zodiac", "11164e");
            AddHex("Blue_Darker_Biscay", "1e4a6a");
            AddHex("Blue_Turquoise", "60e6c7");
            AddHex("Blue_Darker_Ebony_Clay", "1f2333");
            AddHex("Blue_Darker_Rhino", "27374f");
            AddHex("Blue_Dark_Pickled_Bluewood", "2F3F4E");
            AddHex("Blue_Dark_Pickled_Bluewood_Darker", "2b3e47");
            AddHex("Blue_Keppel", "36b1b0");
            AddHex("Blue_Juniper", "718f8b");
            AddHex("Blue_Dark_Mirage", "22203d");
            AddHex("Blue_Dark_Mirage_Darker", "18272d");
            AddHex("Blue_Fiord", "455964");
            AddHex("Blue_Dark_Downriver", "091D42");
            AddHex("Blue_Darker_Nile", "184258");
            AddHex("Blue_Eastern", "1cadb7");
            AddHex("Blue_Lagoon", "007284");
            AddHex("Blue_Stone", "02595a");
            AddHex("Blue_Kimberly", "6d67a3");
            AddHex("Blue_San_Marino", "43679c");
            AddHex("Blue_Wedgewood", "49929d");
        }

        private static void AddHex(string key, string hex)
        {
            _hexes.Add(key, hex[0] == '#' ? hex : "#" + hex);
        }
    }
}

public enum COLOR
{
    // Backgrounds
    Blue_Dark_Ebony_Clay,

    // Non Colors
    Black_Rangoon_Green,
    Black_Cod_Gray,
    White_Orange,
    White_Tana,
    White_Bison_Hide,
    White_Double_Colonial,
    White_Spring_Wood,
    White_Black_Haze,
    White_Coconut_Cream,

    // Grey
    Grey_Juniper,
    Grey_Mantle,
    Grey_Norway,
    Grey_Beryl_Green,

    // Yellow
    Yellow_Texas,
    Yellow_Putty,
    Yellow_Orange,
    Yellow_Portafino,
    Yellow_Gimblet,
    Yellow_Limed_Oak,
    Yellow_Banana_Mania,
    Yellow_Gold_Sand,
    Yellow_Winter_Hazel,
    Yellow_Sweet_Corn,
    Yellow_Cream,
    Yellow,
    Yellow_Thatch_Green,
    Yellow_Pale_Canary,

    // Orange
    Orange_Rajah,
    Orange_Cherokee,
    Orange_Koromiko,
    Orange_Romantic,
    Orange_Atomic_Tangerine,
    Orange_Red_Cinnabar,

    // Red
    Red_Torch,
    Red_Radical,
    Red_Wild_Watermelon,
    Red_Mona_Lisa,
    Red_Sunglo,
    Red_Amaranth,

    // Dark_Red
    Red_Razzmatazz,
    Red_Maroon_Flush,
    Red_Maroon_Flush_Darker,
    Red_Night_Shadz,
    Red_Brown_Eggplant,
    Red_Tabasco,

    // Pink
    Pink_Hot,
    Pink_Charm,
    Pink_Hibiscus,

    // Dark_Pink
    Pink_Dark_Night_Shadz,
    Pink_Dark_Tawny_Port,
    Pink_Dark_Claret,

    // Purple
    Purple_Lavender,
    Purple_Pink_Wisteria,
    Purple_Pink_Fuchsia,
    Purple_Fuchsia_Blue,
    Purple_Heart,
    Purple_Violet_Electric,
    Purple_Studio,
    Purple_Salt_Box,

    // Dark_Purple
    Purple_Dark_Voodoo,
    Purple_Dark_Haiti,
    Purple_Dark_Bastille,

    // Brown
    Brown_Orange_Coral_Reef,
    Brown_Orange_Indian_Khaki,
    Orange_Burnt_Sienna,
    Brown_Sandrift,
    Brown_Bazaar,
    Brown_Orange_Antique_Brass,
    Brown_Roman_Coffee,
    Brown_Ferra,
    Brown_Tobacco,
    Brown_Mule_Fawn,
    Brown_Buccaneer,
    Brown_Kabul,
    Brown_Muddy_Waters,

    // Dark_Brown
    Brown_Finn,
    Brown_Saddle,
    Brown_Loulou,
    Brown_Flint,
    Brown_Dark_Pink_Aubergine,
    Brown_Bistre,
    Brown_English_Walnut,
    Brown_Thunder,
    Brown_Gondola,

    // Light_Green
    Green_Light_Magic_Mint,
    Green_Light_Monte_Carlo,
    Green_Light_Turquoise,
    Green_Light_Spring_Rain,
    Green_Light_Spring_Rain_Darker,
    Green_Light_Coriander,

    // Green
    Green_Yellow,
    Green_Sulu,
    Green_Conifer,
    Green_Inch_Worm,
    Green_Pastel,
    Green_Pastel_Darker,
    Green_Jade,
    Green_Java,
    Green_Aqua_Forest,
    Green_Highland,
    Green_Persian,
    Green_Keppel,
    Green_De_York,
    Green_Shamrock,
    Green_Ocean,
    Green_Puerto_Rico,
    Green_Teal,
    Green_Lochinvar,
    Green_Como,
    Green_Patina,
    Green_Red_Xanadu,
    Green_Blue_Wedgewood,
    Green_Wild_Willow,
    Green_Eucalyptus,
    Green_Observatory,
    Green_Jungle,
    Green_William,
    Green_Genoa,

    // Dark_Green
    Green_Dark_Deep_Teal,

    // Blue
    Blue_Turquoise,
    Blue_Keppel,
    Blue_Juniper,
    Blue_Eastern,
    Blue_Kimberly,
    Blue_San_Marino,
    Blue_Wedgewood,
    Blue_Lagoon,
    Blue_Stone,
    Blue_Fiord,
    Blue_Cornflower,

    // Dark_Blue
    Blue_Darker_Biscay,
    Blue_Darker_Nile,
    Blue_Dark_Pickled_Bluewood,
    Blue_Dark_Pickled_Bluewood_Darker,
    Blue_Darker_Rhino,
    Blue_Darker_Ebony_Clay,
    Blue_Dark_Zodiac,
    Blue_Dark_Downriver,
    Blue_Dark_Mirage,
    Blue_Dark_Mirage_Darker,
}
