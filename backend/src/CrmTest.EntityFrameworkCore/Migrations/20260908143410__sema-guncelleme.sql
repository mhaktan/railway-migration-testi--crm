-- Migration 20260908143410 — sema-guncelleme
-- Provider: postgresql
ALTER TABLE "Customer" ADD COLUMN "Phone" varchar(50) NULL;
