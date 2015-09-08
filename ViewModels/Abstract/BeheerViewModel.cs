using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    abstract class BeheerViewModel : WorkspaceViewModel
    {
        protected ISIS_DataEntities ctx;
        public CollectionViewSource ViewSource { get; protected set; }

        #region Commands
        public NextCommand NextCommandEvent { get; private set; }
        public PreviousCommand PreviousCommandEvent { get; private set; }
        public SaveCommand SaveCommandEvent { get; private set; }
        public DeleteCommand DeleteCommandEvent { get; private set; }
        public RefreshCommand RefreshCommandEvent { get; private set; }
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

        abstract public bool IsValid { get; }

        public BeheerViewModel()
        {
            ViewSource = new CollectionViewSource();
            NextCommandEvent = new NextCommand(this);
            PreviousCommandEvent = new PreviousCommand(this);
            SaveCommandEvent = new SaveCommand(this);
            DeleteCommandEvent = new DeleteCommand(this);
            RefreshCommandEvent = new RefreshCommand(this);
            ButtonToevoegenContent = "Toevoegen";
        }

        public abstract void Delete();
        public abstract void Refresh();
        public abstract void Add();
        public abstract void SaveChanges();
        protected abstract void View_CurrentChanged(object sender, EventArgs e);
    }
}
