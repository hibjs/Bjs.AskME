using AskME.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskME.Infrastructure.Common
{
    public class UUID : IUUID
    {
        public string CreateUUID()
        {
            Random R = new Random();
            string strDateTimeNumber = DateTime.Now.ToString("yyyyMMddHHmmssms");
            string strRandomResult = R.Next(1, 1000).ToString();
            return strDateTimeNumber + strRandomResult;
        }
    }
}
