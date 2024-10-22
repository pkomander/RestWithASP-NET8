﻿using RestWithASPNET.Hypermedia;
using RestWithASPNET.Hypermedia.Abstract;
using System.ComponentModel.DataAnnotations;

namespace RestWithASPNET.DTO.PersonDto
{
    public class ReadPersonDto : ISupporteHyperMedia
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
