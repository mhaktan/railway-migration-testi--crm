-- Migration 20260908145333 — sema-guncelleme
-- Provider: postgresql
ALTER TABLE "Customers" ADD COLUMN "Phone" varchar(50) NULL;
