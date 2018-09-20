using System.Diagnostics;
#if (UNITY_IOS || UNITY_IPHONE)
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Aot
{
	public static class EnumerationExtension
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			if (enumerable == null)
				return;

            var enumerableType = typeof(IEnumerable);

            if(!enumerable.GetType().GetInterfaces().Contains(enumerableType))
                throw new ArgumentException("Object does not implement IEnumerable", "enumerable");

            var method = enumerableType.GetMethod("GetEnumerator");

			if (method == null)
				throw new FormatException("Failed to get 'GetEnumerator()' method from IEnumerable type");

			IEnumerator enumerator = null;

			try
			{
                enumerator = (IEnumerator)method.Invoke(enumerable, null);

				if (enumerator != null)
				{
					while (enumerator.MoveNext())
					{
						action((T)enumerator.Current);
					}
				}
				else
				{
					throw new FormatException(string.Format("GetEnumerator() return null for type {0}", enumerable.GetType()));
				}
			}
			finally
			{
				var disposable = enumerator as IDisposable;

				if (disposable != null)
                {
					disposable.Dispose();
			}
		}
	}

		public static void ForEach(this IEnumerable enumerable, Action<object> action)
		{

			if (enumerable == null)
				return;

			try
			{
				//Trying converstion to array and wrapped collection first for performance
				var arrayEnumerable = enumerable as object[];

				if (arrayEnumerable != null)
				{
					for (var i = 0; i < arrayEnumerable.Length; i++)
					{
						action(arrayEnumerable[i]);
					}

					return;
				}

				var wrappedEnumerable = enumerable as CollectionWrapper<object>;

				if (wrappedEnumerable != null)
				{
					if (wrappedEnumerable.IsGenericCollection())
					{
						((ICollection<object>) wrappedEnumerable.UnderlyingCollection).ForEach(action);
						return;	
					}

					((IEnumerable) wrappedEnumerable.UnderlyingCollection).ForEach(action);
					return;
				}
			}
			catch (Exception ex)
			{
				//Create a more descriptive exception and set InnerException to maintain stack trace.
				throw new FormatException("Error getting appropriate Enumerator from IEnumerable", ex);
			}
			
			//Conversions failed, try to get the Enumerator from the enumerable
			IEnumerator enumerator = null;

			try
			{
				var enumerableType = typeof(IEnumerable);

				if (!enumerable.GetType().GetInterfaces().Contains(enumerableType))
					throw new ArgumentException("Object does not implement IEnumerable", "enumerable");

				var method = enumerableType.GetMethod("GetEnumerator");

				if (method == null)
					throw new FormatException("Failed to get 'GetEnumerator()' method from IEnumerable type");

				object enumeratorResult = method.Invoke(enumerable, null);

				if (enumeratorResult == null)
				{
					throw new FormatException(string.Format("GetEnumerator() return null for type {0}", enumerable.GetType()));
				}

				if (enumeratorResult is string)
				{
					throw new FormatException("GetEnumerator() return a string (Mono Bug)");
				}

				enumerator = (IEnumerator)enumeratorResult;

				while (enumerator.MoveNext())
				{
					action(enumerator.Current);
				}
			}
			catch (Exception ex)
			{
				//Create a more descriptive exception and set InnerException to maintain stack trace.
				throw new FormatException("Error getting appropriate Enumerator from IEnumerable", ex);
			}
			finally
			{
				if (enumerator != null)
				{
					var disposable = enumerator as IDisposable;

					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
	}
}
#endif