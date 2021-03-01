using System;
using System.Collections.Generic;

namespace Issue697.Models
{
    public class Foo
    {
        public Guid Id { get; set; }

        public IEnumerable<Bar> Bars { get; set; } = new List<Bar>();
    }
}
