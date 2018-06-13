using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AskME.Core.Entities.Enum
{
    public enum QuestionStatus
    {
        [Description("正常")]
        OK,
        [Description("被举报")]
        Reported,
        [Description("删除")]
        Delete
    }
}
