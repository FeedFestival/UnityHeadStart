using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.utils;

public class ColorBank : MonoBehaviour
{
    public static ColorBank _ { get { return _colorBank; } }
    private static ColorBank _colorBank;
    void Awake()
    {
        _colorBank = this;
        DontDestroyOnLoad(this);
    }

    [Header("Backgrounds")]
    public Color Blue_Dark_Ebony_Clay;

    [Header("Non Colors")]
    public Color Black_Rangoon_Green;
    public Color Black_Cod_Gray;
    public Color White_Orange;
    public Color White_Tana;
    public Color White_Bison_Hide;
    public Color White_Double_Colonial;
    public Color White_Spring_Wood;
    public Color White_Black_Haze;
    public Color White_Coconut_Cream;

    [Header("Grey")]
    public Color Grey_Juniper;
    public Color Grey_Mantle;
    public Color Grey_Norway;
    public Color Grey_Beryl_Green;

    [Header("Yellow")]
    public Color Yellow_Texas;
    public Color Yellow_Putty;
    public Color Yellow_Orange;
    public Color Yellow_Portafino;
    public Color Yellow_Gimblet;
    public Color Yellow_Limed_Oak;
    public Color Yellow_Banana_Mania;
    public Color Yellow_Gold_Sand;
    public Color Yellow_Winter_Hazel;
    public Color Yellow_Sweet_Corn;
    public Color Yellow_Cream;
    public Color Yellow;
    public Color Yellow_Thatch_Green;
    public Color Yellow_Pale_Canary;

    [Header("Orange")]
    public Color Orange_Rajah;
    public Color Orange_Cherokee;
    public Color Orange_Koromiko;
    public Color Orange_Romantic;
    public Color Orange_Atomic_Tangerine;
    public Color Orange_Red_Cinnabar;

    [Header("Red")]
    public Color Red_Torch;
    public Color Red_Radical;
    public Color Red_Wild_Watermelon;
    public Color Red_Mona_Lisa;
    public Color Red_Sunglo;
    public Color Red_Amaranth;

    [Header("Dark_Red")]
    public Color Red_Razzmatazz;
    public Color Red_Maroon_Flush;
    public Color Red_Maroon_Flush_Darker;
    public Color Red_Night_Shadz;
    public Color Red_Brown_Eggplant;
    public Color Red_Tabasco;

    [Header("Pink")]
    public Color Pink_Hot;
    public Color Pink_Charm;
    public Color Pink_Hibiscus;

    [Header("Dark_Pink")]
    public Color Pink_Dark_Night_Shadz;
    public Color Pink_Dark_Tawny_Port;
    public Color Pink_Dark_Claret;

    [Header("Purple")]
    public Color Purple_Lavender;
    public Color Purple_Pink_Wisteria;
    public Color Purple_Pink_Fuchsia;
    public Color Purple_Fuchsia_Blue;
    public Color Purple_Heart;
    public Color Purple_Violet_Electric;
    public Color Purple_Studio;
    public Color Purple_Salt_Box;

    [Header("Dark_Purple")]
    public Color Purple_Dark_Voodoo;
    public Color Purple_Dark_Haiti;
    public Color Purple_Dark_Bastille;

    [Header("Brown")]
    public Color Brown_Orange_Coral_Reef;
    public Color Brown_Orange_Indian_Khaki;
    public Color Orange_Burnt_Sienna;
    public Color Brown_Sandrift;
    public Color Brown_Bazaar;
    public Color Brown_Orange_Antique_Brass;
    public Color Brown_Roman_Coffee;
    public Color Brown_Ferra;
    public Color Brown_Tobacco;
    public Color Brown_Mule_Fawn;
    public Color Brown_Buccaneer;
    public Color Brown_Kabul;
    public Color Brown_Muddy_Waters;

    [Header("Dark_Brown")]
    public Color Brown_Finn;
    public Color Brown_Saddle;
    public Color Brown_Loulou;
    public Color Brown_Flint;
    public Color Brown_Dark_Pink_Aubergine;
    public Color Brown_Bistre;
    public Color Brown_English_Walnut;
    public Color Brown_Thunder;
    public Color Brown_Gondola;

