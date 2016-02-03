using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyKeys.UserInterface.Designer.Charts
{
    /// <summary>
    /// Implements fake designer collection for SeriesPoints
    /// </summary>
    public class SeriesPointCollection : List<SeriesPoint>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesPointCollection"/> class.
        /// </summary>
        public SeriesPointCollection()
            : base()
        {
        }
    }
}
