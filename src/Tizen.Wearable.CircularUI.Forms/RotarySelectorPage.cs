using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The RotarySelectorPage is a Page that shows the menu in a circle screen.
    /// Each menu item is placed inside the edge of a rounded screen.
    /// </summary>
    public class RotarySelectorPage : Page
    {
        // TODO: native selected_set function is not working. implement it after bug patched.
        // public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(RotarySelectorItem), typeof(RotarySelectorPage), null);

        readonly ObservableCollection<RotarySelectorItem> _items = new ObservableCollection<RotarySelectorItem>();

        /// <summary>
        /// Creates and initializes a new instance of the RotarySelectorPage class.
        /// </summary>
        public RotarySelectorPage()
        {
            _items.CollectionChanged += OnItemsCollectionChanged;
        }

        /// <summary>
        /// Gets a list of RotarySelectorItem represented through RotarySelectorPage.
        /// </summary>
        public ObservableCollection<RotarySelectorItem> Items => _items;

        /* TODO: native selected_set function is not working. implement it after bug patched.
        public RotarySelectorItem SelectedItem
        {
            get => (RotarySelectorItem)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        */

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            foreach (var item in _items)
            {
                SetInheritedBindingContext(item, BindingContext);
            }
        }

        void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Element item in e.NewItems)
                {
                    item.Parent = this;
                }
            }
        }
    }
}