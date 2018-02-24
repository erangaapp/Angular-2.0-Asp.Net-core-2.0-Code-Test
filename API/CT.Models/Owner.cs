using System;
using System.Collections.Generic;
using System.Text;

using CT.Models.Base;
using CT.Models.Enum;

namespace CT.Models
{
    public class Owner : BaseEntity
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}
