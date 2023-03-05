using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWize.Utils.KeyboardBugs
{
    
    public    class KeyboardBugsFix
    {
        public static void HideKeyboard()
        {
        #if ANDROID
            if (Platform.CurrentActivity.CurrentFocus != null)
	        {
                Platforms.Android.KeyboardHelper.HideKeyboard();
	        }
        #endif

        }
    }
}
