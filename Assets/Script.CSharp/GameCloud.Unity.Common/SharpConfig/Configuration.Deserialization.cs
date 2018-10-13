// Copyright (c) 2013-2015 Cemalettin Dervis, MIT License.
// https://github.com/cemdervis/SharpConfig

using System;
using System.Collections.Generic;
using System.IO;

namespace SharpConfig
{
    public partial class Configuration
    {
        private static Configuration DeserializeBinary(BinaryReader reader, string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename");

            Configuration config = null;

            using (var stream = File.OpenRead(filename))
            {
                config = DeserializeBinary(reader, stream);
            }

            return config;
        }

        private static Configuration DeserializeBinary(BinaryReader reader, Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            bool ownReader = false;

            if (reader == null)
            {
                reader = new BinaryReader(stream);
                ownReader = true;
            }

            try
            {
                var config = new Configuration();

                int sectionCount = reader.ReadInt32();

                for (int i = 0; i < sectionCount; ++i)
                {
                    string sectionName = reader.ReadString();
                    int settingCount = reader.ReadInt32();

                    Section section = new Section(sectionName);

                    DeserializeComments(reader, section);

                    for (int j = 0; j < settingCount; j++)
                    {
                        Setting setting = new Setting(
                            reader.ReadString(),
                            reader.ReadString());

                        DeserializeComments(reader, setting);

                        section.Add(setting);
                    }

                    config.Add(section);
                }

                return config;
            }
            finally
            {
                if (ownReader)
                    reader.Close();
            }
        }

        private static void DeserializeComments(BinaryReader reader, ConfigurationElement element)
        {
            bool hasComment = reader.ReadBoolean();
            if (hasComment)
            {
                char symbol = reader.ReadChar();
                string commentValue = reader.ReadString();
                element.Comment = new Comment(commentValue, symbol);
            }

            int preCommentCount = reader.ReadInt32();

            if (preCommentCount > 0)
            {
                element.mPreComments = new List<Comment>(preCommentCount);

                for (int i = 0; i < preCommentCount; ++i)
                {
                    char symbol = reader.ReadChar();
                    string commentValue = reader.ReadString();
                    element.mPreComments.Add(new Comment(commentValue, symbol));
                }
            }
        }

    }
}