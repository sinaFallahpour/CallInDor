using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Account
{
    public class ProfileGetDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ImageAddress { get; set; }

        public List<UserFiledsDTO> UsersFields { get; set; }
    }
}
