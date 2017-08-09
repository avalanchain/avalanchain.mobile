using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Share;

namespace avalanchain
{
    public class CommonService
    {
        public static async Task<bool> CopyToClipboard(string text)
        {
            if (SampleData.IsSupportClipboard)
                return false;

            return await CrossShare.Current.SetClipboardText(text);
        }
    }
}
