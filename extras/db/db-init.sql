USE [master]
GO

IF DB_ID('VanillaDB') IS NOT NULL
  set noexec on               -- prevent creation when already exists

CREATE DATABASE VanillaDB;
GO

USE VanillaDB
GO