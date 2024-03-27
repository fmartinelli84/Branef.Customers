using System;
using System.Collections.Generic;
using System.Text;

namespace Branef.Framework.Data
{
    public interface ITrackableEntity : IReadOnlyTrackableEntity
    {
        DateTime? ModifiedAtDate { get; set; }
    }
}
