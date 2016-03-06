using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Model
{
    partial class StukPrestatie : IDataErrorInfo
    {
        public override void CalculatePrestatie()
        {
            CalculateStrijk();

            if (TotaalDienstenChecks == 0)
                NieuwTegoed = Tegoed - TotaalMinuten;
            else
                NieuwTegoed = (TotaalDienstenChecks * 60) - TotaalBetalen;
        }

        public override byte RecalculatePrestatie(byte newTegoed)
        {
            NieuwTegoed = newTegoed;
            CalculateStrijk();
            return Tegoed;
        }

        private void CalculateStrijk()
        {
            if (TotaalMinuten > Tegoed)
            {
                TotaalBetalen = TotaalMinuten - Tegoed;
                TotaalDienstenChecks = Convert.ToByte(Math.Ceiling(TotaalBetalen / 60.0));
            }
        }

        public bool CanSave
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    if (GetValidationError(property) != null)   // there is an error
                        return false;
                }

                return true;
            }
        }

        #region Data Validation
        static readonly string[] ValidatedProperties =
        {
            "TotaalMinuten"
        };

        public string GetValidationError(string propertyName)
        {
            string result = null;

            var validationResults = new List<ValidationResult>();

            Validator.TryValidateProperty(
                GetType().GetProperty(propertyName).GetValue(this),
                new ValidationContext(this)
                {
                    MemberName = propertyName
                }, validationResults);

            if (validationResults.Count > 0)
                result = validationResults.First().ErrorMessage;

            //else
            //    switch (propertyName)
            //    {
            //    }

            return result;
        }
        #endregion

        #region Interface Implementation
        public string Error => "";
        public string this[string columnName] => GetValidationError(columnName);

        #endregion
    }
}
