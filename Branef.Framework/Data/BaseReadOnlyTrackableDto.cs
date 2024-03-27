using System;
using System.Collections.Generic;
using System.Text;

namespace Branef.Framework.Data
{
    public abstract class BaseReadOnlyTrackableDto : IReadOnlyTrackableDto
    {
        public DateTime? CreatedAtDate { get; set; }
    }
}
