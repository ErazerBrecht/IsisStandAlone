using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIS.Models
{
    partial class Datum : IDataErrorInfo
    {
        public bool CanSave
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    if (GetValidationError(property) != null) // there is an error
                        return false;
                }

                return true;
            }
        }

        #region Data Validation

        private static readonly string[] ValidatedProperties =
        {
            "Strijker1",
            "Strijker2",
            "Strijker3",
            "Strijker4",
            "Strijker5"
        };

        public string GetValidationError(string propertyName)
        {
            string result = null;

            switch (propertyName)
            {
                case "Strijker1":
                    {
                        if (Strijker1 == null)
                            result = "Een prestatie heeft minimum één strijkster nodig!";
                        break;
                    }

                case "Strijker2":
                    {
                        if ((Strijker2 != null) && Strijker2.ID == Strijker1?.ID)
                            result = "Deze strijkster is al toegevoegd!";
                        break;
                    }

                case "Strijker3":
                    {
                        if ((Strijker3 != null) && (Strijker3.ID == Strijker1?.ID || Strijker3.ID == Strijker2?.ID))
                            result = "Deze strijkster is al toegevoegd!";
                        break;
                    }
                case "Strijker4":
                    {
                        if ((Strijker4 != null) && (Strijker4.ID == Strijker1?.ID || Strijker4.ID == Strijker2.ID || Strijker4.ID == Strijker3?.ID))
                            result = "Deze strijkster is al toegevoegd!";
                        break;
                    }
                case "Strijker5":
                    {
                        if ((Strijker5 != null) && (Strijker5.ID == Strijker1?.ID || Strijker5.ID == Strijker2?.ID || Strijker5.ID == Strijker3?.ID || Strijker5.ID == Strijker4?.ID))
                            result = "Deze strijkster is al toegevoegd!";
                        break;
                    }
            }

            return result;
        }

        #endregion

        #region Interface Implementation

        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {
            get { return GetValidationError(columnName); }
        }

        #endregion
    }
}