// Copyright (c) 2013-2015 Cemalettin Dervis, MIT License.
// https://github.com/cemdervis/SharpConfig

using System.Collections.Generic;
using System.IO;

namespace SharpConfig
{
    public partial class Configuration
    {
        private static int mLineNumber;

        // Parses a configuration from a source string.
        // This is the core parsing function.
        private static Configuration Parse(string source)
        {
            // Reset temporary fields.
            mLineNumber = 0;

            Configuration config = new Configuration();
            Section currentSection = null;
            var preComments = new List<Comment>();

            using (var reader = new StringReader(source))
            {
                string line = null;

                // Read until EOF.
                while ((line = reader.ReadLine()) != null)
                {
                    mLineNumber++;

                    // Remove all leading / trailing white-spaces.
                    line = line.Trim();

                    // Empty line? If so, skip.
                    if (string.IsNullOrEmpty(line))
                        continue;

                    int commentIndex = 0;
                    var comment = ParseComment(line, out commentIndex);

                    if (!IgnorePreComments && commentIndex == 0)
                    {
                        // This is a comment line (pre-comment).
                        // Add it to the list of pre-comments.
                        preComments.Add(comment.Value);
                        continue;
                    }
                    else if (!IgnoreInlineComments && commentIndex > 0)
                    {
                        // Strip away the comments of this line.
                        line = line.Remove(commentIndex).Trim();
                    }

                    // Sections start with a '['.
                    if (line.StartsWith("["))
                    {
                        currentSection = ParseSection(line);

                        if (!IgnoreInlineComments)
                            currentSection.Comment = comment;

                        if (config.Contains(currentSection.Name))
                        {
                            if (IgnoreDuplicateSections)
                            {
                                continue;
                            }

                            throw new ParserException(string.Format(
                                "The section '{0}' was already declared in the configuration.",
                                currentSection.Name), mLineNumber);
                        }

                        if (!IgnorePreComments && preComments.Count > 0)
                        {
                            currentSection.mPreComments = new List<Comment>(preComments);
                            preComments.Clear();
                        }

                        config.mSections.Add(currentSection);
                    }
                    else
                    {
                        Setting setting = ParseSetting(line);

                        if (!IgnoreInlineComments)
                            setting.Comment = comment;

                        if (currentSection == null)
                        {
                            throw new ParserException(string.Format(
                                "The setting '{0}' has to be in a section.",
                                setting.Name), mLineNumber);
                        }

                        if (currentSection.Contains(setting.Name))
                        {
                            if (IgnoreDuplicateSettings)
                            {
                                continue;
                            }

                            throw new ParserException(string.Format(
                                "The setting '{0}' was already declared in the section.",
                                setting.Name), mLineNumber);
                        }

                        if (!IgnorePreComments && preComments.Count > 0)
                        {
                            setting.mPreComments = new List<Comment>(preComments);
                            preComments.Clear();
                        }

                        currentSection.Add(setting);
                    }

                }
            }

            return config;
        }

        private static bool IsInQuoteMarks(string line, int startIndex)
        {
            // Check for quote marks.
            // Note: the way it's done here is pretty primitive.
            // It will only check if there are quote marks to the left and right.
            // If so, it presumes that it's a comment symbol inside quote marks and thus, it's not a comment.
            int i = startIndex;
            bool left = false;

            while (--i >= 0)
            {
                if (line[i] == '\"')
                {
                    left = true;
                    break;
                }
            }

            bool right = (line.IndexOf('\"', startIndex) > 0);

            return left && right;
        }

        private static Comment? ParseComment(string line, out int commentIndex)
        {
            Comment? comment = null;
            commentIndex = -1;

            do
            {
                commentIndex = line.IndexOfAny(ValidCommentChars, commentIndex + 1);

                if (commentIndex < 0)
                    break;

                // Tip from MarkAJones:
                // Database connection strings can contain semicolons, which should not be
                // treated as comments, but rather separators.
                // To avoid this, we have to check for two things:
                // 1. Is the comment inside a string? If so, ignore.
                // 2. Is the comment symbol backslashed (an escaping value)? If so, ignore also.

                // If the char before the comment is a backslash, it's not a comment.
                if (commentIndex >= 1 && line[commentIndex - 1] == '\\')
                    return null;

                if (IsInQuoteMarks(line, commentIndex))
                    continue;

                comment = new Comment(
                    value: line.Substring(commentIndex + 1).Trim(),
                    symbol: line[commentIndex]);

                break;
            }
            while (commentIndex >= 0);

            return comment;
        }

        private static Section ParseSection(string line)
        {
            line = line.Trim();

            int closingBracketIndex = line.IndexOf(']');

            if (closingBracketIndex < 0)
                throw new ParserException("closing bracket missing.", mLineNumber);

            // See if there are unwanted chars after the closing bracket.
            if ((line.Length - 1) > closingBracketIndex)
            {
                string unwantedToken = line.Substring(closingBracketIndex + 1);

                throw new ParserException(string.Format(
                    "unexpected token '{0}'", unwantedToken),
                    mLineNumber);
            }

            // Read the section name, and trim all leading / trailing white-spaces.
            string sectionName = line.Substring(1, line.Length - 2).Trim();

            // Otherwise, return a fresh section.
            return new Section(sectionName);
        }

        private static Setting ParseSetting(string line)
        {
            // Find the assignment operator.
            int indexOfAssignOp = line.IndexOf('=');

            if (indexOfAssignOp < 0)
            {
                throw new ParserException("setting assignment expected.", mLineNumber);
            }

            // Trim the setting name and value.
            string settingName = line.Substring(0, indexOfAssignOp).Trim();
            string settingValue = line.Substring(indexOfAssignOp + 1, line.Length - indexOfAssignOp - 1);
            settingValue = settingValue.Trim();

            // Check if non-null name / value is given.
            if (string.IsNullOrEmpty(settingName))
            {
                throw new ParserException("setting name expected.", mLineNumber);
            }

            if (settingValue == null)
                settingValue = string.Empty;

            return new Setting(settingName, settingValue);
        }

    }
}