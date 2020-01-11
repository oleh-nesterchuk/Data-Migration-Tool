using System;

namespace DataMigrationApi.Core.Entities
{
    public static class UserExtensions
    {
        public static User CalculateAge(this User user)
        {
            var age = DateTime.Today.Year - user.BirthDate.Year;
            if (user.BirthDate.Date.AddYears(age) > DateTime.Today)
            {
                --age;
            }
            user.Age = age;
            return user;
        }
    }
}
