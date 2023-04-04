using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Domain.Common
{
    public abstract class BaseEntiy
    {
        
        public virtual int Id { get; set; }
    }
}
