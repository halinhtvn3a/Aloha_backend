﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AlohaVietnam.Repositories.Models;

public partial class UserSubscription
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public int SubscriptionPlanId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; }

    public virtual SubscriptionPlan SubscriptionPlan { get; set; }
}