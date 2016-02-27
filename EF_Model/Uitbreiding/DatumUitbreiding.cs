using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EF_Model
{
    partial class Datum : IDataErrorInfo
    {
        public bool CanSave => ValIdatedProperties.All(property => GetValIdationError(property) == null);

        #region Data ValIdation

        private static readonly string[] ValIdatedProperties =
        {
            "Date",
            "Strijker1",
            "Strijker2",
            "Strijker3",
            "Strijker4",
            "Strijker5"
        };

        public string GetValIdationError(string propertyName)
        {
            string result = null;

            switch (propertyName)
            {
                case "Date":
                {
                    if(Date > DateTime.Now)
                            result = "Een prestatie kan niet in de toekomst plaats vinden!";
                    break;
                }
                case "Strijker1":
                    {
                        if (Strijker1 == null)
                            result = "Een prestatie heeft minimum één strijkster nodig!";
                        break;
                    }

                case "Strijker2":
                    {
                        if ((Strijker2 != null) && Strijker2.Id == Strijker1?.Id)
                            result = "Deze strijkster is al toegevoegd!";
                        break;
                    }

                case "Strijker3":
                    {
                        if ((Strijker3 != null) && (Strijker3.Id == Strijker1?.Id || Strijker3.Id == Strijker2?.Id))
                            result = "Deze strijkster is al toegevoegd!";
                        break;
                    }
                case "Strijker4":
                    {
                        if ((Strijker4 != null) && (Strijker4.Id == Strijker1?.Id || Strijker4.Id == Strijker2?.Id || Strijker4.Id == Strijker3?.Id))
                            result = "Deze strijkster is al toegevoegd!";
                        break;
                    }
                case "Strijker5":
                    {
                        if ((Strijker5 != null) && (Strijker5.Id == Strijker1?.Id || Strijker5.Id == Strijker2?.Id || Strijker5.Id == Strijker3?.Id || Strijker5.Id == Strijker4?.Id))
                            result = "Deze strijkster is al toegevoegd!";
                        break;
                    }
            }

            return result;
        }

        #endregion

        #region Interface Implementation

        public string Error => "";

        public string this[string columnName] => GetValIdationError(columnName);

        #endregion

        #region Stijkers
        [NotMapped]
        public Strijker Strijker1
        {
            get
            {
                if (Strijkers.Count > 0)
                    return this.Strijkers[0];
                return null;
            }
            set
            {
                if (Strijkers.Count < 1)
                    Strijkers.Add(value);
                this.Strijkers[0] = value;     
            }
        }
        [NotMapped]
        public Strijker Strijker2
        {
            get
            {
                if(Strijkers.Count > 1)
                    return this.Strijkers[1];
                return null;
            }
            set
            {
                if (Strijkers.Count < 2)
                    Strijkers.Add(value);

                this.Strijkers[1] = value;
                if (value == null)
                {
                    Strijker3 = null;
                    Strijker4 = null;
                    Strijker5 = null;
                }
            }
        }
        [NotMapped]
        public Strijker Strijker3
        {
            get
            {
                if (Strijkers.Count > 2)
                    return this.Strijkers[2];
                return null;
            }
            set
            {
                if (Strijkers.Count < 3)
                    Strijkers.Add(value);

                this.Strijkers[2] = value;
                if (value == null)
                {
                    Strijker4 = null;
                    Strijker5 = null;
                }
            }
        }
        [NotMapped]
        public Strijker Strijker4
        {
            get
            {
                if (Strijkers.Count > 3)
                    return this.Strijkers[3];
                return null;         
            }
            set
            {
                if (Strijkers.Count < 4)
                    Strijkers.Add(value);

                this.Strijkers[3] = value;
                if (value == null)
                {
                    Strijker5 = null;
                }
            }
        }
        [NotMapped]
        public Strijker Strijker5
        {
            get
            {
                if (Strijkers.Count > 4)
                    return this.Strijkers[4];
                return null;
            }
            set
            {
                if (Strijkers.Count < 5)
                    Strijkers.Add(value);

                this.Strijkers[4] = value; 
                
            }
        }
        #endregion
    }
}