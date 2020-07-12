using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Core.Applicant
{
    public class Applicant
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}