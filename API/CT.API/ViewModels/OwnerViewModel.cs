using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CT.API.ViewModels
{
    public class OwnerViewModel
    {
        public OwnerViewModel()
        {
            Pets = new List<PetViewModel>();
        }

        public string Gender { get; set; }

        public virtual ICollection<PetViewModel> Pets { get; set; }

    }
}
