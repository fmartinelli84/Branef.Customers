using System;
using System.Collections.Generic;
using System.Text;

namespace Branef.Framework.Data
{
    public abstract class BaseTrackableDto : BaseReadOnlyTrackableDto, ITrackableDto
    {
        public DateTime? ModifiedAtDate { get; set; }
    }
}