    [Header("Light_Green")]
    public Color Green_Light_Magic_Mint;
    public Color Green_Light_Monte_Carlo;
    public Color Green_Light_Turquoise;
    public Color Green_Light_Spring_Rain;
    public Color Green_Light_Spring_Rain_Darker;
    public Color Green_Light_Coriander;

    [Header("Green")]
    public Color Green_Yellow;
    public Color Green_Sulu;
    public Color Green_Conifer;
    public Color Green_Inch_Worm;
    public Color Green_Pastel;
    public Color Green_Pastel_Darker;
    public Color Green_Jade;
    public Color Green_Java;
    public Color Green_Aqua_Forest;
    public Color Green_Highland;
    public Color Green_Persian;
    public Color Green_Keppel;
    public Color Green_De_York;
    public Color Green_Shamrock;
    public Color Green_Ocean;
    public Color Green_Puerto_Rico;
    public Color Green_Teal;
    public Color Green_Lochinvar;
    public Color Green_Como;
    public Color Green_Patina;
    public Color Green_Red_Xanadu;
    public Color Green_Blue_Wedgewood;
    public Color Green_Wild_Willow;
    public Color Green_Eucalyptus;
    public Color Green_Observatory;
    public Color Green_Jungle;
    public Color Green_William;
    public Color Green_Genoa;

    [Header("Dark_Green")]
    public Color Green_Dark_Deep_Teal;

    [Header("Blue")]
    public Color Blue_Turquoise;
    public Color Blue_Keppel;
    public Color Blue_Juniper;
    public Color Blue_Eastern;
    public Color Blue_Kimberly;
    public Color Blue_San_Marino;
    public Color Blue_Wedgewood;
    public Color Blue_Lagoon;
    public Color Blue_Stone;
    public Color Blue_Fiord;
    public Color Blue_Cornflower;

    [Header("Dark_Blue")]
    public Color Blue_Darker_Biscay;
    public Color Blue_Darker_Nile;
    public Color Blue_Dark_Pickled_Bluewood;
    public Color Blue_Dark_Pickled_Bluewood_Darker;
    public Color Blue_Darker_Rhino;
    public Color Blue_Darker_Ebony_Clay;
    public Color Blue_Dark_Zodiac;
    public Color Blue_Dark_Downriver;
    public Color Blue_Dark_Mirage;
    public Color Blue_Dark_Mirage_Darker;


    private Dictionary<string, string> _hexes;

    // void Start()
    // {
    //     CalculateColors();
    // }

