// Copyright (c) 2013-2015 Cemalettin Dervis, MIT License.
// https://github.com/cemdervis/SharpConfig

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SharpConfig
{
    /// <summary>
    /// Represents a group of <see cref="Setting"/> objects.
    /// </summary>
    public sealed class Section : ConfigurationElement, IEnumerable<Setting>
    {
        private List<Setting> mSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="Section"/> class.
        /// </summary>
        ///
        /// <param name="name">The name of the section.</param>
        public Section(string name)
            : base(name)
        {
            mSettings = new List<Setting>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Section"/> class that is
        /// based on an existing object.
        /// Important: the section is built only from the public getter properties
        /// and fields of its type.
        /// When this method is called, all of those properties will be called
        /// and fields accessed once to obtain their values.
        /// Properties and fields that are marked with the <see cref="IgnoreAttribute"/> attribute
        /// or are of a type that is marked with that attribute, are ignored.
        /// </summary>
        /// <param name="name">The name of the section.</param>
        /// <param name="obj"></param>
        /// <returns>The newly created section.</returns>
        public static Section FromObject<T>(string name, T obj)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The section name must not be null or empty.", "name");
            }

            if (obj == null)
            {
                throw new ArgumentNullException("obj", "obj must not be null.");
            }

            var section = new Section(name);
            var type = typeof(T);

            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!prop.CanRead || ShouldIgnoreMappingFor(prop))
                {
                    // Skip this property, as it can't be read from.
                    continue;
                }

                Setting setting = new Setting(prop.Name, prop.GetValue(obj, null));
                section.mSettings.Add(setting);
            }

            // Repeat for each public field.
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                if (ShouldIgnoreMappingFor(field))
                {
                    // Skip this field.
                    continue;
                }

                Setting setting = new Setting(field.Name, field.GetValue(obj));
                section.mSettings.Add(setting);
            }

            return section;
        }

        /// <summary>
        /// Creates an object of a specific type, and maps the settings
        /// in this section to the public properties and writable fields of the object.
        /// Properties and fields that are marked with the <see cref="IgnoreAttribute"/> attribute
        /// or are of a type that is marked with that attribute, are ignored.
        /// </summary>
        /// 
        /// <returns>The created object.</returns>
        /// 
        /// <remarks>
        /// The specified type must have a public default constructor
        /// in order to be created.
        /// </remarks>
        public T CreateObject<T>()
        {
            Type type = typeof(T);

            try
            {
                T obj = Activator.CreateInstance<T>();
                MapTo(obj);

                return obj;
            }
            catch (Exception)
            {
                throw new ArgumentException(string.Format(
                    "The type '{0}' does not have a default public constructor.",
                    type.Name));
            }
        }

        private static bool ShouldIgnoreMappingFor(MemberInfo member)
        {
            if (member.GetCustomAttributes(typeof(IgnoreAttribute), false).Length > 0)
            {
                return true;
            }
            else
            {
                PropertyInfo prop = member as PropertyInfo;
                if (prop != null)
                {
                    return prop.PropertyType.GetCustomAttributes(typeof(IgnoreAttribute), false).Length > 0;
                }

                FieldInfo field = member as FieldInfo;
                if (field != null)
                {
                    return field.FieldType.GetCustomAttributes(typeof(IgnoreAttribute), false).Length > 0;
                }
            }

            return false;
        }

        /// <summary>
        /// Assigns the values of this section to an object's public properties and fields.
        /// Properties and fields that are marked with the <see cref="IgnoreAttribute"/> attribute
        /// or are of a type that is marked with that attribute, are ignored.
        /// </summary>
        /// 
        /// <param name="obj">The object that is modified based on the section.</param>
        public void MapTo<T>(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            Type type = typeof(T);

            // Scan the type's properties.
            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!prop.CanWrite || ShouldIgnoreMappingFor(prop))
                {
                    continue;
                }

                var setting = FindSetting(prop.Name);

                if (setting != null)
                {
                    object value = setting.GetValue(prop.PropertyType);
                    prop.SetValue(obj, value, null);
                }
            }

            // Scan the type's fields.
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                // Skip readonly fields.
                if (field.IsInitOnly || ShouldIgnoreMappingFor(field))
                {
                    continue;
                }

                var setting = FindSetting(field.Name);

                if (setting != null)
                {
                    object value = setting.GetValue(field.FieldType);
                    field.SetValue(obj, value);
                }
            }
        }

        /// <summary>
        /// Gets an enumerator that iterates through the section.
        /// </summary>
        public IEnumerator<Setting> GetEnumerator()
        {
            return mSettings.GetEnumerator();
        }

        /// <summary>
        /// Gets an enumerator that iterates through the section.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds a setting to the section.
        /// </summary>
        /// <param name="setting">The setting to add.</param>
        public void Add(Setting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }

            if (Contains(setting))
            {
                throw new ArgumentException("The specified setting already exists in the section.");
            }

            if (Contains(setting.Name))
            {
                throw new ArgumentException(string.Format(
                    "A setting named '{0}' already exists in the section.",
                    setting.Name
                    ));
            }

            mSettings.Add(setting);
        }

        /// <summary>
        /// Removes a setting from the section by its name.
        /// </summary>
        public void Remove(string settingName)
        {
            if (string.IsNullOrEmpty(settingName))
            {
                throw new ArgumentNullException("settingName");
            }

            var setting = FindSetting(settingName);

            if (setting == null)
            {
                throw new ArgumentException(string.Format(
                    "A setting named '{0}' does not exist in the section.",
                    settingName
                    ));
            }

            mSettings.Remove(setting);
        }

        /// <summary>
        /// Removes a setting from the section.
        /// </summary>
        /// <param name="setting">The setting to remove.</param>
        public void Remove(Setting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }

            if (!Contains(setting))
            {
                throw new ArgumentException("The specified setting does not exist in the section.");
            }

            mSettings.Remove(setting);
        }

        /// <summary>
        /// Clears the section of all settings.
        /// </summary>
        public void Clear()
        {
            mSettings.Clear();
        }

        /// <summary>
        /// Determines whether a specified setting is contained in the section.
        /// </summary>
        /// <param name="setting">The setting to check for containment.</param>
        /// <returns>True if the setting is contained in the section; false otherwise.</returns>
        public bool Contains(Setting setting)
        {
            return mSettings.Contains(setting);
        }

        /// <summary>
        /// Determines whether a specifically named setting is contained in the section.
        /// </summary>
        /// <param name="settingName">The name of the setting.</param>
        /// <returns>True if the setting is contained in the section; false otherwise.</returns>
        public bool Contains(string settingName)
        {
            return FindSetting(settingName) != null;
        }

        /// <summary>
        /// Gets the number of settings that are in the section.
        /// </summary>
        public int SettingCount
        {
            get { return mSettings.Count; }
        }

        /// <summary>
        /// Gets or sets a setting by index.
        /// </summary>
        /// <param name="index">The index of the setting in the section.</param>
        /// 
        /// <returns>
        /// The setting at the specified index.
        /// Note: no setting is created when using this accessor.
        /// </returns>
        public Setting this[int index]
        {
            get
            {
                if (index < 0 || index >= mSettings.Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                return mSettings[index];
            }
        }

        /// <summary>
        /// Gets or sets a setting by its name.
        /// </summary>
        ///
        /// <param name="name">The name of the setting.</param>
        ///
        /// <returns>
        /// The setting if found, otherwise a new setting with
        /// the specified name is created, added to the section and returned.
        /// </returns>
        public Setting this[string name]
        {
            get
            {
                var setting = FindSetting(name);

                if (setting == null)
                {
                    setting = new Setting(name);
                    Add(setting);
                }

                return setting;
            }
        }

        // Finds a setting by its name.
        private Setting FindSetting(string name)
        {
            foreach (var setting in mSettings)
            {
                if (string.Equals(setting.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return setting;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the string representation of the section, without its comments.
        /// </summary>
        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// Gets the string representation of the section.
        /// </summary>
        ///
        /// <param name="includeComment">True to include, false to exclude the comment.</param>
        public string ToString(bool includeComment)
        {
            if (includeComment)
            {
                bool hasPreComments = mPreComments != null && mPreComments.Count > 0;

                string[] preCommentStrings = hasPreComments ?
                    mPreComments.ConvertAll(c => c.ToString()).ToArray() : null;

                if (Comment != null && hasPreComments)
                {
                    // Include inline comment and pre-comments.
                    return string.Format("{0}{1}[{2}] {3}",
                        string.Join(Environment.NewLine, preCommentStrings),    // {0}
                        Environment.NewLine,    // {1}
                        Name,                   // {2}
                        Comment.ToString()      // {3}
                        );
                }
                else if (Comment != null)
                {
                    // Include only the inline comment.
                    return string.Format("[{0}] {1}", Name, Comment.ToString());
                }
                else if (hasPreComments)
                {
                    // Include only the pre-comments.
                    return string.Format("{0}{1}[{2}]",
                        string.Join(Environment.NewLine, preCommentStrings),    // {0}
                        Environment.NewLine,    // {1}
                        Name                    // {2}
                        );
                }
            }

            return string.Format("[{0}]", Name);
        }
    }
}