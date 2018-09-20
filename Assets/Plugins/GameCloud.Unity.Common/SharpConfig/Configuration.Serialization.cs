// Copyright (c) 2013-2015 Cemalettin Dervis, MIT License.
// https://github.com/cemdervis/SharpConfig

using System;
using System.Text;
using System.IO;

namespace SharpConfig
{
    public partial class Configuration
    {
        private void Serialize(string filename, Encoding encoding)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("filename");
            }

            using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                Serialize(stream, encoding);
            }
        }

        private void Serialize(Stream stream, Encoding encoding)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            var sb = new StringBuilder();

            // Write all sections.
            bool isFirstSection = true;

            foreach (var section in this)
            {
                if (!isFirstSection)
                {
                    sb.AppendLine();
                }

                // Leave some space between this section and the element that is above,
                // if this section has pre-comments and isn't the first section in the configuration.
                if (!isFirstSection &&
                    section.mPreComments != null && section.mPreComments.Count > 0)
                {
                    sb.AppendLine();
                }

                sb.AppendLine(section.ToString(true));

                // Write all settings.
                foreach (var setting in section)
                {
                    // Leave some space between this setting and the element that is above,
                    // if this element has pre-comments.
                    if (setting.mPreComments != null && setting.mPreComments.Count > 0)
                    {
                        sb.AppendLine();
                    }

                    sb.AppendLine(setting.ToString(true));
                }

                isFirstSection = false;
            }

            // Write to stream.
            var writer = (encoding == null) ?
                new StreamWriter(stream) :
                new StreamWriter(stream, encoding);

            using (writer)
            {
                writer.Write(sb.ToString());
            }
        }

        private void SerializeBinary(BinaryWriter writer, string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("filename");
            }

            using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                SerializeBinary(writer, stream);
            }
        }

        private void SerializeBinary(BinaryWriter writer, Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            bool ownWriter = false;

            if (writer == null)
            {
                writer = new BinaryWriter(stream);
                ownWriter = true;
            }

            try
            {
                writer.Write(SectionCount);

                foreach (var section in this)
                {
                    writer.Write(section.Name);
                    writer.Write(section.SettingCount);

                    SerializeComments(writer, section);

                    // Write the section's settings.
                    foreach (var setting in section)
                    {
                        writer.Write(setting.Name);
                        writer.Write(setting.StringValue);

                        SerializeComments(writer, setting);
                    }
                }
            }
            finally
            {
                if (ownWriter)
                {
                    writer.Close();
                }
            }
        }

        private static void SerializeComments(BinaryWriter writer, ConfigurationElement element)
        {
            // Write the comment.
            var commentNullable = element.Comment;

            writer.Write(commentNullable.HasValue);
            if (commentNullable.HasValue)
            {
                var comment = commentNullable.Value;
                writer.Write(comment.Symbol);
                writer.Write(comment.Value);
            }

            // Write the pre-comments.
            // Note: do not access the PreComments property of element,
            // as it will lazily create a new List of pre-comments.
            // Access the private field instead.
            var preComments = element.mPreComments;
            bool hasPreComments = (preComments != null && preComments.Count > 0);

            writer.Write(hasPreComments ? preComments.Count : 0);

            if (hasPreComments)
            {
                foreach (var preComment in preComments)
                {
                    writer.Write(preComment.Symbol);
                    writer.Write(preComment.Value);
                }
            }
        }

    }
}