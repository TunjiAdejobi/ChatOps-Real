using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Model.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }
        public int OccupationId { get; set; }
        public Occupation Occupation { get; set; }
        public DateTime SignUpDate { get; set; }
        public bool IsActive { get; set; }
            //= false;
    }
    public class UserProfileRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }
        public int OccupationId { get; set; }
        public DateTime SignUpDate { get; set; }
    }
    public class UserProfileRequestMappingConfig : Profile
    {
        public UserProfileRequestMappingConfig()
        {
            CreateMap<UserProfile, UserProfileRequest>().ReverseMap();
        }
    }
}
