using Newtonsoft.Json;
using ML.Lift.Structures.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Lift.Sandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var sRepo = new ML.Lift.Structures.Repositories.FakeStructureRepository();
            //BuildTestStuff();
        }

        public static void BuildTestStuff()
        {
            var structures = BuildTestStructures();
            WriteToJsonFile(structures);
            WriteTestIdsFile(structures);
        }

        public static Structure[] BuildTestStructures()
        {
            const int structureCount = 10;
            const int lineSetCount = 5;
            const int lineCount = 6;
            const int floorCount = 50;

            var structures = new List<Structure>();
            for (var structureIndex = 1; structureIndex <= structureCount; structureIndex++)
            {
                var s = new Structure();
                s.Description = string.Format("Structure_{0}", structureIndex);
                s.Id = Guid.Parse(string.Format("{0,8:D8}-0000-0000-0000-000000000000", structureIndex));

                var lineSets = new List<LineSet>();
                for (var lineSetIndex = 1; lineSetIndex <= lineSetCount; lineSetIndex++)
                {
                    var ls = new LineSet();
                    ls.Description = string.Format("LineSet_{0}_{1}", structureIndex, lineSetIndex);
                    ls.Id = Guid.Parse(string.Format("{0,8:D8}-{1,4:D4}-0000-0000-000000000000", structureIndex, lineSetIndex));
                    var lines = new List<Line>();
                    for (var lineIndex = 1; lineIndex <= lineCount; lineIndex++)
                    {
                        var l = new Line();
                        l.Description = string.Format("Line_{0}_{1}_{2}", structureIndex, lineSetIndex, lineIndex);
                        l.Id = Guid.Parse(string.Format("{0,8:D8}-{1,4:D4}-{2,4:D4}-0000-000000000000", structureIndex, lineSetIndex, lineIndex));
                        lines.Add(l);
                    }
                    ls.Lines = lines.ToArray();
                    var floors = new List<Floor>();
                    for (var floorIndex = 1; floorIndex <= floorCount; floorIndex++)
                    {
                        var f = new Floor();
                        f.Description = string.Format("Floor_{0}_{1}_{2}", structureIndex, lineSetIndex, floorIndex);
                        f.Id = Guid.Parse(string.Format("{0,8:D8}-{1,4:D4}-0000-{2,4:D4}-000000000000", structureIndex, lineSetIndex, floorIndex));
                        floors.Add(f);
                    }
                    ls.Floors = floors.ToArray();
                    lineSets.Add(ls);
                }
                s.LineSets = lineSets.ToArray();
                structures.Add(s);
            }
            return structures.ToArray();
        }

        public static void WriteToJsonFile(Structure[] structures)
        {
            var json = JsonConvert.SerializeObject(structures, Formatting.Indented);
            const string output = "testStructures.json";
            File.WriteAllText(output, json);
        }

        public static void WriteTestIdsFile(Structure[] structures)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("");
            builder.AppendLine("namespace ML.Lift.Structures.Abstractions.Constants");
            builder.AppendLine("{");
            builder.AppendLine("    public static class TestIds");
            builder.AppendLine("    {");
            builder.AppendLine("        // Structures");
            foreach (var structure in structures)
            {
                builder.AppendLine(string.Format("        public static readonly Guid {0} = new Guid(\"{1}\");", structure.Description, structure.Id.ToString()));
            }
            builder.AppendLine("        // LineSets");
            foreach (var structure in structures)
            {
                foreach (var lineSet in structure.LineSets)
                {
                    builder.AppendLine(string.Format("        public static readonly Guid {0} = new Guid(\"{1}\");", lineSet.Description, lineSet.Id.ToString()));
                }
            }
            builder.AppendLine("        // Lines");
            foreach (var structure in structures)
            {
                foreach (var lineSet in structure.LineSets)
                {
                    foreach (var line in lineSet.Lines)
                    {
                        builder.AppendLine(string.Format("        public static readonly Guid {0} = new Guid(\"{1}\");", line.Description, line.Id.ToString()));
                    }
                }
            }
            builder.AppendLine("        // Floors");
            foreach (var structure in structures)
            {
                foreach (var lineSet in structure.LineSets)
                {
                    foreach (var floor in lineSet.Floors)
                    {
                        builder.AppendLine(string.Format("        public static readonly Guid {0} = new Guid(\"{1}\");", floor.Description, floor.Id.ToString()));
                    }
                }
            }
            builder.AppendLine("    }");
            builder.AppendLine("}");
            const string output = "TestIds.cs";
            File.WriteAllText(output, builder.ToString());
        }
    }
}
