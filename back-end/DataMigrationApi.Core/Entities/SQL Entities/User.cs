﻿using System;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Entities.SQL_Entities
{
    public class User : IEntity<string>
    {
        public string ID { get; set; }
        public int Identity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
    }
}
