using SSC.Core.Base.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSC.Database.Entity
{
    public class ValueFilled : IGuidIdentity
    {
        [Key]
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }

    }
}
