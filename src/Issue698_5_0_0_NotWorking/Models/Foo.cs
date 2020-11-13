using System;
using System.Collections.Generic;

namespace Issue698_5_0_0_NotWorking.Models
{
    public class Foo
    {
        public Guid Id { get; set; }

        public IEnumerable<Bar> Bars { get; set; } = new List<Bar>();
    }
}
