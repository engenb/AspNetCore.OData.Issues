using System;
using System.Collections.Generic;

namespace Issue698_4_1_1_Working.Models
{
    public class Foo
    {
        public Guid Id { get; set; }

        public IEnumerable<Bar> Bars { get; set; } = new List<Bar>();
    }
}
