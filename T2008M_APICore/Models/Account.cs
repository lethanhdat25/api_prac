using System;
using System.Collections.Generic;

#nullable disable

namespace T2008M_APICore.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
    }
}
