using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.DataLayer.Models.Output
{
    public class FormTemplateOutput
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public IEnumerable<ControlOutput> Controls { get; set; }
        public IEnumerable<ControlOutput> TableControls { get; set; }
    }
}
