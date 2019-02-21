using Inspection.Models;
using System.Collections.Generic;

namespace Inspection.ViewModels
{
    public class InpectionViewModel
    {
        public Inspection.Models.Inspection Inspections { get; set; }
        public List<InspectionCompliant> InspectionCompliantData { get; set; }
        public List<AuditInspectionDetail> InspectionAuditViewModels { get; set; }

    }
  
}