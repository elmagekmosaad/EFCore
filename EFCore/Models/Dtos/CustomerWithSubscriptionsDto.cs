﻿using EFCore.Data.Models;
using EFCore.MySQL.Models.Dto;

namespace EFCore.Models.Dtos
{
    public class CustomerWithSubscriptionsDto:CustomerDto
    {
        public ICollection<Subscription> Subscriptions { get; set; }

    }
}