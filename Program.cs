
using System;
using HospitalManagementSystem.Helpers;

try
{
    await using var connection = DatabaseHelper.CreateConnection();

    await connection.OpenAsync();

    Console.WriteLine("Hospital Management System");
    Console.WriteLine("SQL Server connection successful!");
    Console.WriteLine($"Connected database: {connection.Database}");
}
catch (Exception ex)
{
    Console.WriteLine("Database connection failed!");
    Console.WriteLine(ex.Message);
}
