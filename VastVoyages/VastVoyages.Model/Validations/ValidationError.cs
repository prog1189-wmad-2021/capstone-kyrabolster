using VastVoyages.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VastVoyages.Model
{
    public class ValidationError
    {
        public ValidationError(string desc, ErrorType errorType)
        {
            Description = desc;
            ErrorType = errorType;
        }

        public string Description { get; set; }
        public ErrorType ErrorType { get; set; }

    }
}
