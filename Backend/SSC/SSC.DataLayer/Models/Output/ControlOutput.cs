using SSC.Database.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.DataLayer.Models.Output
{
    public class ControlOutput
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Style { get; set; }
        public string Hint { get; set; }
        public string Placeholder { get; set; }
        public int? MaxLength { get; set; }
        public string SuffixIcon { get; set; }
        public string PrefixIcon { get; set; }
        public ControlOutput()
        {

        }
        public ControlOutput(BaseControl baseControl)
        {
            Id = baseControl.Id;
            Hint = baseControl.Hint;
            MaxLength = baseControl.MaxLength;
            Placeholder = baseControl.Placeholder;
            PrefixIcon = baseControl.PrefixIcon;
            Style = baseControl.Style;
            SuffixIcon = baseControl.SuffixIcon;
            Title = baseControl.Title;
        }
    }
}
