// Copyright (c) 2013-2015 Cemalettin Dervis, MIT License.
// https://github.com/cemdervis/SharpConfig

using System;

namespace SharpConfig
{
    /// <summary>
    /// Represents a setting in a <see cref="Configuration"/>.
    /// Settings are always stored in a <see cref="Section"/>.
    /// </summary>
    public sealed class Setting : ConfigurationElement
    {
        #region Fields

        private string mRawValue;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        public Setting(string name) :
            this(name, string.Empty)
        {
            mRawValue = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        ///
        /// <param name="name"> The name of the setting.</param>
        /// <param name="value">The value of the setting.</param>
        public Setting(string name, object value) :
            base(name)
        {
            SetValue(value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the raw string value of this setting.
        /// </summary>
        public string StringValue
        {
            get { return mRawValue; }
            set { mRawValue = value; }
        }

        /// <summary>
        /// Gets or sets the value of this setting as an int.
        /// Note: this is a shortcut to GetValue and SetValue.
        /// </summary>
        public int IntValue
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the value of this setting as a float.
        /// Note: this is a shortcut to GetValue and SetValue.
        /// </summary>
        public float FloatValue
        {
            get { return GetValue<float>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the value of this setting as a double.
        /// Note: this is a shortcut to GetValue and SetValue.
        /// </summary>
        public double DoubleValue
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the value of this setting as a bool.
        /// Note: this is a shortcut to GetValue and SetValue.
        /// </summary>
        public bool BoolValue
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the value of this settings as a date.
        /// Note: this is a shortcut to GetValue and SetValue.
        /// </summary>
        public DateTime DateTimeValue
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Gets a value indicating whether this setting is an array.
        /// </summary>
        public bool IsArray
        {
            get { return ArraySize >= 0; }
        }

        /// <summary>
        /// Gets the size of the array that this setting represents.
        /// If this setting is not an array, -1 is returned.
        /// </summary>
        public int ArraySize
        {
            get
            {
                if (string.IsNullOrEmpty(mRawValue))
                {
                    return -1;
                }

                int arrayStartIdx = mRawValue.IndexOf('{');
                int arrayEndIdx = mRawValue.LastIndexOf('}');

                if (arrayStartIdx < 0 || arrayEndIdx < 0)
                {
                    // Not an array.
                    return -1;
                }

                // There may only be spaces between the beginning
                // of the string and the first left bracket.
                for (int i = 0; i < arrayStartIdx; ++i)
                {
                    if (mRawValue[i] != ' ')
                    {
                        return -1;
                    }
                }

                // Also, there may only be spaces between the last
                // right brace and the end of the string.
                for (int i = arrayEndIdx + 1; i < mRawValue.Length; ++i)
                {
                    if (mRawValue[i] != ' ')
                    {
                        return -1;
                    }
                }

                int arraySize = 0;

                // Naive algorithm; assume the number of commas equals the number of elements + 1.
                for (int i = 0; i < mRawValue.Length; ++i)
                {
                    if (mRawValue[i] == ',')
                    {
                        ++arraySize;
                    }
                }

                if (arraySize == 0)
                {
                    // There were no commas in the array expression.
                    // That does not mean that there are no elements.
                    // Check if there is at least something.
                    // If so, that is the single element of the array.
                    for (int i = arrayStartIdx + 1; i < arrayEndIdx; ++i)
                    {
                        if (mRawValue[i] != ' ')
                        {
                            ++arraySize;
                            break;
                        }
                    }
                }
                else if (arraySize > 0)
                {
                    // If there were any commas in the array expression,
                    // we have to increment the array size, as we assumed
                    // that the number of commas equaled the number of elements + 1.
                    ++arraySize;
                }

                return arraySize;
            }
        }

        #endregion

        #region GetValueTyped

        /// <summary>
        /// Gets this setting's value as a specific type.
        /// </summary>
        ///
        /// <typeparam name="T">The type of the object to retrieve.</typeparam>
        [Obsolete("consider using GetValue().")]
        public T GetValueTyped<T>()
        {
            return GetValue<T>();
        }

        /// <summary>
        /// Gets this setting's value as a specific type.
        /// </summary>
        ///
        /// <param name="type">The type of the object to retrieve.</param>
        [Obsolete("consider using GetValue().")]
        public object GetValueTyped(Type type)
        {
            return GetValue(type);
        }

        /// <summary>
        /// Gets this setting's value as a specific type.
        /// </summary>
        ///
        /// <typeparam name="T">The type of the object to retrieve.</typeparam>
        public T GetValue<T>()
        {
            Type type = typeof(T);

            if (type.IsArray)
            {
                throw new InvalidOperationException(
                    "To obtain an array value, use GetValueArray instead of GetValueTyped.");
            }

            if (this.IsArray)
            {
                throw new InvalidOperationException(
                    "The setting represents an array. Use GetValueArray to obtain its value.");
            }

            return (T)CreateObjectFromString(mRawValue, type);
        }

        /// <summary>
        /// Gets this setting's value as a specific type.
        /// </summary>
        ///
        /// <param name="type">The type of the object to retrieve.</param>
        public object GetValue(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (type.IsArray)
            {
                throw new InvalidOperationException(
                    "To obtain an array value, use GetValueArray instead of GetValueTyped.");
            }

            if (this.IsArray)
            {
                throw new InvalidOperationException(
                    "The setting represents an array. Use GetValueArray to obtain its value.");
            }

            return CreateObjectFromString(mRawValue, type);
        }

        /// <summary>
        /// Gets this setting's value as an array of a specific type.
        /// Note: this only works if the setting represents an array. If it is not, then null is returned.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of elements in the array. All values in the array are going to be converted to objects of this type.
        ///     If the conversion of an element fails, an exception is thrown.
        /// </typeparam>
        /// <returns></returns>
        public T[] GetValueArray<T>()
        {
            int myArraySize = this.ArraySize;
            if (myArraySize < 0)
            {
                return null;
            }

            var values = new T[myArraySize];

            if (myArraySize > 0)
            {
                var enumerator = new SettingArrayEnumerator(mRawValue);

                while (enumerator.Next())
                {
                    values[enumerator.Index] = (T)CreateObjectFromString(enumerator.Current, typeof(T));
                }
            }

            return values;
        }

        /// <summary>
        /// Gets this setting's value as an array of a specific type.
        /// Note: this only works if the setting represents an array. If it is not, then null is returned.
        /// </summary>
        /// <param name="elementType">
        ///     The type of elements in the array. All values in the array are going to be converted to objects of this type.
        ///     If the conversion of an element fails, an exception is thrown.
        /// </param>
        /// <returns></returns>
        public object[] GetValueArray(Type elementType)
        {
            int myArraySize = this.ArraySize;
            if (myArraySize < 0)
            {
                return null;
            }

            var values = new object[myArraySize];

            if (myArraySize > 0)
            {
                var enumerator = new SettingArrayEnumerator(mRawValue);

                while (enumerator.Next())
                {
                    values[enumerator.Index] = CreateObjectFromString(enumerator.Current, elementType);
                }
            }

            return values;
        }

        // Converts the value of a single element to a desired type.
        private static object CreateObjectFromString(string value, Type dstType)
        {
            var underlyingType = Nullable.GetUnderlyingType(dstType);
            if (underlyingType != null)
            {
                if (string.IsNullOrEmpty(value))
                {
                    // Returns Nullable<type>().
                    return null;
                }

                // Otherwise, continue with our conversion using
                // the underlying type of the nullable.
                dstType = underlyingType;
            }

            object ret = null;
            IFormatProvider formatProvider = null;

            if (dstType == typeof(bool))
            {
                // Special case for bool.
                switch (value.ToLowerInvariant())
                {
                    case "false":
                    case "off":
                    case "no":
                    case "0":
                        ret = false;
                        break;
                    case "true":
                    case "on":
                    case "yes":
                    case "1":
                        ret = true;
                        break;
                }
            }
            else if (dstType.BaseType == typeof(Enum))
            {
                // It's possible that the value is something like:
                // UriFormat.Unescaped
                // We, and especially Enum.Parse do not want this format.
                // Instead, it wants the clean name like:
                // Unescaped
                //
                // Because of that, let's get rid of unwanted type names.
                int indexOfLastDot = value.LastIndexOf('.');

                if (indexOfLastDot >= 0)
                {
                    value = value.Substring(indexOfLastDot + 1, value.Length - indexOfLastDot - 1).Trim();
                }

                try
                {
                    ret = Enum.Parse(dstType, value);
                }
                catch (Exception ex)
                {
                    throw new SettingValueCastException(value, dstType, ex);
                }
            }
            else if (dstType == typeof(DateTime))
            {
                formatProvider = Configuration.DateTimeFormat;
            }
            else
            {
                // Assume that the destination type is a number.
                formatProvider = Configuration.NumberFormat;
            }

            if (formatProvider != null)
            {
                try
                {
                    // Main conversion routine.
                    ret = Convert.ChangeType(value, dstType, formatProvider);
                }
                catch (Exception ex)
                {
                    throw new SettingValueCastException(value, dstType, ex);
                }
            }

            return ret;
        }

        #endregion

        #region SetValue

        /// <summary>
        /// Sets the value of this setting via an object.
        /// </summary>
        /// 
        /// <param name="value">The value to set.</param>
        public void SetValue<T>(T value)
        {
            mRawValue = (value == null) ? string.Empty : value.ToString();
        }

        /// <summary>
        /// Sets the value of this setting via an array.
        /// </summary>
        /// 
        /// <param name="values">The values to set.</param>
        public void SetValue<T>(T[] values)
        {
            if (values == null)
            {
                mRawValue = string.Empty;
            }
            else
            {
                var strings = new string[values.Length];

                for (int i = 0; i < values.Length; ++i)
                {
                    strings[i] = values[i].ToString();
                }

                mRawValue = string.Format("{{{0}}}", string.Join(",", strings));
            }
        }

        /// <summary>
        /// Sets the value of this setting via a DateTime object.
        /// DateTime.ToString() is used for the conversion, using the format string and DateTimeFormat that
        /// is set in and <see cref="Configuration.DateTimeFormat"/>, respectively.
        /// </summary>
        /// <param name="value">The time value to set.</param>
        public void SetValue(DateTime value)
        {
            mRawValue = value.ToString(Configuration.DateTimeFormat);
        }

        /// <summary>
        /// Sets the value of this setting via a DateTime array.
        /// DateTime.ToString() is used for the conversion, using the format string and DateTimeFormat that
        /// is set in and <see cref="Configuration.DateTimeFormat"/>, respectively.
        /// </summary>
        /// <param name="values">The time values to set.</param>
        public void SetValue(DateTime[] values)
        {
            if (values == null)
            {
                mRawValue = null;
            }
            else
            {
                var strings = new string[values.Length];

                for (int i = 0; i < values.Length; ++i)
                {
                    strings[i] = values[i].ToString(Configuration.DateTimeFormat);
                }

                mRawValue = string.Format("{{{0}}}", string.Join(",", strings));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the string representation of the setting, without its comments.
        /// </summary>
        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// Gets the string representation of the setting.
        /// </summary>
        ///
        /// <param name="includeComment">Specify true to include the comments in the string; false otherwise.</param>
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
                    return string.Format("{0}{1}{2} = {3} {4}",
                        string.Join(Environment.NewLine, preCommentStrings), // {0}
                        Environment.NewLine,    // {1}
                        Name,                   // {2}
                        mRawValue,              // {3}
                        Comment.ToString()      // {4}
                        );
                }
                else if (Comment != null)
                {
                    // Include only the inline comment.
                    return string.Format("{0} = {1} {2}", Name, mRawValue, Comment.ToString());
                }
                else if (hasPreComments)
                {
                    // Include only the pre-comments.
                    return string.Format("{0}{1}{2} = {3}",
                        string.Join(Environment.NewLine, preCommentStrings), // {0}
                        Environment.NewLine,    // {1}
                        Name,                   // {2}
                        mRawValue               // {3}
                        );
                }
            }

            // In every other case, include just the assignment in the string.
            return string.Format("{0} = {1}", Name, mRawValue);
        }

        #endregion
    }
}
