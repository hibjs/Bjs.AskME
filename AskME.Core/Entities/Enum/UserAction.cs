using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AskME.Core.Entities.Enum
{
    public enum UserAction
    {
        [Description("允许操作")]
        Allow,
        [Description("允许规范操作")]
        Standard,
        [Description("禁止操作")]
        Forbind

    }
}
