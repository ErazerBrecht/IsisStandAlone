using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ISIS.Commands;
using ISIS.Services;
using System.Windows;

namespace ISIS.ViewModels
{
    abstract class BeheerExtendViewModel : BeheerViewModel
    {
        public CollectionViewSource ViewSource { get; protected set; }

        #region Commands
        public NextCommand NextCommandEvent { get; private set; }
        public PreviousCommand PreviousCommandEvent { get; private set; }
        public NextErrorCommand NextErrorCommandEvent { get; private set; }
        #endregion

        #region ButtonToevoegenContent
        protected string _buttonToevoegenContent;

        public virtual string ButtonToevoegenContent
        {
            get { return _buttonToevoegenContent; }
            set
            {
                _buttonToevoegenContent = value;
                NoticeMe("ButtonToevoegenContent");
                NoticeMe("IsIdReadOnly");
            }
        }
        #endregion

        public bool IsIdReadOnly
        {
            get
            {
                if (ButtonToevoegenContent == "Annuleren")
                    return false;
                return true;
            }
        }


        public BeheerExtendViewModel()
        {
            ViewSource = new CollectionViewSource();
            NextCommandEvent = new NextCommand(this);
            PreviousCommandEvent = new PreviousCommand(this);
            NextErrorCommandEvent = new NextErrorCommand(this);
            ButtonToevoegenContent = "Toevoegen";
        }

        public abstract void SetErrorAsSelected();

        protected abstract void View_CurrentChanged(object sender, EventArgs e);
    }
}