    public void CalculateColors()
    {
        SetupHexes();

        Blue_Dark_Ebony_Clay = GetColor(_hexes["Blue_Dark_Ebony_Clay"]);

        // Non Colors
        Black_Rangoon_Green = GetColor(_hexes["Black_Rangoon_Green"]);
        Black_Cod_Gray = GetColor(_hexes["Black_Cod_Gray"]);

        White_Orange = GetColor(_hexes["White_Orange"]);
        White_Tana = GetColor(_hexes["White_Tana"]);
        White_Bison_Hide = GetColor(_hexes["White_Bison_Hide"]);
        White_Double_Colonial = GetColor(_hexes["White_Double_Colonial"]);
        White_Spring_Wood = GetColor(_hexes["White_Spring_Wood"]);
        White_Black_Haze = GetColor(_hexes["White_Black_Haze"]);
        White_Coconut_Cream = GetColor(_hexes["White_Coconut_Cream"]);

        // Grey
        Grey_Juniper = GetColor(_hexes["Grey_Juniper"]);
        Grey_Mantle = GetColor(_hexes["Grey_Mantle"]);
        Grey_Norway = GetColor(_hexes["Grey_Norway"]);
        Grey_Beryl_Green = GetColor(_hexes["Grey_Beryl_Green"]);

        // Yellow
        Yellow_Texas = GetColor(_hexes["Yellow_Texas"]);
        Yellow_Putty = GetColor(_hexes["Yellow_Putty"]);
        Yellow_Orange = GetColor(_hexes["Yellow_Orange"]);
        Yellow_Portafino = GetColor(_hexes["Yellow_Portafino"]);
        Yellow_Gimblet = GetColor(_hexes["Yellow_Gimblet"]);
        Yellow_Limed_Oak = GetColor(_hexes["Yellow_Limed_Oak"]);
        Yellow_Banana_Mania = GetColor(_hexes["Yellow_Banana_Mania"]);
        Yellow_Gold_Sand = GetColor(_hexes["Yellow_Gold_Sand"]);
        Yellow_Winter_Hazel = GetColor(_hexes["Yellow_Winter_Hazel"]);
        Yellow_Sweet_Corn = GetColor(_hexes["Yellow_Winter_Hazel"]);
        Yellow_Cream = GetColor(_hexes["Yellow_Cream"]);
        Yellow = GetColor(_hexes["Yellow"]);
        Yellow_Thatch_Green = GetColor(_hexes["Yellow_Thatch_Green"]);


        // Orange
        Orange_Rajah = GetColor(_hexes["Orange_Rajah"]);
        Orange_Koromiko = GetColor(_hexes["Orange_Koromiko"]);
        Brown_Orange_Antique_Brass = GetColor(_hexes["Brown_Orange_Antique_Brass"]);
        Brown_Orange_Coral_Reef = GetColor(_hexes["Brown_Orange_Coral_Reef"]);
        Orange_Romantic = GetColor(_hexes["Orange_Romantic"]);
        Orange_Atomic_Tangerine = GetColor(_hexes["Orange_Atomic_Tangerine"]);
        Orange_Cherokee = GetColor(_hexes["Orange_Cherokee"]);
        Brown_Orange_Indian_Khaki = GetColor(_hexes["Brown_Orange_Indian_Khaki"]);
        Orange_Burnt_Sienna = GetColor(_hexes["Orange_Burnt_Sienna"]);


        // Red
        Red_Night_Shadz = GetColor(_hexes["Red_Night_Shadz"]);
        Red_Torch = GetColor(_hexes["Red_Torch"]);
        Red_Tabasco = GetColor(_hexes["Red_Tabasco"]);
        Orange_Red_Cinnabar = GetColor(_hexes["Orange_Red_Cinnabar"]);
        Red_Razzmatazz = GetColor(_hexes["Red_Razzmatazz"]);
        Brown_Bazaar = GetColor(_hexes["Brown_Bazaar"]);
        Red_Brown_Eggplant = GetColor(_hexes["Red_Brown_Eggplant"]);
        Red_Radical = GetColor(_hexes["Red_Radical"]);
        Red_Wild_Watermelon = GetColor(_hexes["Red_Wild_Watermelon"]);
        Red_Mona_Lisa = GetColor(_hexes["Red_Mona_Lisa"]);
        Red_Maroon_Flush = GetColor(_hexes["Red_Maroon_Flush"]);
        Red_Maroon_Flush_Darker = GetColor(_hexes["Red_Maroon_Flush_Darker"]);
        Red_Sunglo = GetColor(_hexes["Red_Sunglo"]);
        Red_Amaranth = GetColor(_hexes["Red_Amaranth"]);
        Pink_Dark_Claret = GetColor(_hexes["Pink_Dark_Claret"]);

        // Pink
        Pink_Dark_Night_Shadz = GetColor(_hexes["Pink_Dark_Night_Shadz"]);
        Pink_Dark_Tawny_Port = GetColor(_hexes["Pink_Dark_Tawny_Port"]);
        Pink_Hibiscus = GetColor(_hexes["Pink_Hibiscus"]);
        Brown_Dark_Pink_Aubergine = GetColor(_hexes["Brown_Dark_Pink_Aubergine"]);
        Pink_Hot = GetColor(_hexes["Pink_Hot"]);
        Pink_Charm = GetColor(_hexes["Pink_Charm"]);
        Purple_Pink_Fuchsia = GetColor(_hexes["Purple_Pink_Fuchsia"]);
        Purple_Fuchsia_Blue = GetColor(_hexes["Purple_Fuchsia_Blue"]);
        Purple_Pink_Wisteria = GetColor(_hexes["Purple_Pink_Wisteria"]);


        // Purple
        Purple_Lavender = GetColor(_hexes["Purple_Lavender"]);
        Purple_Dark_Bastille = GetColor(_hexes[" Purple_Dark_Bastille"]);
        Purple_Dark_Voodoo = GetColor(_hexes[" Purple_Dark_Voodoo"]);
        Purple_Dark_Haiti = GetColor(_hexes[" Purple_Dark_Haiti"]);
        Purple_Heart = GetColor(_hexes["Purple_Heart"]);
        Purple_Violet_Electric = GetColor(_hexes["Purple_Violet_Electric"]);
        Purple_Studio = GetColor(_hexes["Purple_Studio"]);
        Purple_Salt_Box = GetColor(_hexes["Purple_Salt_Box"]);

        // Brown
        Brown_Finn = GetColor(_hexes["Brown_Finn"]);
        Brown_Buccaneer = GetColor(_hexes["Brown_Buccaneer"]);
        Brown_Ferra = GetColor(_hexes["Brown_Ferra"]);
        Brown_Roman_Coffee = GetColor(_hexes["Brown_Roman_Coffee"]);
        Brown_Loulou = GetColor(_hexes["Brown_Loulou"]);
        Brown_Flint = GetColor(_hexes["Brown_Flint"]);
        Brown_English_Walnut = GetColor(_hexes["Brown_English_Walnut"]);
        Brown_Gondola = GetColor(_hexes["Brown_Gondola"]);
        Brown_Bistre = GetColor(_hexes["Brown_Bistre"]);
        Brown_Tobacco = GetColor(_hexes["Brown_Tobacco"]);
        Brown_Saddle = GetColor(_hexes["Brown_Saddle"]);
        Brown_Mule_Fawn = GetColor(_hexes["Brown_Mule_Fawn"]);
        Brown_Kabul = GetColor(_hexes["Brown_Kabul"]);
        Brown_Muddy_Waters = GetColor(_hexes["Brown_Muddy_Waters"]);
        Brown_Thunder = GetColor(_hexes["Brown_Thunder"]);
        Brown_Sandrift = GetColor(_hexes["Brown_Sandrift"]);

        // Green
        Green_Inch_Worm = GetColor(_hexes["Green_Inch_Worm"]);
        Green_Eucalyptus = GetColor(_hexes["Green_Eucalyptus"]);
        Green_Genoa = GetColor(_hexes["Green_Genoa"]);
        Green_Dark_Deep_Teal = GetColor(_hexes["Green_Dark_Deep_Teal"]);
        Green_Observatory = GetColor(_hexes["Green_Observatory"]);
        Green_Jungle = GetColor(_hexes["Green_Jungle"]);
        Green_Java = GetColor(_hexes["Green_Java"]);
        Green_Pastel = GetColor(_hexes["Green_Pastel"]);
        Green_Light_Coriander = GetColor(_hexes["Green_Light_Coriander"]);
        Green_Light_Turquoise = GetColor(_hexes["Green_Light_Turquoise"]);
        Green_Sulu = GetColor(_hexes["Green_Sulu"]);
        Green_Light_Spring_Rain = GetColor(_hexes["Green_Light_Spring_Rain"]);
        Green_Light_Spring_Rain_Darker = GetColor(_hexes["Green_Light_Spring_Rain_Darker"]);
        Green_Aqua_Forest = GetColor(_hexes["Green_Aqua_Forest"]);
        Green_Light_Magic_Mint = GetColor(_hexes["Green_Light_Magic_Mint"]);
        Green_Light_Monte_Carlo = GetColor(_hexes["Green_Light_Monte_Carlo"]);
        Green_Como = GetColor(_hexes["Green_Como"]);
        Green_Highland = GetColor(_hexes["Green_Highland"]);
        Green_Persian = GetColor(_hexes["Green_Persian"]);
        Green_Keppel = GetColor(_hexes["Green_Keppel"]);
        Green_De_York = GetColor(_hexes["Green_De_York"]);
        Green_Yellow = GetColor(_hexes["Green_Yellow"]);
        Yellow_Pale_Canary = GetColor(_hexes["Yellow_Pale_Canary"]);
        Green_Teal = GetColor(_hexes["Green_Teal"]);
        Green_Jade = GetColor(_hexes["Green_Jade"]);
        Green_Conifer = GetColor(_hexes["Green_Conifer"]);
        Green_Pastel_Darker = GetColor(_hexes["Green_Pastel_Darker"]);
        Green_Lochinvar = GetColor(_hexes["Green_Lochinvar"]);
        Green_Shamrock = GetColor(_hexes["Green_Shamrock"]);
        Green_Ocean = GetColor(_hexes["Green_Ocean"]);
        Green_William = GetColor(_hexes["Green_William"]);
        Green_Puerto_Rico = GetColor(_hexes["Green_Puerto_Rico"]);
        Green_Patina = GetColor(_hexes["Green_Patina"]);
        Green_Red_Xanadu = GetColor(_hexes["Green_Red_Xanadu"]);
        Green_Blue_Wedgewood = GetColor(_hexes["Green_Blue_Wedgewood"]);
        Green_Wild_Willow = GetColor(_hexes["Green_Wild_Willow"]);


        // Blue
        Blue_Dark_Zodiac = GetColor(_hexes["Blue_Dark_Zodiac"]);
        Blue_Darker_Biscay = GetColor(_hexes["Blue_Darker_Biscay"]);
        Blue_Turquoise = GetColor(_hexes["Blue_Turquoise"]);
        Blue_Darker_Ebony_Clay = GetColor(_hexes["Blue_Darker_Ebony_Clay"]);
        Blue_Darker_Rhino = GetColor(_hexes["Blue_Darker_Rhino"]);
        Blue_Dark_Pickled_Bluewood = GetColor(_hexes["Blue_Dark_Pickled_Bluewood"]);
        Blue_Dark_Pickled_Bluewood_Darker = GetColor(_hexes["Blue_Dark_Pickled_Bluewood_Darker"]);
        Blue_Keppel = GetColor(_hexes["Blue_Keppel"]);
        Blue_Juniper = GetColor(_hexes["Blue_Juniper"]);
        Blue_Dark_Mirage = GetColor(_hexes["Blue_Dark_Mirage"]);
        Blue_Dark_Mirage_Darker = GetColor(_hexes["Blue_Dark_Mirage_Darker"]);
        Blue_Fiord = GetColor(_hexes["Blue_Fiord"]);
        Blue_Cornflower = GetColor(_hexes["Blue_Cornflower"]);
        Blue_Dark_Downriver = GetColor(_hexes["Blue_Dark_Downriver"]);
        Blue_Darker_Nile = GetColor(_hexes["Blue_Darker_Nile"]);
        Blue_Eastern = GetColor(_hexes["Blue_Eastern"]);
        Blue_Lagoon = GetColor(_hexes["Blue_Lagoon"]);
        Blue_Stone = GetColor(_hexes["Blue_Stone"]);
        Blue_Kimberly = GetColor(_hexes["Blue_Kimberly"]);
        Blue_San_Marino = GetColor(_hexes["Blue_San_Marino"]);
        Blue_Wedgewood = GetColor(_hexes["Blue_Wedgewood"]);
    }

    public Color GetColor(string hex)
    {
        Color newCol;
        if (ColorUtility.TryParseHtmlString(hex, out newCol))
        {
            return newCol;
        }
        return new Color();
    }

    public Color GetColorByName(string colorName)
    {
        // Debug.Log(colorName);
        if (_hexes == null)
        {
            SetupHexes();
        }
        // Debug.Log(__utils.DebugDict<string>(_hexes));

        Color newCol;
        if (ColorUtility.TryParseHtmlString(_hexes[colorName], out newCol))
        {
            return newCol;
        }
        return new Color();
    }

    private void SetupHexes()
    {
        _hexes = new Dictionary<string, string>();
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

    private void AddHex(string key, string hex)
    {
        _hexes.Add(key, hex[0] == '#' ? hex : "#" + hex);
    }
}
