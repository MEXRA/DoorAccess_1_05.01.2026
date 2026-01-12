using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorAccess_1
{
    public static class Session
    {
        public static string CurrentUser { get; set; } = "Unknown";
        public static AppRole CurrentRole { get; set; } = AppRole.User;
    }
}

