#if !UNITY_WINRT || UNITY_EDITOR || (UNITY_WP8 &&  !UNITY_WP_8_1)
using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.ObservableSupport
{
	public class NotifyCollectionChangedEventArgs
	{
		internal NotifyCollectionChangedAction Action { get; set; }
		internal IList NewItems { get; set; }
		internal int NewStartingIndex { get; set; }
		internal IList OldItems { get; set; }
		internal int OldStartingIndex { get; set; }

		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
		{
			Action = action;

		}

		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems) 
			: this(action)
		{
			NewItems = changedItems;
		}

		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, Object changedItem)
			: this(action)
		{
			NewItems = new List<Object>{ changedItem };
		}

		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
			: this(action, newItems)
		{
			OldItems = oldItems;
		}

		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
			: this(action, changedItems)
		{
			NewStartingIndex = startingIndex;
		}

		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, Object changedItem, int index)
			: this(action, changedItem)
		{
			NewStartingIndex = index;
		}

		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, Object newItem, Object oldItem)
			: this(action, newItem)
		{
			OldItems = new List<Object> { oldItem };
		}

		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex)
			: this(action, newItems, oldItems)
		{
			NewStartingIndex = startingIndex;
		}

		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int index, int oldIndex)
			: this(action, changedItems, index)
		{
			OldStartingIndex = oldIndex;
		}

		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, Object changedItem, int index, int oldIndex)
			: this(action, changedItem, index)
		{
			OldStartingIndex = oldIndex;
		}

		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, Object newItem, Object oldItem, int index)
			: this(action, newItem, oldItem)
		{
			NewStartingIndex = index;
		}
	}
}

#endif