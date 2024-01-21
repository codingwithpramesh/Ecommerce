using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.MobileApp.Models
{
    public class Todo : INotifyPropertyChanged
    {
        int _id;

        public int Id
        {
            get => _id;
            set
            {
                if (_id == value) {
                    return;

                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }


        string _todoname;
        public string TodoName { get => _todoname; 
            set{
                if(_todoname == value)
                {
                    return;

                _todoname = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(TodoName)));
                }
            } }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
