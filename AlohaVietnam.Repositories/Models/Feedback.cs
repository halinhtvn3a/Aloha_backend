﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AlohaVietnam.Repositories.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public int PackageId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Package Package { get; set; }
}