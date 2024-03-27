﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Branef.Framework.Logging
{
    public class SeqOptions
    {
        public Uri Address { get; set; } = null!;

        public long? EventBodyLimitBytes { get; set; }
    }
}
