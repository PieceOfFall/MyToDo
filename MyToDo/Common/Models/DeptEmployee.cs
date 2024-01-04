using Prism.Mvvm;

namespace MyToDo.Common.Models
{
    public class DeptEmployee : BindableBase
    {

        private string _empName;

        public string empName 
        { 
            get
            {
                return _empName;
            }
            set
            {
                _empName = value;
                RaisePropertyChanged();
            }
        }
    }
}
