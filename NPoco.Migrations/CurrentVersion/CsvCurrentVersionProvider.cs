using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NPoco.Migrations.CurrentVersion
{
    public class CsvCurrentVersionProvider : ICurrentVersionProvider
    {
        protected string Filepath { get; }

        private static object _syncRoot = new object();

        public CsvCurrentVersionProvider(string Filepath)
        {
            this.Filepath = Filepath;
        }

        private List<CurrentVersionModel> load()
        {
            lock (_syncRoot)
            {
                var result = new List<CurrentVersionModel>();
                using (FileStream stream = new FileStream(Filepath, FileMode.OpenOrCreate, FileAccess.Read))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var item = CurrentVersionModel.FromCsv(line);
                        result.Add(item);
                    }
                }
                return result;
            }
        }

        protected void save(IEnumerable<CurrentVersionModel> list)
        {
            lock (_syncRoot)
            {
                using (FileStream stream = new FileStream(Filepath, FileMode.OpenOrCreate, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    foreach (var item in list)
                        writer.WriteLine(item.ToCsv());
                }
            }
        }

        public Version GetMigrationVersion(string migrationName)
        {
            return load().SingleOrDefault(x => x.MigrationName == migrationName)?.Version ?? new Version(0, 0);
        }

        public void SetMigrationVersion(string migrationName, Version version)
        {
            List<CurrentVersionModel> models = new List<CurrentVersionModel>();
            bool found = false;
            foreach (var item in load())
            {
                if (item.MigrationName == migrationName)
                {
                    found = true;
                    item.Version = version;
                }
                models.Add(item);
            }
            if (!found)
                models.Add(new CurrentVersionModel { MigrationName = migrationName, Version = version });
            save(models);
        }


        public class CurrentVersionModel
        {
            public static readonly char[] SPLIT_CHARS = new[] { ';', ',' };

            public string MigrationName { get; set; }
            public string VersionString { get; set; }
            public Version Version { get => Version.Parse(VersionString); set => VersionString = value.ToString(); }

            public CurrentVersionModel()
            {
            }

            public static CurrentVersionModel FromCsv(string line)
            {
                if (string.IsNullOrWhiteSpace(line))
                    throw new ArgumentException(nameof(line));

                var split = line.Split(SPLIT_CHARS);
                return new CurrentVersionModel
                {
                    MigrationName = split.ElementAt(0),
                    VersionString = split.ElementAtOrDefault(1) ?? "0.0"
                };
            }

            public string ToCsv()
            {
                if (MigrationName.Any(SPLIT_CHARS.Contains))
                    throw new Exception("Invalid MigrationName - Migration names saved in csv cannot ',' or ';' as they are used as seperators");
                return $"{MigrationName},{VersionString}";
            }
        }
    }
}
