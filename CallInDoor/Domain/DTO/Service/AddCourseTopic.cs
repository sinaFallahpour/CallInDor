using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Service
{
    public class AddCourseTopic
    {

        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "TopicName")]
        public string TopicName { get; set; }



        public bool IsFreeForEveryOne { get; set; }

        //public string FileAddress { get; set; }

        public IFormFile File { get; set; }


    }
}
