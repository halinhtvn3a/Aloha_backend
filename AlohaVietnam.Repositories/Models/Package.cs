﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using AlohaVietnam.Repositories.Domains;
using System;
using System.Collections.Generic;

namespace AlohaVietnam.Repositories.Models;

public partial class Package : EntityBase<int>
{

    public int CityId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsFree { get; set; }

    public decimal? Price { get; set; }

    public virtual City City { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
}