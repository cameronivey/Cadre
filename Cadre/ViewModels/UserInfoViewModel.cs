using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cadre.ViewModels
{
    public class UserInfoViewModel
    {
        public UserInfoViewModel()
        {

        }

        public UserInfoViewModel(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}