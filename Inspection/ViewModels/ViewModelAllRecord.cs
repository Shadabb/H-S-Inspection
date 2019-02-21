using System.Collections.Generic;
using Inspection.Models;

namespace Inspection.ViewModels
{
    public class ViewModelAllRecord
    {
        public List<MasterDetails> MasterDetails { get; set; }
        public List<Models.Inspection> Inspection { get; set; }
    }
}