using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sinistrius.CalendrierRepublicain;


internal class RepublicanDateValidator
{

    internal static ValidationResult IsValid(int era)
    {
        if (era == 1)
        {
            return new ValidationResult(true);
        }
        else
        {
            return new ValidationResult(false, "The era must be 1.");
        }
    }

}
