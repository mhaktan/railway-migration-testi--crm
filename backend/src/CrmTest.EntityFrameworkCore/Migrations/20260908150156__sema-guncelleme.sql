-- Migration 20260908150156 — sema-guncelleme
-- Provider: postgresql
ALTER TABLE "Customers" ADD COLUMN "City" varchar(100) NULL;
