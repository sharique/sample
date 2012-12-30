using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data
{
    class Project
    {
        public virtual int ProjectId { get; set; }
        public virtual string ProjectName { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
    }
}
