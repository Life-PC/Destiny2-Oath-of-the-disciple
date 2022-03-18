using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destiny2_Vow_of_the_Disciple.lib
{
    public class TextCheck
    {
        static List<string> symbolText = new List<string>();

        public TextCheck()
        {
            string[] s = {
                "思い出す", "愛", "ピラミッド","飲み物","ワーム",
                "ブラックガーデン","タワー","亜空間","スコーン","暗黒",
                "授ける","光","キル","停止","ハイブ","ガーディアン",
                "目撃者","サバスン","トラベラー","ブラックハート",
                "崇拝","接触","入る","地球","艦隊","嘆き"
            };
            symbolText.AddRange(s);
        }
        public void checkAndSend(string text)
        {
            if (symbolText.Contains(text))
            {
                SendKeys.SendWait("{ENTER}");
                SendKeys.SendWait(text);
                SendKeys.SendWait("{ENTER}");
            }
        }
    }
}
