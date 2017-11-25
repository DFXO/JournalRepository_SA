IF  NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'JournalsDB')
CREATE DATABASE [JournalsDB]