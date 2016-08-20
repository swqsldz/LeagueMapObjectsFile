using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LeagueMapObjectsFile
{
    public class MapObjectsFile
    {
        public uint Version;
        public uint Zero;
        public List<MapObjectsFileObject> Objects = new List<MapObjectsFileObject>();

        public MapObjectsFile(string fileLocation)
        {
            using (BinaryReader br = new BinaryReader(File.Open(fileLocation, FileMode.Open)))
            {
                this.Read(br);
            }
        }

        public void Save(string fileLocation)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(fileLocation, FileMode.Create)))
            {
                this.Write(bw);
            }
        }

        private void Read(BinaryReader br)
        {
            string magic = Encoding.ASCII.GetString(br.ReadBytes(4));
            if (magic != "OPAM")
                throw new Exception("Invalid MapObjects file");
            this.Version = br.ReadUInt32();
            uint objectsCount = br.ReadUInt32();
            this.Zero = br.ReadUInt32();
            for (int i = 0; i < objectsCount; i++)
            {
                this.Objects.Add(new MapObjectsFileObject(br));
            }
        }
        private void Write(BinaryWriter bw)
        {
            bw.Write(Encoding.ASCII.GetBytes("OPAM"));
            bw.Write(this.Version);
            bw.Write((uint)this.Objects.Count);
            bw.Write(this.Zero);
            foreach (MapObjectsFileObject mapObject in this.Objects)
            {
                mapObject.Write(bw);
            }
        }

        private static string GetStringFromChars(char[] chars)
        {
            string final = "";
            int i = 0;
            while (i < chars.Length && chars[i] != 0)
            {
                final += chars[i];
                i++;
            }
            return final;
        }
        private static char[] GetCharsFromString(string str, int size)
        {
            char[] final = new char[size];
            int i = 0;
            while (i < size && i < str.Length)
            {
                final[i] = str[i];
                i++;
            }
            return final;
        }

        public class MapObjectsFileObject
        {
            public string Name;
            public ushort Zero1 = 0;
            public MapObjectType Type;
            public byte Zero2 = 0;
            public float[] Position = new float[3];
            public float[] Rotation = new float[3];
            public float[] Scale = new float[3];
            public float[] HealthBarPosition1 = new float[3];
            public float[] HealthBarPosition2 = new float[3];
            public uint Zero3 = 0;

            public MapObjectsFileObject(BinaryReader br)
            {
                this.Name = GetStringFromChars(br.ReadChars(60));
                this.Zero1 = br.ReadUInt16();
                this.Type = (MapObjectType)br.ReadByte();
                this.Zero2 = br.ReadByte();
                for (int i = 0; i < 3; i++)
                {
                    this.Position[i] = br.ReadSingle();
                }
                for (int i = 0; i < 3; i++)
                {
                    this.Rotation[i] = br.ReadSingle();
                }
                for (int i = 0; i < 3; i++)
                {
                    this.Scale[i] = br.ReadSingle();
                }
                for (int i = 0; i < 3; i++)
                {
                    this.HealthBarPosition1[i] = br.ReadSingle();
                }
                for (int i = 0; i < 3; i++)
                {
                    this.HealthBarPosition2[i] = br.ReadSingle();
                }
                this.Zero3 = br.ReadUInt32();
            }

            public MapObjectsFileObject(string objectName)
            {
                this.Name = objectName;
            }

            public void Write(BinaryWriter bw)
            {
                bw.Write(GetCharsFromString(this.Name, 60));
                bw.Write(this.Zero1);
                bw.Write((byte)this.Type);
                bw.Write(this.Zero2);
                for (int i = 0; i < 3; i++)
                {
                    bw.Write(this.Position[i]);
                }
                for (int i = 0; i < 3; i++)
                {
                    bw.Write(this.Rotation[i]);
                }
                for (int i = 0; i < 3; i++)
                {
                    bw.Write(this.Scale[i]);
                }
                for (int i = 0; i < 3; i++)
                {
                    bw.Write(this.HealthBarPosition1[i]);
                }
                for (int i = 0; i < 3; i++)
                {
                    bw.Write(this.HealthBarPosition2[i]);
                }
                bw.Write(this.Zero3);
            }
        }

        public enum MapObjectType : byte {
            BarrackSpawn = 0,
            NexusSpawn = 1,
            LevelSize = 2,
            Barrack = 3,
            Nexus = 4,
            Turret = 5,
            Nav = 8,
            Info = 9,
            LevelProp = 10 };
    }
}
