using System;
using System.Collections.Generic;
using System.Text;

namespace Branef.Framework.Data
{
    public interface ITrackableDto : IReadOnlyTrackableDto
    {
        DateTime? ModifiedAtDate { get; set; }
    }
}
