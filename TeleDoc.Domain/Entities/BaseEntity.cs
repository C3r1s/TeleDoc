﻿using System.ComponentModel.DataAnnotations;

namespace TeleDoc.Domain.Entities;

public abstract class BaseEntity
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}