﻿namespace UserManagement.Application.DTOs;

public class MenuItemDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}