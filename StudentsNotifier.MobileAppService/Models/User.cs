﻿using System;
namespace StudentsNotifier.MobileAppService.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string NotificationToken { get; set; }
    }
}
