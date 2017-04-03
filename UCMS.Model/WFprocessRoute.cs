using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Model
{
    public class WFProcessRoute
    {
        public string Id { get; set; }

        /// <summary>
        /// string Id of departure step of this route
        /// </summary>
        public string FromStepId { get; set; }

        /// <summary>
        /// string Id of destination step of this route
        /// </summary>
        public string ToStepId { get; set; }

        /// <summary>
        /// A valid value for executing of this route 
        /// </summary>
        public object RoutingParam { get; set; }
    }
}
