using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CrmTest.EntityFrameworkCore.Migrations
{
    /// <summary>
    /// Archipid şema migration'ları. Publish sırasında ER modelinin bir önceki
    /// sürümüyle karşılaştırılarak üretilir — elle düzenlenmemelidir.
    /// </summary>
    public static class SchemaMigrations
    {
        public sealed class Migration
        {
            public string Version { get; }
            public string Name { get; }
            public string Sql { get; }
            public Migration(string version, string name, string sql)
            {
                Version = version; Name = name; Sql = sql;
            }
        }

        private static readonly List<Migration> All = new List<Migration>
        {
            new Migration(@"20260908145333", @"sema-guncelleme", @"-- Migration 20260908145333 — sema-guncelleme
-- Provider: postgresql
ALTER TABLE ""Customers"" ADD COLUMN ""Phone"" varchar(50) NULL;
"),
        };

        private const string VersionTableSql = @"CREATE TABLE IF NOT EXISTS ""__ArchipidSchemaVersions"" (""Version"" text NOT NULL PRIMARY KEY, ""Name"" text NULL, ""AppliedAt"" timestamp NOT NULL);";

        /// <summary>
        /// <paramref name="freshlyCreated"/> true ise şema EnsureCreated ile zaten en
        /// güncel hâlde kurulmuştur; migration'lar çalıştırılmaz, sadece uygulanmış
        /// olarak kaydedilir (baseline).
        /// </summary>
        public static void Apply(DbContext db, bool freshlyCreated)
        {
            db.Database.ExecuteSqlRaw(VersionTableSql);

            var applied = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var conn = db.Database.GetDbConnection();
            var wasClosed = conn.State != System.Data.ConnectionState.Open;
            if (wasClosed) conn.Open();
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = QuoteFor("SELECT \"Version\" FROM \"__ArchipidSchemaVersions\"");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) applied.Add(reader.GetString(0));
                    }
                }
            }
            finally
            {
                if (wasClosed) conn.Close();
            }

            var pending = All.Where(m => !applied.Contains(m.Version)).OrderBy(m => m.Version, StringComparer.Ordinal).ToList();
            if (pending.Count == 0)
            {
                Console.WriteLine("[Migration] Sema guncel — bekleyen migration yok.");
                return;
            }

            foreach (var m in pending)
            {
                if (freshlyCreated)
                {
                    // Sema zaten en guncel hali ile kuruldu — sadece isaretle.
                    Record(db, m);
                    continue;
                }

                Console.WriteLine($"[Migration] Uygulaniyor: {m.Version} — {m.Name}");
                foreach (var stmt in SplitStatements(m.Sql))
                {
                    db.Database.ExecuteSqlRaw(stmt);
                }
                Record(db, m);
                Console.WriteLine($"[Migration] Tamam: {m.Version}");
            }

            Console.WriteLine($"[Migration] {pending.Count} migration {(freshlyCreated ? "baseline olarak isaretlendi" : "uygulandi")}.");
        }

        private static void Record(DbContext db, Migration m)
        {
            db.Database.ExecuteSqlRaw(
                QuoteFor("INSERT INTO \"__ArchipidSchemaVersions\" (\"Version\", \"Name\", \"AppliedAt\") VALUES ({0}, {1}, {2})"),
                m.Version, m.Name, DateTime.UtcNow);
        }

        /// <summary>Tanimlayici kacisini provider'a cevirir (uretim aninda sabitlenir).</summary>
        private static string QuoteFor(string sql)
        {
            return sql;
        }

        /// <summary>
        /// Uretilen SQL basit ifadelerden olusur (fonksiyon/blok yok), bu yuzden
        /// ";" + satir sonu sinirinda bolmek guvenlidir.
        /// </summary>
        private static IEnumerable<string> SplitStatements(string sql)
        {
            return sql
                .Split(new[] { ";\n", ";\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                // Sadece yorum satirlarindan olusan parcalar calistirilmaz
                .Where(s => s.Split('\n').Any(line => line.Trim().Length > 0 && !line.TrimStart().StartsWith("--")))
                .Select(s => s.EndsWith(";") ? s : s + ";");
        }
    }
}
