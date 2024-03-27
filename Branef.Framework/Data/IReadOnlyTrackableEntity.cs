using System;
using System.Collections.Generic;
using System.Text;

namespace Branef.Framework.Data
{
    public interface IReadOnlyTrackableEntity
    {
        DateTime? CreatedAtDate { get; set; }
    }
}
