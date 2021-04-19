using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Stahli2Robots
{
    [XmlInclude(typeof(ErrorDescription))]
    public class ErrorDescription                        // class holding all Errors(PLC,Robot etc) numbers and description
    {
        public ErrorDescription()   //constructor
        {
            ErrorDescriptionEng = new string[1024];
            ErrorDescriptionHeb = new string[1024];

            ErrorDescriptionEng[1] = "";
            ErrorDescriptionEng[2] = "";
            ErrorDescriptionEng[3] = "";
            ErrorDescriptionEng[4] = "";
            ErrorDescriptionEng[5] = "";
            ErrorDescriptionEng[6] = "";
            ErrorDescriptionEng[7] = "";
            ErrorDescriptionEng[8] = "";

            ErrorDescriptionEng[15] = "";
            ErrorDescriptionEng[16] = "";
            ErrorDescriptionEng[17] = "";
            ErrorDescriptionEng[18] = "";
            ErrorDescriptionEng[19] = "";
            ErrorDescriptionEng[20] = "";

            ErrorDescriptionEng[101] = "";
            ErrorDescriptionEng[102] = "";
            ErrorDescriptionEng[103] = "";
            ErrorDescriptionEng[104] = "Vision Error at Loading cycle - Carrier Tech Holes Or Wrong Side";
            ErrorDescriptionEng[105] = "";
            ErrorDescriptionEng[106] = "";
            ErrorDescriptionEng[107] = "";
            ErrorDescriptionEng[108] = "";
            ErrorDescriptionEng[109] = "";
            ErrorDescriptionEng[110] = "";
            ErrorDescriptionEng[111] = "";
            ErrorDescriptionEng[112] = "";
            ErrorDescriptionEng[113] = "";
            ErrorDescriptionEng[114] = "";
            ErrorDescriptionEng[115] = "";

            ErrorDescriptionEng[120] = "";
            ErrorDescriptionEng[121] = "";
            ErrorDescriptionEng[122] = "";
            ErrorDescriptionEng[123] = "";
            ErrorDescriptionEng[124] = "";
            ErrorDescriptionEng[125] = "";
            ErrorDescriptionEng[126] = "";
            ErrorDescriptionEng[127] = "";
            ErrorDescriptionEng[128] = "";
            ErrorDescriptionEng[129] = "";
            ErrorDescriptionEng[130] = "";
            ErrorDescriptionEng[131] = "";
            ErrorDescriptionEng[132] = "";
            ErrorDescriptionEng[133] = "";
            ErrorDescriptionEng[134] = "";
            ErrorDescriptionEng[135] = "";
            ErrorDescriptionEng[136] = "";

            ErrorDescriptionEng[201] = "";
            ErrorDescriptionEng[202] = "";
            ErrorDescriptionEng[203] = "";

            ErrorDescriptionEng[215] = "";
            ErrorDescriptionEng[216] = "";
            ErrorDescriptionEng[217] = "";
            ErrorDescriptionEng[218] = "";
            ErrorDescriptionEng[219] = "";
            ErrorDescriptionEng[220] = "";
            ErrorDescriptionEng[221] = "";
            ErrorDescriptionEng[222] = "";
            ErrorDescriptionEng[223] = "";
            ErrorDescriptionEng[224] = "";
            ErrorDescriptionEng[225] = "";
            ErrorDescriptionEng[226] = "";
            ErrorDescriptionEng[227] = "";
            ErrorDescriptionEng[228] = "";
            ErrorDescriptionEng[229] = "";
            ErrorDescriptionEng[230] = "";
            ErrorDescriptionEng[231] = "";
            ErrorDescriptionEng[232] = "";

            ErrorDescriptionEng[240] = "";
            ErrorDescriptionEng[241] = "";
            ErrorDescriptionEng[242] = "";
            ErrorDescriptionEng[243] = "";
            ErrorDescriptionEng[244] = "";
            ErrorDescriptionEng[245] = "";
            ErrorDescriptionEng[246] = "";
            ErrorDescriptionEng[247] = "";

            ErrorDescriptionEng[250] = "";
            ErrorDescriptionEng[251] = "";
            ErrorDescriptionEng[252] = "";
            ErrorDescriptionEng[253] = "";
            ErrorDescriptionEng[254] = "";
            ErrorDescriptionEng[255] = "";
            ErrorDescriptionEng[256] = "";
            ErrorDescriptionEng[257] = "";
            ErrorDescriptionEng[258] = "";

            ErrorDescriptionEng[260] = "";
            ErrorDescriptionEng[261] = "";
            ErrorDescriptionEng[262] = "";
            ErrorDescriptionEng[263] = "";
            ErrorDescriptionEng[264] = "";
            ErrorDescriptionEng[265] = "";
            ErrorDescriptionEng[266] = "";
            ErrorDescriptionEng[267] = "";
            ErrorDescriptionEng[268] = "";
            ErrorDescriptionEng[269] = "";
            ErrorDescriptionEng[270] = "";
            ErrorDescriptionEng[271] = "";
            ErrorDescriptionEng[272] = "";
            ErrorDescriptionEng[273] = "";
            ErrorDescriptionEng[274] = "";
            ErrorDescriptionEng[275] = "";
            ErrorDescriptionEng[276] = "";
            ErrorDescriptionEng[277] = "";
            ErrorDescriptionEng[278] = "";

            ErrorDescriptionEng[280] = "";
            ErrorDescriptionEng[281] = "";
            ErrorDescriptionEng[282] = "";
            ErrorDescriptionEng[283] = "";
            ErrorDescriptionEng[284] = "";
            ErrorDescriptionEng[285] = "";

            ErrorDescriptionEng[290] = "";
            ErrorDescriptionEng[291] = "";
            ErrorDescriptionEng[292] = "";
            ErrorDescriptionEng[293] = "";

            ErrorDescriptionEng[301] = "";
            ErrorDescriptionEng[301] = "";
            ErrorDescriptionEng[301] = "";
            ErrorDescriptionEng[301] = "";
            ErrorDescriptionEng[301] = "";

            ErrorDescriptionEng[301] = "";
            ErrorDescriptionEng[302] = "";
            ErrorDescriptionEng[303] = "";
            ErrorDescriptionEng[304] = "";
            ErrorDescriptionEng[305] = "";
            ErrorDescriptionEng[306] = "";
            ErrorDescriptionEng[307] = "";
            ErrorDescriptionEng[308] = "";
            ErrorDescriptionEng[309] = "";
            ErrorDescriptionEng[310] = "";
            ErrorDescriptionEng[311] = "";

            ErrorDescriptionEng[315] = "Max (One carrier) Insert Not Picked from Unload Station";
            ErrorDescriptionEng[316] = "Max (In Total) Insert Not Picked from Unload Station";
            ErrorDescriptionEng[317] = "Vision not recognize number of inserts as expected";
            ErrorDescriptionEng[318] = "Too many empty pockets in One Carrier. Index Table Paused";
            ErrorDescriptionEng[319] = "Too many empty pockets in Total,Reset Missed counter before continue . Index Table Paused";           
            ErrorDescriptionEng[320] = "Full Carrier. No empty pockets";

            ErrorDescriptionEng[321] = "";
            ErrorDescriptionEng[322] = "";
            ErrorDescriptionEng[323] = "";
            ErrorDescriptionEng[324] = "";
            ErrorDescriptionEng[325] = "";
            ErrorDescriptionEng[326] = "";
            ErrorDescriptionEng[327] = "";
            ErrorDescriptionEng[328] = "";
            ErrorDescriptionEng[329] = "";
            ErrorDescriptionEng[330] = "";
            ErrorDescriptionEng[331] = "";
            ErrorDescriptionEng[332] = "";
            ErrorDescriptionEng[333] = "";
            ErrorDescriptionEng[334] = "";
            ErrorDescriptionEng[335] = "";
            ErrorDescriptionEng[336] = "";
            ErrorDescriptionEng[337] = "";
            //Robots:
            ErrorDescriptionEng[401] = "Robot Door Opened! Please Close and press Resume";              //Shafir
            ErrorDescriptionEng[402] = "Robot Stoped! Pick or Place Station Not Ready";                     //Shafir
            ErrorDescriptionEng[403] = "Target out of Tray/Carrier Rang";                               //Shafir
            ErrorDescriptionEng[404] = "Illegal digital signal";                                        //adept error
            ErrorDescriptionEng[405] = "Target Position too close to origen";                           //adept error
            ErrorDescriptionEng[406] = "Robot collision detected";                                      //adept error
            ErrorDescriptionEng[407] = "Robot Position Out of Range";                                   //adept error
            ErrorDescriptionEng[408] = "Positive overtravel (robot Motor), Robot power disabled";       //adept error
            ErrorDescriptionEng[409] = "";
            ErrorDescriptionEng[410] = "";
            ErrorDescriptionEng[411] = "";
            ErrorDescriptionEng[412] = "";
            ErrorDescriptionEng[413] = "";
            ErrorDescriptionEng[414] = "";
            ErrorDescriptionEng[415] = "";
            //Vision:
            ErrorDescriptionEng[501] = "Foult at Orientation Calculation, check Camera condition, or carrier upside down";
            ErrorDescriptionEng[502] = "Problem locating center hole";
            ErrorDescriptionEng[503] = "Search Index Exceeded Last Index";
            ErrorDescriptionEng[504] = "";
            ErrorDescriptionEng[505] = "";
            ErrorDescriptionEng[506] = "Camera field of view is Disturbed";


            //***** Main Control Error Discription *****
            ErrorDescriptionHeb[1] = "001 : תקלת מחזור";
            ErrorDescriptionHeb[2] = "002: מידע בשולחן טעינה/פריקה לא מאפשר המשך מחזור טעינה/פריקה";
            ErrorDescriptionHeb[3] = "003: תקלה בשינוי אופן עבודת שולחן סובב";
            ErrorDescriptionHeb[4] = "004: מחזור טעינה נתקל בתקלה";
            ErrorDescriptionHeb[5] = "005: מחזור מתעכב - רובוטים לא סיימו הנחה";
            ErrorDescriptionHeb[6] = "006: מכונה לא עברה למצב בדיקת השחזה";
            ErrorDescriptionHeb[7] = "007: מכונה עדיין במצב בדיקת השחזה";
            ErrorDescriptionHeb[8] = "008 : זמן מקסימאלי לצעד יחיד חלף";

            ErrorDescriptionHeb[15] = "015: לחצן חרום הופעל";
            ErrorDescriptionHeb[16] = "016: לחץ אוויר נמוך";
            ErrorDescriptionHeb[17] = "017: דלת רובוט טעינה נפתחה";
            ErrorDescriptionHeb[18] = "018: דלת רובוט פריקה נפתחה";
            ErrorDescriptionHeb[19] = "019: תקלה במכונת שטאלי";
            ErrorDescriptionHeb[20] = "019: אוטומציה בעצירה";
            //***** Load Service Tray Error Descriptions *****
            ErrorDescriptionHeb[101] = "101: אין תגובה ממגש תהליך";
            ErrorDescriptionHeb[102] = "102: זמן החלפת מגש גדול מידי - בדוק גששים";
            ErrorDescriptionHeb[103] = "103: לא ניתן לבצע טעינת שימות כל עוד שולחן סובב אינו במצב בטוח";
            ErrorDescriptionHeb[104] = "104: שגיאת vision מעכבת את מחזור הטעינה  (בדוק שהלול לא מונח הפוך)";
            ErrorDescriptionHeb[105] = "105: תקלת מחזור";
            ErrorDescriptionHeb[106] = "106: שימה לא נלקחה כראוי";
            ErrorDescriptionHeb[107] = "107: רובוט מסתיר את איזור הצילום";
            ErrorDescriptionHeb[108] = "108: מגש שימות לא נמצא בתחנת הטעינה";
            ErrorDescriptionHeb[109] = "109: מספר לול נוכחי גדול מכמות הלולים המתוכננת";
            ErrorDescriptionHeb[110] = "110: רובוט טעינה לא במקום בטוח - לא ניתן לסובב שולחן";
            ErrorDescriptionHeb[111] = "111: שולחן ממתין לשינוי מצב עבודה - בדוק בוכנת וידוא הנחה";
            ErrorDescriptionHeb[112] = "112: נמצאו שימות בלול לפני טעינתו";
            ErrorDescriptionHeb[113] = "113: איזורי הנחה ללא שימות התגלו לאחר טעינה";
            ErrorDescriptionHeb[114] = "114: קואורדינאטות שימה לא נשלחו לרובוט טעינה";
            ErrorDescriptionHeb[115] = "115: 3 שימות לא נטענו ברציפות - בדוק גריפר או מצלמות";

            ErrorDescriptionHeb[120] = "120: מערכת במצב ידני";
            ErrorDescriptionHeb[121] = "121: יש לאפשר לפחות אחת מהמעליות (A או B)";
            ErrorDescriptionHeb[122] = "122: מעלית איסוף ריקים (C) לא למטה";
            ErrorDescriptionHeb[123] = "123: ניגמרו המגשים במעליות הטעינה המאופשרות";
            ErrorDescriptionHeb[124] = "124: חריגת זמן לטעינת מגש חדש";
            ErrorDescriptionHeb[125] = "125: מגש עבודה לא עלה";
            ErrorDescriptionHeb[126] = "126:מגש עבודה לא ירד";
            ErrorDescriptionHeb[127] = "127: תפסני מגש קומה תחתונה במעלית A לא במצב חיצוני";
            ErrorDescriptionHeb[128] = "128: תפסני מגש קומה תחתונה במעלית B לא במצב חיצוני";
            ErrorDescriptionHeb[129] = "129: תפסני מגש קומה תחתונה במעלית A לא במצב פנימי";
            ErrorDescriptionHeb[130] = "130: תפסני מגש קומה תחתונה במעלית B לא במצב פנימי";
            ErrorDescriptionHeb[131] = "131: תפסני מגש קומה עליונה במעלית A לא במצב פנימי";
            ErrorDescriptionHeb[132] = "132: תפסני מגש קומה עליונה במעלית B לא במצב פנימי";
            ErrorDescriptionHeb[133] = "133: זיהוי גשש בטיחות";
            ErrorDescriptionHeb[134] = "134: ווסת תדר מסוע טעינה בתקלה";
            ErrorDescriptionHeb[135] = "135: ווסת תדר מעליות טעינה בתקלה";
            ErrorDescriptionHeb[136] = "136: זמן מינימלי להחלפת מגש לא חלף";
            //***** Index Table Error Descriptions *****
            ErrorDescriptionHeb[201] = "201: תקלת מחזור";
            ErrorDescriptionHeb[202] = "202: סיבוב שולחן נכשל";
            ErrorDescriptionHeb[203] = "203: לא ניתן לשנות אופן עבודת שולחן כאשר המערכת אינה באוטומט";

            ErrorDescriptionHeb[215] = "215: אין זיהוי גשש נועל לולים";
            ErrorDescriptionHeb[216] = "216: בוכנה 80 לא סגורה";
            ErrorDescriptionHeb[217] = "";  //tbd after Vadim done update PLC...
            ErrorDescriptionHeb[218] = "";
            ErrorDescriptionHeb[219] = "";
            ErrorDescriptionHeb[220] = "";
            ErrorDescriptionHeb[221] = "";
            ErrorDescriptionHeb[222] = "";
            ErrorDescriptionHeb[223] = "";
            ErrorDescriptionHeb[224] = "";
            ErrorDescriptionHeb[225] = "";
            ErrorDescriptionHeb[226] = "";
            ErrorDescriptionHeb[227] = "";
            ErrorDescriptionHeb[228] = "";
            ErrorDescriptionHeb[229] = "";
            ErrorDescriptionHeb[230] = "";
            ErrorDescriptionHeb[231] = "";
            ErrorDescriptionHeb[232] = "";

            ErrorDescriptionHeb[240] = "";
            ErrorDescriptionHeb[241] = "";
            ErrorDescriptionHeb[242] = "";
            ErrorDescriptionHeb[243] = "";
            ErrorDescriptionHeb[244] = "";
            ErrorDescriptionHeb[245] = "";
            ErrorDescriptionHeb[246] = "";
            ErrorDescriptionHeb[247] = "";

            ErrorDescriptionHeb[250] = "";
            ErrorDescriptionHeb[251] = "";
            ErrorDescriptionHeb[252] = "";
            ErrorDescriptionHeb[253] = "";
            ErrorDescriptionHeb[254] = "";
            ErrorDescriptionHeb[255] = "";
            ErrorDescriptionHeb[256] = "";
            ErrorDescriptionHeb[257] = "";
            ErrorDescriptionHeb[258] = "";

            ErrorDescriptionHeb[260] = "";
            ErrorDescriptionHeb[261] = "";
            ErrorDescriptionHeb[262] = "";
            ErrorDescriptionHeb[263] = "";
            ErrorDescriptionHeb[264] = "";
            ErrorDescriptionHeb[265] = "";
            ErrorDescriptionHeb[266] = "";
            ErrorDescriptionHeb[267] = "";
            ErrorDescriptionHeb[268] = "";
            ErrorDescriptionHeb[269] = "";
            ErrorDescriptionHeb[270] = "";
            ErrorDescriptionHeb[271] = "";
            ErrorDescriptionHeb[272] = "";
            ErrorDescriptionHeb[273] = "";
            ErrorDescriptionHeb[274] = "";
            ErrorDescriptionHeb[275] = "";
            ErrorDescriptionHeb[276] = "";
            ErrorDescriptionHeb[277] = "";
            ErrorDescriptionHeb[278] = "";

            ErrorDescriptionHeb[280] = "";
            ErrorDescriptionHeb[281] = "";
            ErrorDescriptionHeb[282] = "";
            ErrorDescriptionHeb[283] = "";
            ErrorDescriptionHeb[284] = "";
            ErrorDescriptionHeb[285] = "";

            ErrorDescriptionHeb[290] = "";
            ErrorDescriptionHeb[291] = "";
            ErrorDescriptionHeb[292] = "";
            ErrorDescriptionHeb[293] = "";

            ErrorDescriptionHeb[301] = "";
            ErrorDescriptionHeb[301] = "";
            ErrorDescriptionHeb[301] = "";
            ErrorDescriptionHeb[301] = "";
            ErrorDescriptionHeb[301] = "";

            ErrorDescriptionHeb[301] = "";
            ErrorDescriptionHeb[302] = "";
            ErrorDescriptionHeb[303] = "";
            ErrorDescriptionHeb[304] = "";
            ErrorDescriptionHeb[305] = "";
            ErrorDescriptionHeb[306] = "";
            ErrorDescriptionHeb[307] = "";
            ErrorDescriptionHeb[308] = "";
            ErrorDescriptionHeb[309] = "";
            ErrorDescriptionHeb[310] = "";
            ErrorDescriptionHeb[311] = "";

            ErrorDescriptionHeb[315] = "315: מקס' שימות לא נפרקו ברציפות - בדוק גריפר או מצלמות";
            ErrorDescriptionHeb[316] = "316: מקס' שימות נגרפו או עומדות להיגרף לפסולת - בדוק גריפר או מצלמות";

            ErrorDescriptionHeb[320] = "";
            ErrorDescriptionHeb[321] = "";
            ErrorDescriptionHeb[322] = "";
            ErrorDescriptionHeb[323] = "";
            ErrorDescriptionHeb[324] = "";
            ErrorDescriptionHeb[325] = "";
            ErrorDescriptionHeb[326] = "";
            ErrorDescriptionHeb[327] = "";
            ErrorDescriptionHeb[328] = "";
            ErrorDescriptionHeb[329] = "";
            ErrorDescriptionHeb[330] = "";
            ErrorDescriptionHeb[331] = "";
            ErrorDescriptionHeb[332] = "";
            ErrorDescriptionHeb[333] = "";
            ErrorDescriptionHeb[334] = "";
            ErrorDescriptionHeb[335] = "";
            ErrorDescriptionHeb[336] = "";
            ErrorDescriptionHeb[337] = "";
            //***** Robots Error Description *****
            ErrorDescriptionHeb[401] = "401: אין תקשורת עם רובוט";
            ErrorDescriptionHeb[402] = "402: תקלה בשליחת קואורדינטות לרובוט";
            ErrorDescriptionHeb[403] = "403: מיקום רובוט טעינה מחוץ למעטפת הבטיחות";
            ErrorDescriptionHeb[404] = "404: מיקום רובוט פריקה מחוץ למעטפת הבטיחות";
            ErrorDescriptionHeb[405] = "405: מיקום רובוט טעינה מבוקש מחוץ לתחום";
            ErrorDescriptionHeb[406] = "406: מיקום רובוט פריקה מבוקש מחוץ לתחום";
            ErrorDescriptionHeb[407] = "407: מיקום מבוקש קרוב מידי לראשית";
            ErrorDescriptionHeb[408] = "408: דלת רובוט טעינה נפתחה - מכונה נעצרה";
            ErrorDescriptionHeb[409] = "409: דלת רובוט פריקה נפתחה - מכונה נעצרה";
            ErrorDescriptionHeb[410] = "410: לא ניתן לשלוח פקודות לרובוט טעינה כאשר דלת בטיחות פתוחה";
            ErrorDescriptionHeb[411] = "411: לא ניתן לשלוח פקודות לרובוט פריקה כאשר דלת בטיחות פתוחה";
            ErrorDescriptionHeb[412] = "412: רובוט טעינה ממתין לתנאי בטיחות";
            ErrorDescriptionHeb[413] = "413: רובוט פריקה ממתין לתנאי בטיחות";
            ErrorDescriptionHeb[414] = "414: תקלה בלקיחת שימה מלול פריקה";
            ErrorDescriptionHeb[415] = "415: תקלה בלקיחת שימה ממגש טעינה";
            ErrorDescriptionHeb[416] = "416: תקלת רובוט - וודא דלת סגורה ותקשורת תקינה";
            //***** Cameras Error Description *****
            ErrorDescriptionHeb[501] = "501: תקלה במציאת אוריינטציה -מרכז לול בסטייה של יותר מ-5 מילימטר - בדוק תנאי צילום";
            ErrorDescriptionHeb[502] = "502: שימה לא נמצאה במקומה לאחר הנחה";
            ErrorDescriptionHeb[503] = "503: אינדקס חיפוש גדול ממספר השימה האחרונה";
            ErrorDescriptionHeb[504] = "504: שימה לא נמצאה במגש המקור";
            ErrorDescriptionHeb[505] = "505: נמצאו שימות בלול לפני טעינתו";
            ErrorDescriptionHeb[506] = "506: איזור צילום מוסתר";
        }

        //members:
        public string[] ErrorDescriptionEng { get; set; }
        public string[] ErrorDescriptionHeb { get; set; }
    }
}
