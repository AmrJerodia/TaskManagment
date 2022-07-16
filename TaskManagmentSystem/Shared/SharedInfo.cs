using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public enum TasksType
    {
        [EnumMember]
        Ignored = 1,
        [EnumMember]
        Important = 2,
        [EnumMember]
        Average = 3,
        [EnumMember]
        Critical = 4,
    }

    public enum TasksStatus
    {
        [EnumMember]
        Created = 1,
        [EnumMember]
        Opened = 2,
        [EnumMember]
        Closed = 3,
        [EnumMember]
        Started = 4,

    }

    public enum CommentsType
    {
        [EnumMember]
        Info = 1,
        [EnumMember]
        Important = 2,
        [EnumMember]
        Pinned = 3,

    }
}
