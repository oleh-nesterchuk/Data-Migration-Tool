using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataMigrationApi.Core.Entities
{
    public class User : IEntity<string>
    {
        public string ID { get; set; }

        public int Identity { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [BirthDate]
        public DateTime BirthDate { get; set; }

        public int Age { get; set; }

        public virtual List<Email> Emails { get; set; }
    }
}
