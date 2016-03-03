using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EF_Model
{
    public partial class TijdPrestatie : IDataErrorInfo
    {
        //Here I'll add properties that doesn't need to be saved in the database!
        //Because the value of these properties can be calculated out of the existing fields in the db!

        public void AddParameters(Parameters p)
        {
            ParameterHemden = Convert.ToDecimal(p.ParameterHemden);
            ParameterLakens1 = Convert.ToDecimal(p.ParameterLakens1);
            ParameterLakens2 = Convert.ToDecimal(p.ParameterLakens2);
            ParameterAndereStrijk = p.ParameterAndereStrijk;
        }


        [NotMapped]
        public int TotaalMinuten { get; set; }
        [NotMapped]
        public int TotaalStrijk { get; set; }
        [NotMapped]
        public int TotaalHemden { get; set; }
        [NotMapped]
        public int TotaalLakens1 { get; set; }
        [NotMapped]
        public int TotaalLakens2 { get; set; }
        [NotMapped]
        public int TotaalAndereStrijk { get; set; }

        public void CalculateStrijk()
        {
            TotaalHemden = (int)Math.Ceiling(Convert.ToByte(AantalHemden) * ParameterHemden);
            TotaalLakens1 = (int)Math.Ceiling(Convert.ToByte(AantalLakens1) * ParameterLakens1);
            TotaalLakens2 = (int)Math.Ceiling(Convert.ToByte(AantalLakens2) * ParameterLakens2);
            TotaalAndereStrijk = (int)Math.Ceiling(Convert.ToByte(TijdAndereStrijk) * ParameterAndereStrijk);
            TotaalStrijk = Convert.ToInt32(AantalHemden + AantalLakens1 + AantalLakens2 + AantalAndereStrijk);

            //Calculate the "administratie" time
            if (TotaalStrijk < 20)
                TijdAdministratie = 5;
            else if (TotaalStrijk < 40)
                TijdAdministratie = 10;
            else if (TotaalStrijk < 80)
                TijdAdministratie = 15;
            else
                TijdAdministratie = 20;

            TotaalMinuten = Convert.ToInt32(TotaalHemden + TotaalLakens1 + TotaalLakens2 + TotaalAndereStrijk + TijdAdministratie);
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
            "AantalHemden",
            "AantalLakens1",
            "AantalLakens2",
            "AantalAndereStrijk",
            "TijdAndereStrijk"
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
